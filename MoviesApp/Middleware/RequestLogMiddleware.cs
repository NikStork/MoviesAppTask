using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MoviesApp.Middleware
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;
        
        public RequestLogMiddleware(RequestDelegate next) => _next = next;
        
        public async Task Invoke(HttpContext httpContext, ILogger<RequestLogMiddleware> logger)
        {
            try
            {
                var onActors = httpContext.Request.Path.StartsWithSegments("/Actors");

                if (onActors)
                {
                    logger.LogTrace($"Request: {httpContext.Request.Path}  Method: {httpContext.Request.Method}");
                    
                    if (httpContext.Request.Method == HttpMethod.Post.Method)
                    {
                        var form = httpContext.Request.Form;
                        
                        foreach (var obj in form)
                        {
                            logger.LogTrace($"Key obj: {obj.Key} -> Value obj: {obj.Value}");
                        }
                    }
                }
                
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        // Обрабатываем исключения
        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            
            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.ValidationResult);
                    break;
                case DllNotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;

            if (result == String.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }

            return httpContext.Response.WriteAsync(result);
        }
    }
}