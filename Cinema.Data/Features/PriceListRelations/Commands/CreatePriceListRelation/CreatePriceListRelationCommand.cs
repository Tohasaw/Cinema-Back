using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using Cinema.Data.Services.JwtTokenServices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.PriceListRelations.Commands.CreatePriceListRelation
{
    public sealed record CreatePriceListRelationCommand(CreatePriceListRelationDto Dto) : IRequest<int>;
    public sealed class CreatePriceListRelationHandler : IRequestHandler<CreatePriceListRelationCommand, int>
    {
        private readonly DbContext _context;
        private readonly JwtSecurityTokenService _jwtSecurityTokenService;
        private readonly IMapper _mapper;
        public CreatePriceListRelationHandler(
            DbContext context,
            JwtSecurityTokenService jwtSecurityTokenService,
            IMapper mapper)
        {
            _context = context;
            _jwtSecurityTokenService = jwtSecurityTokenService;
            _mapper = mapper;
        }

        public async Task<int> Handle(
            CreatePriceListRelationCommand request, 
            CancellationToken cancellationToken)
        {
            var relation = _mapper.Map<PriceListRelation>(request.Dto);

            var exists1 = await IsExistPriceListAsync(
                relation.PriceListId,
                cancellationToken);
            if (!exists1)
                throw new BadRequestException("Выбранный прайс-лист не существует");

            var exists2 = await IsExistPriceAsync(
                relation.PriceId,
                cancellationToken);
            if (!exists2)
                throw new BadRequestException("Выбранная цена не существует");

            var existRelation = await IsExistRelation(
                relation.PriceListId,
                relation.PriceId,
                cancellationToken);
            if (existRelation)
                throw new BadRequestException("Запись уже существует");

            await _context.AddAsync(relation, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return relation.Id;
        }
        private async Task<bool> IsExistPriceListAsync(
            int priceListId,
            CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<PriceList>()
                .AsNoTracking()
                .AnyAsync(u => u.Id == priceListId, cancellationToken);

            return exists;
        }
        private async Task<bool> IsExistPriceAsync(
            int priceId,
            CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<Price>()
                .AsNoTracking()
                .AnyAsync(u => u.Id == priceId, cancellationToken);

            return exists;
        }
        private async Task<bool> IsExistRelation(
            int priceListId,
            int priceId,
            CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<PriceListRelation>()
                .AsNoTracking()
                .AnyAsync(u => u.PriceId == priceId && u.PriceListId == priceListId, cancellationToken);

            return exists;
        }
    }
}
