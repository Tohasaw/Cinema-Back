using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Cinema.Data.Exceptions;

namespace Cinema.Data.Middlewares;

/// <summary>
///     Промежуточное програмное обеспечение для преобразования
///     содержимого исключений в приемлемый для пользователя вид
/// </summary>
public sealed class ExceptionMiddleware : IMiddleware
{
    /// <summary>
    ///     Метод обработки запроса
    /// </summary>
    /// <param name="context">Контекст запроса</param>
    /// <param name="next">Делегат перемещения в следующее промежуточное програмное обеспечение</param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (BadRequestException e)
        {
            await SetExceptionResult(
                context,
                e.StatusCode,
                e.Message);
        }
        catch (ForbiddenException e)
        {
            await SetExceptionResult(
                context,
                e.StatusCode,
                e.Message);
        }
        catch (NotFoundException e)
        {
            await SetExceptionResult(
                context,
                e.StatusCode,
                e.Message);
        }
        catch (Exception)
        {
            await SetExceptionResult(
                context,
                HttpStatusCode.InternalServerError,
                "Ошибка сервера");
        }
    }

    /// <summary>
    ///     Метод преобразования исключительной
    ///     ситуации в приемлемый для пользователя вид
    /// </summary>
    /// <param name="context">Контекст запроса</param>
    /// <param name="statusCode">Статус код запроса</param>
    /// <param name="message">Сообщение для пользователя</param>
    private static async Task SetExceptionResult(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.StatusCode = Convert.ToInt32(statusCode);
        context.Response.ContentType = "application/json";
        var result = JsonConvert.SerializeObject(new
        {
            context.Response.StatusCode,
            ErrorMessage = message
        });
        await context.Response.WriteAsync(result);
    }
}