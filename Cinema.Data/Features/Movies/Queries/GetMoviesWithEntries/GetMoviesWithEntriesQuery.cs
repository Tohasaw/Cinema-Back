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

namespace Cinema.Data.Features.Movies.Queries.GetMoviesWithEntries
{
    public sealed record GetMoviesWithEntriesQuery : IRequest<IEnumerable<GetMoviesWithEntriesDto>>;
    internal sealed class GetMoviesWithEntriesHandler : IRequestHandler<GetMoviesWithEntriesQuery, IEnumerable<GetMoviesWithEntriesDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetMoviesWithEntriesHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetMoviesWithEntriesDto>> Handle(
            GetMoviesWithEntriesQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(cancellationToken);
            return dto;
        }
        private async Task<IEnumerable<GetMoviesWithEntriesDto>> GetDtoAsync(
            CancellationToken cancellationToken)
        {
             return await _context
                .Set<Movie>()
                .Include(movie => movie.TableEntries)
                .ProjectTo<GetMoviesWithEntriesDto>(_provider)
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
