using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Osiris.DogApi;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using Osiris;
using System.Net.Http;
using System.Threading.Tasks;

namespace Osiris.Api.CacheWarmer
{
    public class DogsApiCacheWarmer : Osiris.CacheWarmer, IDogsApiCacheWarmer
    {
        public IOptions<DogApiSettings> Settings { get; set; }

        ILogger<DogsApiCacheWarmer> _logger;

        public DogsApiCacheWarmer(IHttpClientFactory client, IOptions<DogApiSettings> settings,ILogger<DogsApiCacheWarmer>logger)
        {
            Settings = settings;
            _logger = logger;
        }


        public async Task<bool> CacheAlBreeds()
        {
            _logger.LogInformation("Caching ALL breeds");
            await Task.Delay(1500);

            return await Task.FromResult(true);
        }

        public async Task<bool> CacheAllImages()
        {
            _logger.LogInformation("Caching ALL images");
            await Task.Delay(1500);

            return await Task.FromResult(true);
        }

        public async Task<bool> CachePirorityBreeds(List<int> priorityList)
        {
            _logger.LogInformation("Caching priority breeds");
            await Task.Delay(2500);

            return await Task.FromResult(true);
        }

        public async Task<bool> CachePirorityImages(List<int> priorityList)
        {
            _logger.LogInformation("Caching priority images");
            await Task.Delay(3500);

            return await Task.FromResult(true);

        }

      

        public async override Task<bool> WarmCache()
        {

            await CachePirorityBreeds(Settings.Value.cache_warmers.breeds);
            await CachePirorityImages(Settings.Value.cache_warmers.images);
            await CacheAlBreeds();
            await CacheAllImages();

            return await Task.FromResult(true);
        }
    }
}
