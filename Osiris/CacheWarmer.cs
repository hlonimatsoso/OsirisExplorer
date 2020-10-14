using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Osiris
{
   public abstract class CacheWarmer
    {
        public abstract Task<bool> WarmCache();
    }
}
