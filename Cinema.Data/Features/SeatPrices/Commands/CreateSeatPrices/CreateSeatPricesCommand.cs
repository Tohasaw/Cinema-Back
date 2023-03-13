using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.SeatPrices.Commands.CreateSeatPrices
{
    public sealed record CreateSeatPricesCommand(IEnumerable<CreateSeatPricesDto> Dtos) : IRequest;

    public sealed class CreateSeatPricesHandler : AsyncRequestHandler<CreateSeatPricesCommand>
    {
        public CreateSeatPricesHandler(
            DbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        protected override async Task Handle(
            CreateSeatPricesCommand request,
            CancellationToken cancellationToken)
        {
            foreach(var dto in request.Dtos)
            {
                var existingSeatPrice = await GetExistRowAsync(
                dto.SeatId,
                dto.PriceListId,
                cancellationToken);
                if (existingSeatPrice != null) {
                    if (existingSeatPrice.PriceId != dto.PriceId) {
                        existingSeatPrice.PriceId = dto.PriceId;
                    }
                } else {
                    var seatPrice = _mapper.Map<SeatPrice>(dto);
                    await _context.AddAsync(seatPrice, cancellationToken);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
        private async Task<SeatPrice?> GetExistRowAsync(
            int seatId,
            int priceListId,
            CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<SeatPrice>()
                .SingleOrDefaultAsync(u => u.SeatId == seatId && u.PriceListId == priceListId, cancellationToken);

            return exists;
        }
    }
}
