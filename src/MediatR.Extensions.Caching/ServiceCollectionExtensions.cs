using MediatR;
using MediatR.Extensions.Caching;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRCaching(this IServiceCollection services)
        {
            services.AddTransient(typeof(CachingPipelineBehavior<,>), typeof(IPipelineBehavior<,>));
            return services;
        }

        public static IServiceCollection AddMediatRCaching(this IServiceCollection services, IEnumerable<ICacheEntryOptions<IBaseRequest>> cacheEntryOptions)
        {
            if (cacheEntryOptions == null)
            {
                throw new ArgumentNullException(nameof(cacheEntryOptions));
            }

            services.AddTransient(typeof(CachingPipelineBehavior<,>), typeof(IPipelineBehavior<,>));
            foreach (var cacheEntryOption in cacheEntryOptions)
            {
                services.AddSingleton(cacheEntryOption.GetType());
            }
            return services;
        }
    }
}
