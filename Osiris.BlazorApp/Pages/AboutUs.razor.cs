using Microsoft.AspNetCore.Components;
using Osiris.DogApi;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Osiris.BlazorApp.Pages
{
    public class AboutUsBase : ComponentBase
    {
        [Inject] public IConfigClient Client { get; set; }

       public ClientSettings Settings { get; set; }

        public List<Bio> Bios { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Settings = await Client.GetClientSettings();

            Bios = await Client.GetBio();

            await base.OnInitializedAsync();
        }
    }
    
}
