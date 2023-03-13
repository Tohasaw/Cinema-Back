using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Features.Movies.Queries.GetMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.TableEntries.Queries.GetTableEntriesMovies
{
    internal sealed class GetTableEntriesMoviesProfile : Profile
    {
        public GetTableEntriesMoviesProfile()
        {
            CreateMap<TableEntry, GetTableEntriesMoviesDto>();
        }
    }
}
