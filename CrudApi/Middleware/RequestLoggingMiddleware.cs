using System.Diagnostics;
using CrudApi.Services;

namespace CrudApi.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task Invoke(HttpContext httpContext)
    {
        logger.LogInformation(
            "Method: {Method}; Path: {Path}",  
            httpContext.Request.Method, 
            httpContext.Request.Path);
        
        var sw = Stopwatch.StartNew();
        await next(httpContext);
        sw.Stop();
        logger.LogInformation( 
            "Status: {StatusCode}; Duration: {Elapsed} ms",
            httpContext.Response.StatusCode,
            sw.ElapsedMilliseconds);
    }
}