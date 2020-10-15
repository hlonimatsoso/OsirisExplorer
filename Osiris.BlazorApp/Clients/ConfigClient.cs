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

        public ConfigClient(IHttpRestClient restClient, ILogger<ConfigClient> logger)
        {
            _restClient = restClient;
            _logger = logger;
        }

        public async Task<int> GetPictureCallageCount()
        {
            ApiResult<int> result = new ApiResult<int>();

            try
            {
                result = await _restClient.GetAsync<int>("api/Configuration/RandomPictureCallageCount");
            }
            catch (Exception ex)
            {

                _logger.LogError($"!!! ERROR !!! {ex.ToString()}");
            }

            return result.Results;
        }
    }
}
