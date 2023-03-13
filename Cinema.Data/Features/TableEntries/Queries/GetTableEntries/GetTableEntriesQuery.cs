using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.TableEntries.Queries.GetTableEntries
{
    public sealed record class GetTableEntriesQuery : IRequest<IEnumerable<GetTableEntriesDto>>;
    internal sealed class GetTableEntriesHandler : IRequestHandler<GetTableEntriesQuery, IEnumerable<GetTableEntriesDto>>
    {
        private readonly DbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetTableEntriesHandler(DbContext context, IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetTableEntriesDto>> Handle(
            GetTableEntriesQuery request,
            CancellationToken cancellationToken)
        {
            var dtos = await _context
                .Set<TableEntry>()
                .ProjectTo<GetTableEntriesDto>(_provider)
                .ToListAsync(cancellationToken);

            return dtos;
        }
    }
}
