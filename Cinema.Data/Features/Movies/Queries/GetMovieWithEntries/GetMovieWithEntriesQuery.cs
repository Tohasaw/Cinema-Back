using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Movies.Queries.GetMovieWithEntries
{
    public sealed record GetMovieWithEntriesQuery(int MovieId) : IRequest<GetMovieWithEntriesDto>;
    internal sealed class GetMovieWithEntriesHandler : IRequestHandler<GetMovieWithEntriesQuery, GetMovieWithEntriesDto>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetMovieWithEntriesHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<GetMovieWithEntriesDto> Handle(
            GetMovieWithEntriesQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(request, cancellationToken);
            if (dto == null) throw new NotFoundException("Фильм не найден");
            return dto;
        }
        private async Task<GetMovieWithEntriesDto?> GetDtoAsync(
            GetMovieWithEntriesQuery request,
            CancellationToken cancellationToken)
        {
             return await _context
                .Set<Movie>()
                .Include(movie => movie.TableEntries)
                .ProjectTo<GetMovieWithEntriesDto>(_provider)
                .SingleOrDefaultAsync(movie => movie.Id == request.MovieId, cancellationToken);
        }
    }
}
