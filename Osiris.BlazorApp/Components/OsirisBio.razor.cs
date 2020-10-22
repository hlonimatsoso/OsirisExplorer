using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Osiris.BlazorApp.Components
{
    public class OsirisBioBase : ComponentBase
    {
        [Parameter] public string Name { get; set; }

        [Parameter] public string Picture { get; set; }
        
        [Parameter] public string Description { get; set; }

    }
}
