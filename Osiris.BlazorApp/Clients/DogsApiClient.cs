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

        public async Task<ApiResult<List<Breed>>> GetAllBreeds()
        {
            return await Get<List<Breed>>("api/dogs/");
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
    }
}
