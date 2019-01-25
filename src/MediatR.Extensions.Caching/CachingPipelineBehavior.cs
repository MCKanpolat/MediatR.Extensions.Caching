using MediatR.Extensions.Caching.Internal;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Extensions.Caching
{
    public class CachingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseRequest
        where TResponse : new()
    {
        private readonly ICacheEntryOptions<TRequest> _cachableModelConfig;
        private readonly IDistributedCache _distributedCache;
        private readonly ISerializer _serializer;

        public CachingPipelineBehavior(ICacheEntryOptions<TRequest> cachableModelConfig, IDistributedCache distributedCache, ISerializer serializer)
        {
            _cachableModelConfig = cachableModelConfig;
            _distributedCache = distributedCache;
            _serializer = serializer;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_cachableModelConfig == null)
            {
                return await next();
            }
            else
            {
                TResponse response = default(TResponse);
                var cachedData = await _distributedCache.GetAsync(_cachableModelConfig.CacheKey);
                if (cachedData == null)
                {
                    await next().ContinueWith(async (t) =>
                    {
                        var realData = await t;
                        var serializedData = _serializer.Serialize(realData);
                        await _distributedCache.SetAsync(_cachableModelConfig.CacheKey, serializedData, _cachableModelConfig.Map());
                        response = realData;
                    });
                }
                else
                {
                    response = _serializer.Deserialize<TResponse>(cachedData);
                }
                return await new Task<TResponse>(() => response);
            }
        }
    }
}