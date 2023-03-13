using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Seats.Commands.CreateSeat
{
    public sealed record CreateSeatCommand(CreateSeatDto Dto) : IRequest<int>;

    public sealed class CreateSeatHandler : IRequestHandler<CreateSeatCommand, int>
    {
        public CreateSeatHandler(
            DbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public async Task<int> Handle(
            CreateSeatCommand request,
            CancellationToken cancellationToken)
        {
            var seatInfo = _mapper.Map<Seat>(request.Dto);

            var exists = await IsExistsEntryAsync(
                seatInfo.Row,
                seatInfo.Column,
                cancellationToken);
            if (exists)
                throw new BadRequestException("Запись на это место уже создана");

            await _context.AddAsync(seatInfo, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return seatInfo.Id;
        }
        private async Task<bool> IsExistsEntryAsync(
            int row,
            int column,
            CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<Seat>()
                .AsNoTracking()
                .AnyAsync(u => u.Row == row && u.Column == column, cancellationToken);

            return exists;
        }
    }
}
