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

        public DogService(IHttpClientFactory factory, IOptions<DogApiSettings> settings) : base(factory, settings)
        { }

        public async Task<ApiResult<List<Breed>>> GetAllBreeds()
        {
            string url = $"{DbSettings.Value.queries_breeds}";

            var r = await Get<List<Breed>>(url);

            return r;
        }

        public async Task<ApiResult<List<Breed>>> GetBreedByName(string breedName)
        {
            string url = $"{DbSettings.Value.queries_breeds}";

            if (!string.IsNullOrEmpty(breedName))
                url = $"{url}/search?q={breedName}";

            var r = await Get<List<Breed>>(url);

            return r;
        }

        public async Task<ApiResult<List<Image>>> GetDogByBreedId(int breedId)
        {
            string url = $"{DbSettings.Value.queries_images}";

            url = $"{url}/search?breed_id={breedId}";

            var r = await Get<List<Image>>(url);

            return r;
        }

        public async Task<ApiResult<List<Image>>> GetRandomDog()
        {
            string url = $"{DbSettings.Value.queries_images}";

            url = $"{url}/search";

            var r = await Get<List<Image>>(url);

            return r;
        }

        
    }
}
