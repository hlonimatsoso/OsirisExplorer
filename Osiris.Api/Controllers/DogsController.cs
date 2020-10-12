using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Osiris.Api.DogApi;
using Osiris.DogApi;
using Osiris.Interfaces;

namespace Osiris.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        IDogService _dogService;

        public DogsController(IDogService service)
        {
            this._dogService = service;
        }

        [HttpGet("")]
        [HttpGet("breeds")]
        public async Task<ApiResult<List<Breed>>>GetAllBreeds()
        {
            return await _dogService.GetAllBreeds();
        }

        [HttpGet("breeds/search/{breedName}")]
        public async Task<ApiResult<List<Breed>>> SearchBreeds(string breedName)
        {
            return await _dogService.GetBreedByName(breedName);
        }

        [HttpGet("random")]
        public async Task<ApiResult<List<Image>>> Random()
        {
            return await _dogService.GetRandomDog();
        }

        [HttpGet("images/search/{breedId:int}")]
        public async Task<ApiResult<List<Image>>> SearchImages(int breedId)
        {
            return await _dogService.GetDogByBreedId(breedId);
        }

    }
}
