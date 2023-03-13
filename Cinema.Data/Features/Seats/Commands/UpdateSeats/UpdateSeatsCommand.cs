using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Seats.Commands.UpdateSeats
{
    public sealed record UpdateSeatsCommand(int[] SeatIds) : IRequest;

    public sealed class UpdateSeatsHandler : AsyncRequestHandler<UpdateSeatsCommand>
    {
        public UpdateSeatsHandler(
            DbContext context)
        {
            _context = context;
        }
        private readonly DbContext _context;

        protected override async Task Handle(
            UpdateSeatsCommand request,
            CancellationToken cancellationToken)
        {
            var seats = await _context.Set<Seat>().ToListAsync();

            UpdateSeats(request.SeatIds, seats);

            await _context.SaveChangesAsync(cancellationToken);
        }
        private void UpdateSeats(int[] seatIds, IList<Seat> seats)
        {
            foreach (var id in seatIds)
            {
                var seat = seats.SingleOrDefault(s => s.Id == id);
                if (seat != null)
                {
                    seat.IsAvailable = !seat.IsAvailable;
                }
            }
        }
    }
}
