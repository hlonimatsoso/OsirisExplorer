using Microsoft.AspNetCore.Components;
using Osiris.DogApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Osiris.BlazorApp.Components
{
    public class DogBase : ComponentBase
    {
        [Parameter] public Image Dog { get; set; }

    }
}
