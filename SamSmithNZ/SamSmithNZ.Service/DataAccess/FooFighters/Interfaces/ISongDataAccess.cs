using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters.Interfaces
{
    public interface ISongDataAccess
    {
        Task<List<Song>> GetList();
        Task<List<Song>> GetListForAlbumAsync(int albumCode);
        Task<List<Song>> GetListForShowAsync(int showCode);
        Task<Song> GetItem(int songCode);
        Task<bool> SaveItem(int songCode, int showCode, int showSongOrder);

    }
}


