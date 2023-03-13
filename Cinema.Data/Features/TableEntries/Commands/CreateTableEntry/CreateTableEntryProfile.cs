using AutoMapper;
using Cinema.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.TableEntries.Commands.CreateTableEntry
{
    internal sealed class CreateTableEntryProfile : Profile
    {
        public CreateTableEntryProfile()
        {
            CreateMap<CreateTableEntryDto, TableEntry>();
        }
    }
}
