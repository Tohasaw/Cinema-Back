using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.TableEntries.Commands.DeletePurchase;
public sealed record DeletePurchaseCommand(int PurchaseId) : IRequest;

internal sealed class DeletePurchaseHandler: AsyncRequestHandler<DeletePurchaseCommand>
{
    private readonly DbContext _context;

    public DeletePurchaseHandler(
        DbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(
        DeletePurchaseCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExisting(request.PurchaseId, cancellationToken);
        if (!exists)
            throw new BadRequestException("Попытка удаления не существующей записи!");

        var purchase = await GetEntry(request.PurchaseId, cancellationToken);
        _context.Remove(purchase);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Purchase> GetEntry(int purchaseId, CancellationToken cancellationToken)
    {
        var purchase = await _context
            .Set<Purchase>()
            .SingleAsync(e => e.Id == purchaseId, cancellationToken);

        return purchase;
    }

    private async Task<bool> IsExisting(
        int purchaseId,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<Purchase>()
            .AnyAsync(e => e.Id == purchaseId, cancellationToken);

        return exists;
    }
}

