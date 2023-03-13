using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.TableEntries.Commands.DeleteTableEntry;
public sealed record DeleteTableEntryCommand(int EntryId) : IRequest;

internal sealed class DeleteTableEntryHandler: AsyncRequestHandler<DeleteTableEntryCommand>
{
    private readonly DbContext _context;

    public DeleteTableEntryHandler(
        DbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(
        DeleteTableEntryCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExisting(request.EntryId, cancellationToken);
        if (!exists)
            throw new BadRequestException("Попытка удаления не существующей записи!");

        var entry = await GetEntry(request.EntryId, cancellationToken);
        _context.Remove(entry);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<TableEntry> GetEntry(int entryId, CancellationToken cancellationToken)
    {
        var entry = await _context
            .Set<TableEntry>()
            .SingleAsync(e => e.Id == entryId, cancellationToken);

        return entry;
    }

    private async Task<bool> IsExisting(
        int entryId,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<TableEntry>()
            .AnyAsync(e => e.Id == entryId, cancellationToken);

        return exists;
    }
}

