using Microsoft.AspNetCore.Components;
using Osiris.DogApi;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Osiris.BlazorApp.Pages
{
    public class GlossoryBase : ComponentBase
    {

        [Parameter]
        public List<Breed> Breeds { get; set; }

        public List<Image> Dogs
        {
            get
            {
                if (Client != null) return Client.Dogs;
                else
                    return null;

            }
        }

        public string ErrorMessage { get; set; }

        [Inject]
        public IDogsApiClient Client { get; set; }

        private int _breedCount;
        public int BreedCount
        {
            get
            {
                if (Breeds != null)
                    _breedCount = Breeds.Count;

                return _breedCount;
            }
        }


        protected async override Task OnInitializedAsync()
        {

            var apiResult = await Client.GetAllBreeds();

            if (apiResult.IsValid)
            {
                Breeds = apiResult.Results;
            }
            else
                ErrorMessage = apiResult.ErrorMessage;

            await InvokeAsync(() => { StateHasChanged(); });

            await base.OnInitializedAsync();

        }

    }
}
