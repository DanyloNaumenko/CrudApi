using Microsoft.AspNetCore.Http.HttpResults;

namespace CrudApi.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next,  ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(new {message = "Something went wrong" });
        }
    }
}