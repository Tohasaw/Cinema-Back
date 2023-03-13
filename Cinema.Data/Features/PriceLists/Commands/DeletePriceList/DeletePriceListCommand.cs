using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.PriceLists.Commands.DeletePriceList;
public sealed record DeletePriceListCommand(int PriceListId) : IRequest;

internal sealed class DeletePriceListHandler : AsyncRequestHandler<DeletePriceListCommand>
{
    private readonly DbContext _context;

    public DeletePriceListHandler(
        DbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(
        DeletePriceListCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsPriceListAsync(request.PriceListId, cancellationToken);
        if (!exists)
            throw new BadRequestException("Попытка удаления не существующего фильма!");

        var note = await GetPriceListAsync(request.PriceListId, cancellationToken);

        _context.Remove(note);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> IsExistsPriceListAsync(
        int PriceListId,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<PriceList>()
            .AnyAsync(n => n.Id == PriceListId, cancellationToken);

        return exists;
    }

    private async Task<PriceList> GetPriceListAsync(
        int PriceListId,
        CancellationToken cancellationToken)
    {
        var note = await _context
            .Set<PriceList>()
            .SingleAsync(n => n.Id == PriceListId, cancellationToken);

        return note;
    }
}
