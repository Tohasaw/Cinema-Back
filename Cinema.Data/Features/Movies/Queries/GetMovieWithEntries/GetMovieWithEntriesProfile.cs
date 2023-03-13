using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Features.Movies.Queries.GetMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Movies.Queries.GetMovieWithEntries
{
    internal sealed class GetMovieWithEntriesProfile : Profile
    {
        public GetMovieWithEntriesProfile()
        {
            var dateTime = DateTime.Now.AddDays(7).Date;
            var dateNow = DateTime.Today.Date;
            CreateMap<Movie, GetMovieWithEntriesDto>()
                .ForMember(dest => dest.TableEntries, src => src.MapFrom(src => src.TableEntries.Where(e => e.DateTime > dateNow && e.DateTime < dateTime)));
            CreateMap<TableEntry, GetTableEntryDto>();
        }
    }
}
