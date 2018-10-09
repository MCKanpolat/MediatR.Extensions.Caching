using Microsoft.Extensions.Caching.Distributed;

namespace MediatR.Extensions.Caching.Internal
{
    internal static class DistributedCacheEntryOptionsExtension
    {
        public static DistributedCacheEntryOptions Map<T>( this ICacheEntryOptions<T> source) where T : IBaseRequest
        {
            return new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = source.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = source.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = source.SlidingExpiration
            };
        }
    }
}
