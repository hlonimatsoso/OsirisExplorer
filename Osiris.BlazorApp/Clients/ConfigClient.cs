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

        public ConfigClient(IHttpRestClient restClient, ILogger<ConfigClient> logger, ICacheIsKing cache)
        {
            _restClient = restClient;
            _logger = logger;
            _cache = cache;
        }

        public async Task<int> GetPictureCallageCount()
        {
            ApiResult<int> result = new ApiResult<int>();
            string url = "api/Configuration/RandomPictureCallageCount";
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
                    result = await _restClient.GetAsync<int>("api/Configuration/RandomPictureCallageCount");
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
    }
}
