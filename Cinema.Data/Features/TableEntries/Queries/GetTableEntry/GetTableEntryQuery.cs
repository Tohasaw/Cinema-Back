using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.TableEntries.Queries.GetTableEntry
{
    public sealed record GetTableEntryQuery(int EntryId) : IRequest<GetTableEntryDto>;

    internal sealed class GetTableEntryHandler : IRequestHandler<GetTableEntryQuery, GetTableEntryDto>
    {
        private readonly DbContext _context;
        private readonly IConfigurationProvider _provider;

        public GetTableEntryHandler(
            DbContext context, 
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }

        public async Task<GetTableEntryDto> Handle(
            GetTableEntryQuery request, 
            CancellationToken cancellationToken)
        {
            var exists = await IsExistsEntryAsync(
                request.EntryId,
                cancellationToken);
            if (!exists)
                throw new NotFoundException("Запись не найдена");

            var dto = await GetDtoAsync(
                request.EntryId,
                cancellationToken);

            return dto;
        }

        private async Task<bool> IsExistsEntryAsync(
        int entryId,
        CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<TableEntry>()
                .AnyAsync(entry => entry.Id == entryId, cancellationToken);

            return exists;
        }

        private async Task<GetTableEntryDto> GetDtoAsync(
            int entryId,
            CancellationToken cancellationToken)
        {
            return await _context
                .Set<TableEntry>()
                .Where(entry => entry.Id == entryId)
                .ProjectTo<GetTableEntryDto>(_provider)
                .SingleAsync(cancellationToken);
        }
    }
}
