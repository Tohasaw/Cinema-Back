using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Movies.Queries.GetMovies
{
    public sealed record GetMoviesQuery : 
        IRequest<IEnumerable<GetMoviesDto>>;

    public sealed class GetMoviesHandler : IRequestHandler<GetMoviesQuery, IEnumerable<GetMoviesDto>>
    {
        private readonly DbContext _context;
        private readonly IConfigurationProvider _provider;

        public GetMoviesHandler(
            DbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }

        public async Task<IEnumerable<GetMoviesDto>> Handle(
            GetMoviesQuery request, 
            CancellationToken cancellationToken)
        {
            var dtos = await _context
                .Set<Movie>()
                .OrderByDescending(m => m.Id)
                .ProjectTo<GetMoviesDto>(_provider)
                .ToListAsync(cancellationToken);

            return dtos;
        }
    }
}
