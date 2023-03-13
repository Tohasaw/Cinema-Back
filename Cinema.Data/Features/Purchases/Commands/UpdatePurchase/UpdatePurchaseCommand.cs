using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using Cinema.Data.Services.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Purchases.Commands.UpdatePurchase;
public sealed record UpdatePurchaseCommand(int PurchaseId, UpdatePurchaseDto Dto) : IRequest;

internal sealed class UpdatePurchaseHandle : AsyncRequestHandler<UpdatePurchaseCommand>
{
    private readonly IMapper _mapper;
    private readonly DbContext _context;

    public UpdatePurchaseHandle(
        IMapper mapper,
        DbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    protected override async Task Handle(
        UpdatePurchaseCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExisting(
            request.PurchaseId,
            cancellationToken);

        if (!exists)
            throw new BadRequestException("Попытка обновить несуществующую запись");

        var purchase = await GetPurchase(
            request.PurchaseId,
            cancellationToken);
        _mapper.Map(request.Dto, purchase);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Purchase> GetPurchase(int purchaseId, CancellationToken cancellationToken)
    {
        var ticket = await _context
            .Set<Purchase>()
            .SingleAsync(t => t.Id == purchaseId, cancellationToken);

        return ticket;
    }

    private async Task<bool> IsExisting(int purchaseId, CancellationToken cancellationToken)
    {
        var exist = await _context
            .Set<Purchase>()
            .AnyAsync(t => t.Id == purchaseId, cancellationToken);

        return exist;
    }
}

