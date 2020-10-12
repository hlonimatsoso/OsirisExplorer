using Osiris.DogApi;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Osiris.BlazorApp.Clients
{
    public class DogsApiClient : IDogsApiClient
    {
        public Task<ApiResult<List<Breed>>> GetAllBreeds()
        {
            throw new NotImplementedException();
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
