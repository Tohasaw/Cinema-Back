using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Prices.Commands.DeletePrice;
public sealed record DeletePriceCommand(int PriceId) : IRequest;

internal sealed class DeletePriceHandler : AsyncRequestHandler<DeletePriceCommand>
{
    private readonly DbContext _context;

    public DeletePriceHandler(
        DbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(
        DeletePriceCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsPriceAsync(request.PriceId, cancellationToken);
        if (!exists)
            throw new BadRequestException("Попытка удаления не существующей записи!");

        var note = await GetPriceAsync(request.PriceId, cancellationToken);

        _context.Remove(note);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> IsExistsPriceAsync(
        int PriceId,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<Price>()
            .AnyAsync(n => n.Id == PriceId, cancellationToken);

        return exists;
    }

    private async Task<Price> GetPriceAsync(
        int PriceId,
        CancellationToken cancellationToken)
    {
        var note = await _context
            .Set<Price>()
            .SingleAsync(n => n.Id == PriceId, cancellationToken);

        return note;
    }
}
