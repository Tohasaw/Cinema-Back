using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Middlewares;

/// <summary>
/// Промежуточное программное обеспечение для создания единой транзакции запроса
/// </summary>
public sealed class TransactionMiddleware : IMiddleware
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    private readonly DbContext _context;

    /// <summary>
    /// Конструктор промежуточного программного обеспечения для создания единой транзакции запроса
    /// </summary>
    /// <param name="context"></param>
    public TransactionMiddleware(DbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Мотод обработки запроса
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    /// <param name="next">Делегат перемещения в следующее промежуточное програмное обеспечение</param>
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await next(context);
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}