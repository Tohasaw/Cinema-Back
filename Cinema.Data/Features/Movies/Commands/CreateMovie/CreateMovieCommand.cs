using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using Cinema.Data.Services.JwtTokenServices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Movies.Commands.CreateMovie
{
    public sealed record CreateMovieCommand(CreateMovieDto Dto) : IRequest<int>;
    public sealed class CreateMovieHandler : IRequestHandler<CreateMovieCommand, int>
    {
        private readonly DbContext _context;
        private readonly JwtSecurityTokenService _jwtSecurityTokenService;
        private readonly IMapper _mapper;
        public CreateMovieHandler(
            DbContext context,
            JwtSecurityTokenService jwtSecurityTokenService,
            IMapper mapper)
        {
            _context = context;
            _jwtSecurityTokenService = jwtSecurityTokenService;
            _mapper = mapper;
        }

        public async Task<int> Handle(
            CreateMovieCommand request, 
            CancellationToken cancellationToken)
        {
            var movie = _mapper.Map<Movie>(request.Dto);

            var exists = await IsExistsMovieAsync(
                movie.Title,
                cancellationToken);
            if (exists)
                throw new BadRequestException("Фильм с данным названием уже существует");

            await _context.AddAsync(movie, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return movie.Id;
        }
        private async Task<bool> IsExistsMovieAsync(
            string title,
            CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<Movie>()
                .AsNoTracking()
                .AnyAsync(u => u.Title == title, cancellationToken);

            return exists;
        }
    }
}
