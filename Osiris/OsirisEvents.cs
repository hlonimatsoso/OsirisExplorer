using Microsoft.AspNetCore.Components;
using Osiris.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris
{
    public class OsirisEvents : IOsirisEvents
    {
        public bool IsBusy { get; set; }
        public bool IsBusyWithPercentage { get; set; }
        public int IsBusyPercentageAsync { get; set; }
        public EventCallback<bool> IsBusyChanged { get ; set ; }
        public double IsBusyPercentage { get ; set ; }
        public EventCallback<double> IsBusyPercentageChanged { get ; set ; }
    }
}
