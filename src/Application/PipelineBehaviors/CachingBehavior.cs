using FluentPOS.Application.Abstractions.Queries;
using FluentPOS.Application.Abstractions.Serializations;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Application.PipelineBehaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private static readonly DistributedCacheEntryOptions _defaultCacheOptions = new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(1.0) };

        private readonly ISerializerService _serializer;

        private readonly IDistributedCache _cache;

        private readonly ILogger _logger;

        public CachingBehavior(IDistributedCache cache, ISerializerService serializer, ILogger<TResponse> logger)
        {
            this._cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this._serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is ICacheableQuery cacheableQuery)
            {
                TResponse response;
                async Task<TResponse> GetResponseAndAddToCache()
                {
                    response = await next();
                    var options = cacheableQuery.SlidingExpiration != null ? new DistributedCacheEntryOptions { SlidingExpiration = cacheableQuery.SlidingExpiration } : _defaultCacheOptions;
                    await _cache.SetAsync(cacheableQuery.CacheKey, _serializer.Serialize(response), options, cancellationToken);
                    return response;
                }

                var cachedResponse = await _cache.GetAsync(cacheableQuery.CacheKey, cancellationToken);
                if (cachedResponse != null)
                {
                    _logger.LogInformation($"Fetching from Cache for key -> '{cacheableQuery.CacheKey}'.");
                    response = _serializer.DeserializeBytes<TResponse>(cachedResponse);
                }
                else
                {
                    _logger.LogInformation($"Adding to Cache with key -> '{cacheableQuery.CacheKey}'.");
                    response = await GetResponseAndAddToCache();
                }

                return response;
            }
            else
            {
                return await next();
            }
        }
    }
}