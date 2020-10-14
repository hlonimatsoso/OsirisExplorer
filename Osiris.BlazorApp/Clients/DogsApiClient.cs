using Microsoft.Extensions.Caching.Memory;
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
    public class DogsApiClient : ApiBase, IDogsApiClient
    {

        public DogsApiClient(HttpClient client, IOptions<DogApiSettings> settings) : base(client, settings)
        { }

        public List<Breed> Breeds { get; set; }
        public List<Image> Dogs { get; set; }
        public IMemoryCache Cache { get; set ; }

        public async Task<ApiResult<List<Breed>>> GetAllBreeds()
        {
            CurrentlyBusyWith = $"GetAllBreeds()";

            var temp = await Get<List<Breed>>("api/dogs/");
            Breeds = temp.Results;
            await GetBreedImagesAsync();
            CurrentlyBusyWith = "GetAllBreeds & GetBreedImagesAsync done";
            return temp;
        }

        private async Task GetBreedImagesAsync()
        {
            CurrentlyBusyWith = $"GetBreedImagesAsync()";

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

        public Task<ApiResult<List<Image>>> GetRandomDog()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<List<Image>>> SearchAllImages(string list)
        {
            string url = $"api/dogs/images/search/{list}";
            return Get<List<Image>>(url);
        }
    }
}
