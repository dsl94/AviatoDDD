using System.Net;
using AviatoDDD.Domain.DTO;
using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Exceptions;

namespace AviatoDDD.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (EntityNotFoundException e)
        {
            HandleNotFoundException(e, httpContext);
        }
        catch (Exception e)
        {
            HandleGeneralException(e, httpContext);
        }
    }

    private async void HandleNotFoundException(EntityNotFoundException e, HttpContext httpContext)
    {
        _logger.LogError(e.Message);
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = 404;
        var errorResponse = new ErrorResponse() { ErrorCode = e.ErrorCode, ErrorMessage = e.Message };
        await httpContext.Response.WriteAsJsonAsync(errorResponse);
    }

    private async void HandleGeneralException(Exception e, HttpContext httpContext)
    {
        _logger.LogError(e.Message);
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = 500;
        var errorResponse = new ErrorResponse() { ErrorCode = ErrorCode.GeneralError, ErrorMessage = e.Message };
        await httpContext.Response.WriteAsJsonAsync(errorResponse);

    }
}