using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.TableEntries.Queries.GetTableEntries
{
    internal sealed class GetTableEntriesProfile : Profile
    {
        public GetTableEntriesProfile()
        {
            CreateMap<TableEntry, GetTableEntriesDto>();
        }
    }
}
