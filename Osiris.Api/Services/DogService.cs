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
    public class DogService : IDogService
    {

        IHttpRestClient _restClient;
        ILogger<DogService> _logger;
        IOptions<DogApiSettings> _settings;

        public DogService(IHttpRestClient restClient, ILogger<DogService> logger, IOptions<DogApiSettings> settings, ICacheIsKing cache)
        {
            _restClient = restClient;
            _logger = logger;
            _settings = settings;
            Cache = cache;
        }


        public ICacheIsKing Cache { get; set; }

        public async Task<ApiResult<List<Breed>>> GetAllBreeds()
        {
            string url = $"{_settings.Value.urls.breeds}";

            ApiResult<List<Breed>> cacheEntry;

            if (Cache.TryGetCache(url, out cacheEntry))
                cacheEntry.IsCachedData = true;
            else
                cacheEntry = await GetAndCacheDataAync<List<Breed>>(url, _settings.Value.cache_stratergies.breeds);

            return cacheEntry;
        }

        private async Task<ApiResult<T>> GetAndCacheDataAync<T>(string url,int cacheDuration)
        {
            _logger.LogInformation($"Caching data for {url}");

            ApiResult<T> cacheEntry = await _restClient.GetAsync<T>(url);

            return Cache.SetCache(url, cacheEntry, cacheDuration);
             
        }



        public async Task<ApiResult<List<Breed>>> GetBreedById(string breedId)
        {
            string url = $"{_settings.Value.urls.breeds}";

            if (!string.IsNullOrEmpty(breedId))
                url = $"{url}/search?breed_ids={breedId}";

            ApiResult<List<Breed>> cacheEntry;

            if (Cache.TryGetCache(url, out cacheEntry))
                cacheEntry.IsCachedData = true;
            else
                cacheEntry = await GetAndCacheDataAync<List<Breed>>(url, _settings.Value.cache_stratergies.images);

            return cacheEntry;
        }
        public async Task<ApiResult<List<Breed>>> GetBreedByName(string breedName)
        {
            string url = $"{_settings.Value.urls.breeds}";

            if (!string.IsNullOrEmpty(breedName))
                url = $"{url}/search?q={breedName}";

            ApiResult<List<Breed>> cacheEntry;

            if (Cache.TryGetCache(url, out cacheEntry))
                cacheEntry.IsCachedData = true;
            else
                cacheEntry = await GetAndCacheDataAync<List<Breed>>(url, _settings.Value.cache_stratergies.images);

            return cacheEntry;
        }

        public async Task<ApiResult<List<Image>>> GetDogByBreedId(int breedId)
        {
            string url = $"{_settings.Value.urls.images}";

            url = $"{url}/search?breed_id={breedId}";

            ApiResult<List<Image>> cacheEntry;

            if (Cache.TryGetCache(url, out cacheEntry))
                cacheEntry.IsCachedData = true;
            else
                cacheEntry = await GetAndCacheDataAync<List<Image>>(url, _settings.Value.cache_stratergies.images);

            return cacheEntry;
        }

        public async Task<ApiResult<List<Image>>> GetRandomDog()
        {
            // No caching for random dogs

            string url = $"{_settings.Value.urls.images}";

            url = $"{url}/search";

            var r = await _restClient.GetAsync<List<Image>>(url);

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
