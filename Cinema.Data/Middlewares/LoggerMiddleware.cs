using Microsoft.AspNetCore.Http;
using Serilog;

namespace Cinema.Data.Middlewares;

/// <summary>
///     Промежуточное програмное обеспечение для журналирования исключений
/// </summary>
public class LoggerMiddleware : IMiddleware
{
    /// <summary>
    ///     Логгер
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    ///     Конструктор промежуточного програмного
    ///     обеспечения для журналирования исключений
    /// </summary>
    /// <param name="logger">Логгер</param>
    public LoggerMiddleware(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Метод обработки запроса
    /// </summary>
    /// <param name="context">Контекст запроса</param>
    /// <param name="next">Делегат перемещения в следующее промежуточное програмное обеспечение</param>
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.Error(e.ToString());
            throw;
        }
    }
}