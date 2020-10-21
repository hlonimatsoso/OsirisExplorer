using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris
{
    public class Bio : ComponentBase
    {
        [Parameter] public string Name { get; set; }
    }
}
