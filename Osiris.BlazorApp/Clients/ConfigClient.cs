using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Osiris.DogApi;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Osiris.BlazorApp.Clients
{
    public class ConfigClient : IConfigClient
    {
        IHttpRestClient _restClient;
        ILogger<ConfigClient> _logger;
        ICacheIsKing _cache;

        //ClientSettings _clientSettings;

        public ConfigClient(IHttpRestClient restClient, ILogger<ConfigClient> logger, ICacheIsKing cache)
        {
            _restClient = restClient;
            _logger = logger;
            _cache = cache;
           // _clientSettings = GetClientSettings();
        }

        public async Task<int> GetPictureCallageCount()
        {

            ApiResult<int> result = new ApiResult<int>();
            string url = $"{Constants.RANDOM_PICTURE_COLLAGE_COUNT_URL}";
            int count;
            try
            {

                if (_cache.TryGetCache<int>(url, out count))
                {
                    result.Results = count;
                    _logger.LogInformation($"'{count}' for '{url}' was received from cache");
                }
                else
                {
                    result = await _restClient.GetAsync<int>($"{Constants.RANDOM_PICTURE_COLLAGE_COUNT_URL}");
                    _cache.SetCache<int>(url, result.Results, 10000);
                    _logger.LogInformation($"'{result}' for '{url}' is now cached for 10000s");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"!!! ERROR !!! {ex.ToString()}");
            }

            return result.Results;
        }

        public async Task<ClientSettings> GetClientSettings()
        {
            _logger.LogWarning($"Trying to load client settings : {Constants.CLIENT_SETTINGS_URL}");

            var result = await _restClient.GetAsync<ClientSettings>(Constants.CLIENT_SETTINGS_URL);

            _logger.LogWarning($"Loaded client settings : {result.Results}");

            return result.Results;
        }

        public async Task<List<Bio>> GetBio()
        {
            _logger.LogWarning($"Trying to load bios : {Constants.BIO_URL}");

            var result = await _restClient.GetAsync<List<Bio>>(Constants.BIO_URL);
            _logger.LogWarning($"BIOs : {result}");

            return result.Results;
        }
    }
}
