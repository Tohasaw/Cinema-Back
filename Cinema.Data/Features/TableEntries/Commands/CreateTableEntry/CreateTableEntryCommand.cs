using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using Cinema.Data.Services.JwtTokenServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.TableEntries.Commands.CreateTableEntry
{
    public sealed record CreateTableEntryCommand(CreateTableEntryDto Dto) : IRequest<int>;

    public sealed class CreateTableEntryHandler : IRequestHandler<CreateTableEntryCommand, int>
    {
        public CreateTableEntryHandler(
            DbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public async Task<int> Handle(
            CreateTableEntryCommand request,
            CancellationToken cancellationToken)
        {
            var tableEntry = _mapper.Map<TableEntry>(request.Dto);

            var exists = await IsExistsEntryAsync(
                tableEntry.DateTime,
                cancellationToken);
            if (exists)
                throw new BadRequestException("Запись в расписании на выбранное время уже существует");

            await _context.AddAsync(tableEntry, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return tableEntry.Id;
        }
        private async Task<bool> IsExistsEntryAsync(
            DateTime dateTime,
            CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<TableEntry>()
                .AsNoTracking()
                .AnyAsync(u => u.DateTime == dateTime, cancellationToken);

            return exists;
        }
    }
}
