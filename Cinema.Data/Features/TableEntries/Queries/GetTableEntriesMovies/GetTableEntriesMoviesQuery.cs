using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.TableEntries.Queries.GetTableEntriesMovies
{
    public sealed record GetTableEntriesMoviesQuery : IRequest<IEnumerable<GetTableEntriesMoviesDto>>;
    internal sealed class GetTableEntriesMovieHandler : IRequestHandler<GetTableEntriesMoviesQuery, IEnumerable<GetTableEntriesMoviesDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetTableEntriesMovieHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetTableEntriesMoviesDto>> Handle(
            GetTableEntriesMoviesQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(cancellationToken);
            var seatCount = await _context.Set<Seat>().CountAsync(cancellationToken);
            foreach (var entry in dto)
            {
                if (entry.DateTime < DateTime.Now)
                {
                    var visitCount = await _context.Set<Ticket>().Include(x => x.Purchase).CountAsync(x => x.Purchase.TableEntryId == entry.Id && x.Visited == true, cancellationToken);
                    entry.Visit = visitCount;
                    entry.VisitPercent = ((float) visitCount / seatCount * 100).ToString("0.0");
                }
            }
            return dto;
        }
        private async Task<IEnumerable<GetTableEntriesMoviesDto>> GetDtoAsync(
            CancellationToken cancellationToken)
        {
             return await _context
                .Set<TableEntry>()
                .Include(e => e.Movie)
                .Include(e => e.PriceList)
                .OrderByDescending(e => e.DateTime)
                .ProjectTo<GetTableEntriesMoviesDto>(_provider)
                .ToListAsync(cancellationToken);
        }
    }
}
