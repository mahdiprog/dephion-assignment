using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Triskele.Common.Application.Exceptions;

namespace Common.Web
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;
            if (exception is RequestException)
                exception = exception.InnerException;
            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Failures);
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;
                case ConflictException conflictException:
                    code = HttpStatusCode.Conflict;
                    result = conflictException.Message;
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    result = notFoundException.Message;
                    break;
            }

            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = (int)code;


            if (result == string.Empty)
            {
                Exception exc = null;
#if(DEBUG)
                exc = exception;
#endif
                var errorObject = new {Message=exception.Message, Detail = exc};

                result = JsonSerializer.Serialize(errorObject);
            }
            
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
