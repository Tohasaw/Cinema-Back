using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.PriceLists.Commands.CreatePriceList
{
    public sealed record CreatePriceListCommand(CreatePriceListDto Dto) : IRequest<int>;

    public sealed class CreatePriceListHandler : IRequestHandler<CreatePriceListCommand, int>
    {
        public CreatePriceListHandler(
            DbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public async Task<int> Handle(
            CreatePriceListCommand request,
            CancellationToken cancellationToken)
        {
            var priceList = _mapper.Map<PriceList>(request.Dto);
            priceList.DateTime = DateTime.Now;

            await _context.AddAsync(priceList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return priceList.Id;
        }
    }
}
