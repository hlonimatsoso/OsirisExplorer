using Osiris.DogApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Osiris.Interfaces
{
    public interface IConfigClient
    {
        Task<int> GetPictureCallageCount();
        Task<ClientSettings> GetClientSettings();
        Task<List<Bio>> GetBio();

    }
}
