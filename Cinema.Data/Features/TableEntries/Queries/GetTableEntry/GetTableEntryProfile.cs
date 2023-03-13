using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.TableEntries.Queries.GetTableEntry
{
    internal sealed class GetTableEntryProfile : Profile
    {
        public GetTableEntryProfile()
        {
            CreateMap<TableEntry, GetTableEntryDto>();
        }
    }
}
