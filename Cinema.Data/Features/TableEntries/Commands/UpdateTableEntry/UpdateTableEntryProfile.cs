using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.TableEntries.Commands.UpdateTableEntry;
internal sealed class UpdateTableEntryProfile : Profile
{
    public UpdateTableEntryProfile()
    {
        CreateMap<UpdateTableEntryDto, TableEntry>();
    }
}

