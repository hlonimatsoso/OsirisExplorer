﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Osiris.DogApi;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Osiris.BlazorApp.Pages
{
    public class BreedsBase : ComponentBase
    {
        [Inject] public IDogsApiClient DogsClient { get; set; }

        [Inject] private ILogger<BreedsBase> Logger { get; set; }


        public ApiResult<List<Breed>> AllBreedsResult { get; set; }

        public List<Image> AllDogs { get; set; }

        public List<Image> FilteredDogs { get; set; }



        protected async override void OnInitialized()
        {
            AllBreedsResult = await DogsClient.GetAllBreeds();

            AllDogs = DogsClient.Dogs.ToList();

            await InvokeAsync(() => { StateHasChanged(); });

        }
    }
}
