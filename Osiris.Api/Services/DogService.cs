using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Osiris.DogApi;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Osiris.Api.DogApi
{
    public class DogService : ApiBase, IApiBase, IDogService
    {

        ILogger<DogService> _logger;

        public DogService(IHttpClientFactory factory, IOptions<DogApiSettings> settings, ICacheIsKing cache, ILogger<DogService> logger) : base(factory, settings)
        {
            Cache = cache;
            _logger = logger;
        }

        public ICacheIsKing Cache { get; set; }

        public async Task<ApiResult<List<Breed>>> GetAllBreeds()
        {
            string url = $"{DbSettings.Value.urls.breeds}";

            ApiResult<List<Breed>> cacheEntry;

            if (Cache.TryGetCache(url, out cacheEntry))
                cacheEntry.IsCachedData = true;
            else
                cacheEntry = await GetAndCacheDataAync<List<Breed>>(url, DbSettings.Value.cache_stratergies.breeds);

            return cacheEntry;
        }

        private async Task<ApiResult<T>> GetAndCacheDataAync<T>(string url,int cacheDuration)
        {
            _logger.LogInformation($"Caching data for {url}");

            ApiResult<T> cacheEntry = await Get<T>(url);

            return Cache.SetCache(url, cacheEntry, cacheDuration);
             
        }



        public async Task<ApiResult<List<Breed>>> GetBreedById(string breedId)
        {
            string url = $"{DbSettings.Value.urls.breeds}";

            if (!string.IsNullOrEmpty(breedId))
                url = $"{url}/search?breed_ids={breedId}";

            ApiResult<List<Breed>> cacheEntry;

            if (Cache.TryGetCache(url, out cacheEntry))
                cacheEntry.IsCachedData = true;
            else
                cacheEntry = await GetAndCacheDataAync<List<Breed>>(url, DbSettings.Value.cache_stratergies.images);

            return cacheEntry;
        }
        public async Task<ApiResult<List<Breed>>> GetBreedByName(string breedName)
        {
            string url = $"{DbSettings.Value.urls.breeds}";

            if (!string.IsNullOrEmpty(breedName))
                url = $"{url}/search?q={breedName}";

            ApiResult<List<Breed>> cacheEntry;

            if (Cache.TryGetCache(url, out cacheEntry))
                cacheEntry.IsCachedData = true;
            else
                cacheEntry = await GetAndCacheDataAync<List<Breed>>(url, DbSettings.Value.cache_stratergies.images);

            return cacheEntry;
        }

        public async Task<ApiResult<List<Image>>> GetDogByBreedId(int breedId)
        {
            string url = $"{DbSettings.Value.urls.images}";

            url = $"{url}/search?breed_id={breedId}";

            ApiResult<List<Image>> cacheEntry;

            if (Cache.TryGetCache(url, out cacheEntry))
                cacheEntry.IsCachedData = true;
            else
                cacheEntry = await GetAndCacheDataAync<List<Image>>(url, DbSettings.Value.cache_stratergies.images);

            return cacheEntry;
        }

        public async Task<ApiResult<List<Image>>> GetRandomDog()
        {
            // No caching for random dogs

            string url = $"{DbSettings.Value.urls.images}";

            url = $"{url}/search";

            var r = await Get<List<Image>>(url);

            return r;
        }

        public async Task<ApiResult<List<Image>>> SearchAllImages(string list)
        {

            var ids = list.Split(',').ToList();
            ApiResult<List<Image>> temp;
            ApiResult<List<Image>> result = new ApiResult<List<Image>>() { Results = new List<Image> { } };

            foreach (string id in ids)
            {
                temp = await GetDogByBreedId(int.Parse(id));
                result.IsValid = temp.IsValid;
                result.Results.AddRange(temp.Results);
                result.IsCachedData = temp.IsCachedData;
            }

            return result;
        }
    }
}
