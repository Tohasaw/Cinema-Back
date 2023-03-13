using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Prices.Commands.CreatePrice
{
    public sealed record CreatePriceCommand(CreatePriceDto Dto) : IRequest<int>;

    public sealed class CreatePriceHandler : IRequestHandler<CreatePriceCommand, int>
    {
        public CreatePriceHandler(
            DbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public async Task<int> Handle(
            CreatePriceCommand request,
            CancellationToken cancellationToken)
        {
            var price = _mapper.Map<Price>(request.Dto);

            var exists = await IsExistsPriceAsync(
                price.Value,
                cancellationToken);
            if (exists)
                throw new BadRequestException("Запись с выбранной ценой уже существует");

            await _context.AddAsync(price, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return price.Id;
        }
        private async Task<bool> IsExistsPriceAsync(
            double value,
            CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<Price>()
                .AsNoTracking()
                .AnyAsync(u => u.Value == value, cancellationToken);

            return exists;
        }
    }
}
