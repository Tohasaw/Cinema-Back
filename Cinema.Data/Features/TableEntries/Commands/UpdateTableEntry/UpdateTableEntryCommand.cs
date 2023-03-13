using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.TableEntries.Commands.UpdateTableEntry;
public sealed record UpdateTableEntryCommand(int EntryId, UpdateTableEntryDto Dto) : IRequest;

internal sealed class UpdateTableEntryHandle : AsyncRequestHandler<UpdateTableEntryCommand>
{
    private readonly IMapper _mapper;
    private readonly DbContext _context;

    public UpdateTableEntryHandle(IMapper mapper, DbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    protected override async Task Handle(
        UpdateTableEntryCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExisting(
            request.EntryId, 
            cancellationToken);

        if (!exists)
            throw new BadRequestException("Попытка обновить несуществующую запись");

        var entry = await GetEntry(
            request.EntryId, 
            cancellationToken);

        _mapper.Map(request.Dto, entry);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<TableEntry> GetEntry(int entryId, CancellationToken cancellationToken)
    {
        var entry = await _context
            .Set<TableEntry>()
            .SingleAsync(e => e.Id == entryId, cancellationToken);

        return entry;
    }

    private async Task<bool> IsExisting(int entryId, CancellationToken cancellationToken)
    {
        var exist = await _context
            .Set<TableEntry>()
            .AnyAsync(e => e.Id == entryId, cancellationToken);

        return exist;
    }
}

