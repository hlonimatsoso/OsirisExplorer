using Newtonsoft.Json;
using Osiris.DogApi;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Osiris
{
    public class HttpRestClient : IHttpRestClient
    {
        HttpClient _dogsAPIclient;

        public HttpRestClient(IHttpClientFactory factory)
        {
            _dogsAPIclient = factory.CreateClient("base");
        }

        public async Task<ApiResult<T>> GetAsync<T>(string url)
        {
            var httpResponse = await _dogsAPIclient.GetAsync($"{url}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"BOOM!!! : {httpResponse.ReasonPhrase}");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            ApiResult<T> r = new ApiResult<T>();
            r.IsValid = httpResponse.IsSuccessStatusCode;
            r.HttpStatusCode = httpResponse.StatusCode;
            try
            {
                r.Results = JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception)
            {

                var test = JsonConvert.DeserializeObject<ApiResult<T>>(content);
                r.Results = test.Results;
            }

            return r;
        }
    }
}
