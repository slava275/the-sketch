using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TheSketch.Domain.Exceptions;

namespace TheSketch.WebApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/problem+json";

        var statusCode = ex switch
        {
            EntityNotFoundException => StatusCodes.Status404NotFound,
            UserException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = ex switch
            {
                EntityNotFoundException => "Entity not found",
                ArgumentException => "Invalid argument",
                UserException => "Invalid user data",
                _ => "An unexpected error occurred"
            },
            Detail = ex.Message,
            Instance = context.Request.Path,
            Type = ex switch
            {
                EntityNotFoundException => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ArgumentException => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                UserException => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            }
        };

        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var payload = JsonSerializer.Serialize(problemDetails, jsonOptions);

        return context.Response.WriteAsync(payload);
    }
}
