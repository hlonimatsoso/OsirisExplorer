using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Osiris.Api.DogApi;
using Osiris.DogApi;
using Osiris.Interfaces;
using Serilog;

namespace Osiris.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        IDogService _dogService;
        ILogger<DogsController> _logger;

        public DogsController(IDogService service, ILogger<DogsController> logger)
        {
            this._dogService = service;
            this._logger = logger;
        }

        [HttpGet("")]
        [HttpGet("breeds")]
        public async Task<ApiResult<List<Breed>>> GetAllBreeds()
        {
            _logger.LogInformation("Getting all breeds");
            Log.Information("INFO: Getting all breeds");

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

        [HttpGet("images/search/{breedIds}")]
        public async Task<ApiResult<List<Image>>> SearchAllImages(string breedIds)
        {
            return await _dogService.SearchAllImages(breedIds);
        }
    }
}
