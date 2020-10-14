using Microsoft.Extensions.Caching.Memory;
using Osiris.DogApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Osiris.Interfaces
{
    public interface IDogsClient
    {
        Task<ApiResult<List<Breed>>> GetAllBreeds();

        Task<ApiResult<List<Breed>>> GetBreedByName(string breedName);

        Task<ApiResult<List<Image>>> GetRandomDog();

        Task<ApiResult<List<Image>>> GetDogByBreedId(int breedId);

        Task<ApiResult<List<Image>>> SearchAllImages(string list);


        
    }
}
