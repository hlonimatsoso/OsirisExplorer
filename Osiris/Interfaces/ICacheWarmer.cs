using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Osiris.Interfaces
{
    public interface ICacheWarmer
    {
        Task<bool> WarmCache();
    }

    //public interface ICacheWarmerExecution
    //{
    //    Task<bool> WarmCache();
    //}
}
