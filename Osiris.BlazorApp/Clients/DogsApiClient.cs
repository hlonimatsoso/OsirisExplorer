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
    public class DogsApiClient :  IDogsApiClient
    {

        IHttpRestClient _restClient;
        ILogger<ConfigClient> _logger;

        public DogsApiClient(IHttpRestClient restClient, ILogger<ConfigClient> logger)
        {
            _restClient = restClient;
            _logger = logger;
        }

        public List<Breed> Breeds { get; set; }
        public List<Image> Dogs { get; set; }
        public IMemoryCache Cache { get; set ; }

        public async Task<ApiResult<List<Breed>>> GetAllBreeds()
        {

            var temp = await _restClient.GetAsync<List<Breed>>("api/dogs/");
            Breeds = temp.Results;
            await GetBreedImagesAsync();
            return temp;
        }

        private async Task GetBreedImagesAsync()
        {
    
            var list = Breeds.Select(x => x.id).ToList();
            var temp = await SearchAllImages(string.Join(',', list));
            if (temp.IsValid)
            {
                Dogs = temp.Results;

            }
        }

        public Task<ApiResult<List<Breed>>> GetBreedByName(string breedName)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<List<Image>>> GetDogByBreedId(int breedId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<List<Image>>> GetRandomDog()
        {
            var temp = await _restClient.GetAsync<List<Image>>("api/dogs/random");
            return temp;
        }

        public async Task<ApiResult<List<Image>>> SearchAllImages(string list)
        {
            string url = $"api/dogs/images/search/{list}";
            return await _restClient.GetAsync<List<Image>>(url);
        }
    }
}
