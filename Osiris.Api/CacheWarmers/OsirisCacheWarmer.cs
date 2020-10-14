using Osiris.DogApi;
using Osiris.Interfaces;
using Osiris;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Osiris.Api.CacheWarmers
{
    public class OsirisCacheWarmer : IOsirisCacheWarmer
    {
        ILogger<OsirisCacheWarmer> _logger;

        List<Osiris.CacheWarmer> _cacheWarmers;
        public OsirisCacheWarmer(IDogsApiCacheWarmer dogsCache, ILogger<OsirisCacheWarmer> logger)
        {
            _cacheWarmers = new List<Osiris.CacheWarmer> { };
            _cacheWarmers.Add((Osiris.CacheWarmer)dogsCache);
            _logger = logger;
        }

        public async void WarmAllCache()
        {
            _logger.LogWarning("Warming ALL Cache Starting...");

            bool result;

            foreach (Osiris.CacheWarmer cacheWarmer in this._cacheWarmers)
            {
                result = await cacheWarmer.WarmCache();
                _logger.LogInformation($"");
            }

            _logger.LogWarning("Warming ALL Cache DONE");

        }
    }
}
