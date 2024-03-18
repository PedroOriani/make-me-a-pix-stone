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
            NotFoundException _ => new ExceptionResponse(HttpStatusCode.NotFound, exception.Message),
            UnavailableKeyException _ => new ExceptionResponse(HttpStatusCode.Conflict, exception.Message),
            CpfDifferentException _ => new ExceptionResponse(HttpStatusCode.Conflict, exception.Message),
            InvalidTypeException _ => new ExceptionResponse(HttpStatusCode.BadRequest, exception.Message),
            InvalidFormatException _ => new ExceptionResponse(HttpStatusCode.BadRequest, exception.Message),
            InvalidToken _ => new ExceptionResponse(HttpStatusCode.Unauthorized, exception.Message),
            LimitExceededException _ => new ExceptionResponse(HttpStatusCode.BadRequest, exception.Message),
            RecentPaymentException _ => new ExceptionResponse(HttpStatusCode.BadRequest, exception.Message),
            _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        context.Response.WriteAsJsonAsync(response);
    }

}

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);