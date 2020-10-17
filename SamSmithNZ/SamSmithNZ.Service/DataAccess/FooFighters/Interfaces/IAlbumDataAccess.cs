using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters
{
    public interface IAlbumDataAccess
    {
        Task<List<Album>> GetListAsync();
        Task<Album> GetItemAsync(int albumCode);
    }
}


