using System.Net;
using System.Text.Json;
using RDS.Core.Responses;

namespace RDS.Server.Libraries.Middlewares;

/// <summary>
/// Middleware para capturar exceções globais e retornar uma resposta JSON amigável.
/// </summary>
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro interno não tratado: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = Response<object>.ErrorResponse("Ocorreu um erro interno no servidor.");

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}