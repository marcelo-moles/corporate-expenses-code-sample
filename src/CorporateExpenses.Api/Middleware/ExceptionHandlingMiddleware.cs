using System.Net;
using System.Text.Json;

namespace CorporateExpenses.Api.Middleware;

public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Unhandled exception occurred.");

            await HandleExceptionAsync(
                context,
                exception);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode =
            exception switch
            {
                ArgumentException =>
                    (int)HttpStatusCode.BadRequest,

                UnauthorizedAccessException =>
                    (int)HttpStatusCode.Unauthorized,

                _ =>
                    (int)HttpStatusCode.InternalServerError
            };

        var response = new
        {
            statusCode = context.Response.StatusCode,
            message = context.Response.StatusCode ==
                      (int)HttpStatusCode.InternalServerError
                ? "An unexpected error occurred."
                : exception.Message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}