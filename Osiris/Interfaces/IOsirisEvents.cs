using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris.Interfaces
{
    public interface IOsirisEvents
    {
        bool IsBusy { get; set; }

        EventCallback<bool> IsBusyChanged { get; set; }


        double IsBusyPercentage { get; set; }

        EventCallback<double> IsBusyPercentageChanged { get; set; }



    }
}
