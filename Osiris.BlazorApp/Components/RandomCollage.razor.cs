using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
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

            await InvokeAsync(() => { StateHasChanged(); });

            base.OnInitialized();
        }

    }
}
