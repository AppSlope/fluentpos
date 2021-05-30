using FluentPOS.Application.Abstractions.Queries;
using FluentPOS.Application.Abstractions.Serializations;
using FluentPOS.Application.Settings;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Application.PipelineBehaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ISerializerService _serializer;
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;
        private readonly CacheSettings _settings;
        public CachingBehavior(IDistributedCache cache, ISerializerService serializer, ILogger<TResponse> logger, IOptions<CacheSettings> settings)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is ICacheableQuery cacheableQuery)
            {
                TResponse response;
                if (cacheableQuery.BypassCache) return await next();
                async Task<TResponse> GetResponseAndAddToCache()
                {
                    response = await next();
                    var slidingExpiration = cacheableQuery.SlidingExpiration == null ? TimeSpan.FromHours(_settings.SlidingExpiration) : cacheableQuery.SlidingExpiration;
                    var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };
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