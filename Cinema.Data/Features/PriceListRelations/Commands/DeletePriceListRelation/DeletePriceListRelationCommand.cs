using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.PriceListRelations.Commands.DeletePriceListRelation;
public sealed record DeletePriceListRelationCommand(int PriceListRelationId) : IRequest;

internal sealed class DeletePriceListRelationHandler : AsyncRequestHandler<DeletePriceListRelationCommand>
{
    private readonly DbContext _context;

    public DeletePriceListRelationHandler(
        DbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(
        DeletePriceListRelationCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsPriceListRelationAsync(request.PriceListRelationId, cancellationToken);
        if (!exists)
            throw new BadRequestException("Попытка удаления не существующей записи!");

        var note = await GetPriceListRelationAsync(request.PriceListRelationId, cancellationToken);

        _context.Remove(note);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> IsExistsPriceListRelationAsync(
        int PriceListRelationId,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<PriceListRelation>()
            .AnyAsync(n => n.Id == PriceListRelationId, cancellationToken);

        return exists;
    }

    private async Task<PriceListRelation> GetPriceListRelationAsync(
        int PriceListRelationId,
        CancellationToken cancellationToken)
    {
        var note = await _context
            .Set<PriceListRelation>()
            .SingleAsync(n => n.Id == PriceListRelationId, cancellationToken);

        return note;
    }
}
