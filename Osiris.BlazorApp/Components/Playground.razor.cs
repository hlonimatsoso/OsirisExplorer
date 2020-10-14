using Microsoft.AspNetCore.Components;
using Osiris.DogApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Osiris.BlazorApp.Components
{
    public class PlaygroundBase : ComponentBase
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

        }

       [Parameter] public List<Breed> Breeds { get; set; }
    }
}
