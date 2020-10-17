using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters
{
    public interface ISongDataAccess
    {
        Task<List<Song>> GetListAsync();
        Task<List<Song>> GetListForAlbumAsync(int albumCode);
        Task<List<Song>> GetListForShowAsync(int showCode);
        Task<Song> GetItemAsync(int songCode);
        Task<bool> SaveItemAsync(int songCode, int showCode, int showSongOrder);

    }
}


