using Microsoft.Extensions.Caching.Memory;
using Shopi.Application.Exceptions;
using Shopi.Resources;

namespace Shopi.WebApi.Middlewares;

public class RateLimitMiddleware(RequestDelegate next,IMemoryCache memoryCache)
{
    private readonly TimeSpan timeSpan = TimeSpan.FromMinutes(1);
    private readonly int limit = 10;

    public async Task Invoke(HttpContext context)
    {
        var key = context.Connection.RemoteIpAddress!.ToString();

        memoryCache.TryGetValue(key, out int requestCount);

        if (requestCount > limit)
        {
            throw new TooManyRequestException(Messages.TooManyRequest);
        }
        else
        {
            requestCount++;
            memoryCache.Set(key, requestCount, timeSpan);

            await next(context);
        }
    }
}
