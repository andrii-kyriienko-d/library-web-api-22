using System;
using System.Threading.Tasks;
using LibraryWebApi.Exceptions;
using Microsoft.AspNetCore.Http;

namespace LibraryWebApi.Middlewares;

internal sealed class BusinessExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public BusinessExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception exceptionObj)
        {
            await HandleExceptionAsync(context, exceptionObj);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, BusinessException exception)
    {
        var result = exception.ToString();
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        return context.Response.WriteAsync(result);
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var result = $"{exception.Message}\n\n{exception.StackTrace}\nAT\n{exception.Source}";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return context.Response.WriteAsync(result);
    }
}