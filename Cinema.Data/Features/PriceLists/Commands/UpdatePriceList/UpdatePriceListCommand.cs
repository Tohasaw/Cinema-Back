using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using Cinema.Data.Services.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.PriceLists.Commands.UpdatePriceList;
public sealed record UpdatePriceListCommand(int PriceListId, UpdatePriceListDto Dto) : IRequest;

internal sealed class UpdatePriceListHandle : AsyncRequestHandler<UpdatePriceListCommand>
{
    private readonly IMapper _mapper;
    private readonly DbContext _context;
    private readonly CurrentUserService _userService;

    public UpdatePriceListHandle(
        IMapper mapper,
        DbContext context,
        CurrentUserService userService)
    {
        _mapper = mapper;
        _context = context;
        _userService = userService;
    }
    protected override async Task Handle(
        UpdatePriceListCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExisting(
            request.PriceListId,
            cancellationToken);

        if (!exists)
            throw new BadRequestException("Попытка обновить несуществующую запись");

        var priceList = await GetPriceList(
            request.PriceListId,
            cancellationToken);

        _mapper.Map(request.Dto, priceList);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<PriceList> GetPriceList(int priceListId, CancellationToken cancellationToken)
    {
        var ticket = await _context
            .Set<PriceList>()
            .SingleAsync(t => t.Id == priceListId, cancellationToken);

        return ticket;
    }

    private async Task<bool> IsExisting(int priceListId, CancellationToken cancellationToken)
    {
        var exist = await _context
            .Set<PriceList>()
            .AnyAsync(t => t.Id == priceListId, cancellationToken);

        return exist;
    }
}

