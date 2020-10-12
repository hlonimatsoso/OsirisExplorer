using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Osiris.DogApi;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Osiris
{
    public abstract class ApiBase : IApiBase
    {
       
        public ApiBase(IHttpClientFactory factory, IOptions<DogApiSettings> settings)
        {
            this.Client = factory.CreateClient("DogsAPI");
            this.DbSettings = settings;
        }

        public HttpClient Client { get ; set ; }

        public IOptions<DogApiSettings> DbSettings { get; set; }

        public async Task<ApiResult<T>> Get<T>(string url)
        {
            var httpResponse = await Client.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"BOOM!!! : {httpResponse.ReasonPhrase}");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            ApiResult<T> r = new ApiResult<T>();
            r.IsValid = httpResponse.IsSuccessStatusCode;
            r.HttpStatusCode = httpResponse.StatusCode;
            var dez = JsonConvert.DeserializeObject<T>(content);

            r.Results = dez;
            return r;
        }

        public ApiResult<bool> Post<T>(string url)
        {
            throw new NotImplementedException();
        }
    }
}
