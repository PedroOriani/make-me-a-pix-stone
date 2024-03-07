using System.Net;
using Pix.Exceptions;

namespace Pix.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            HandleException(context, ex);
        }
    }

    private static void HandleException(HttpContext context, Exception exception)
    {
        ExceptionResponse response = exception switch
        {
            EmailAlreadyExistsException _ => new ExceptionResponse(HttpStatusCode.Conflict, exception.Message),
            _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        context.Response.WriteAsJsonAsync(response);
    }

}

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);