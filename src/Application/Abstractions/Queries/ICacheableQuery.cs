using System;

namespace FluentPOS.Application.Abstractions.Queries
{
    public interface ICacheableQuery
    {
        bool BypassCache { get; }
        string CacheKey { get; }
        TimeSpan? SlidingExpiration { get; }
    }
}