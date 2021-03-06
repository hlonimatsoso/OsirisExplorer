﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Osiris.DogApi;

namespace Osiris.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        IOptions<DogApiSettings> _settings;

        public ConfigurationController(IOptions<DogApiSettings> settings)
        {
            _settings = settings;
        }

        [HttpGet("RandomPictureCallageCount")]
        public int GetPictureCallageCount()
        {
            return _settings.Value.randomPictureCallageCount;
        }

        [HttpGet("Bios")]
        public Task<List<Bio>> GetBios()
        {
            return Task.FromResult<List<Bio>>(_settings.Value.biographies.ToList());
        }

        [HttpGet("clientSettings")]
        public Task<ClientSettings> GetClientSettings()
        {
            return Task.FromResult<ClientSettings>(_settings.Value.client_settings);
        }

        
    }
}
