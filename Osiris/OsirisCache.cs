using Microsoft.Extensions.Caching.Memory;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris
{
    public class OsirisCache : ICacheIsKing
    {

        public IMemoryCache Cache { get; set; }



        public OsirisCache(IMemoryCache cache)
        {
            Cache = cache;
        }


        public T SetCache<T>(string key, T @object, int cacheDurationInSeconds)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(cacheDurationInSeconds));

           return Cache.Set(key, @object, cacheEntryOptions);
        }

        public bool TryGetCache<T>(string key, out T cache)
        {
            return Cache.TryGetValue(key, out cache);
        }
    }
}
