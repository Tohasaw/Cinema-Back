using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.SeatPrices.Commands.CreatePurchase
{
    public sealed record CreatePurchaseCommand(CreatePurchaseDto Dto) : IRequest;

    public sealed class CreatePurchaseHandler : AsyncRequestHandler<CreatePurchaseCommand>
    {
        public CreatePurchaseHandler(
            DbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        protected override async Task Handle(
            CreatePurchaseCommand request,
            CancellationToken cancellationToken)
        {
            var purchase = _mapper.Map<Purchase>(request.Dto);
            purchase.DateTime = DateTime.Now;
            purchase.RefundKey = Guid.NewGuid().ToString();
            await _context.AddAsync(purchase, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
