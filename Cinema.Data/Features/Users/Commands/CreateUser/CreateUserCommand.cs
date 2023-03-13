using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;

namespace Cinema.Data.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand(CreateUserDto Dto) : IRequest<Unit>;

internal sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, Unit>
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public CreateUserHandler(
        DbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.Dto);
        user.RoleId = 2; // role: id = 2, name = user
        if (request.Dto.Email == null || request.Dto.Password == null)
            throw new BadRequestException("Данные для регистрации не введены");
        var exists = await IsExistsUserEmailAsync(
            user.Email!,
            cancellationToken);
        if (exists)
            throw new BadRequestException("Аккаунт с введенным адресом почты уже существует");

        await _context.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<bool> IsExistsUserEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<User>()
            .AsNoTracking()
            .AnyAsync(u => u.Email == email, cancellationToken);

        return exists;
    }
}