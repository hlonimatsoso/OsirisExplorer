using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Osiris.DogApi;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Osiris.BlazorApp.Components
{

    public class RandomCollageBase : ComponentBase
    {
        [Inject] public IConfigClient ConfigClient { get; set; }
        [Inject] public IDogsApiClient DogsClient { get; set; }

        [Inject] private ILogger<RandomCollageBase> Logger { get; set; }


        public List<Image> RandomDogs { get; set; }

        private int _pictureCallageCount;
        public int PictureCallageCount
        {
            get
            {

                return _pictureCallageCount;
            }

            set
            {
                if (value > 0)
                    _pictureCallageCount = value;

            }
        }

        protected async override void OnInitialized()
        {

            if (_pictureCallageCount == 0)
                _pictureCallageCount = await ConfigClient.GetPictureCallageCount();

            await InitilizeRndomDogs();

            await InvokeAsync(() => { StateHasChanged(); });

            base.OnInitialized();
        }


        private async Task InitilizeRndomDogs()
        {
            RandomDogs = new List<Image>();

            for (int i = 0; i < _pictureCallageCount; i++)
            {
                ApiResult<List<Image>> doggie = await DogsClient.GetRandomDog();
                Logger.LogInformation($"Retrieved random dog # {i} : {JsonConvert.SerializeObject(doggie.Results.First())}");
                RandomDogs.Add(doggie.Results.First());
            }
        }
    }
}
