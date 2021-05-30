using System;

namespace FluentPOS.Application.Abstractions.Queries
{
    public interface ICacheableQuery
    {
        bool BypassCache { get; }
        string CacheKey { get; set; }
        TimeSpan? SlidingExpiration { get; }
    }
}
