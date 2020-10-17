using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters
{
    public interface IAverageSetlistDataAccess 
    {
        Task<List<AverageSetlist>> GetListAsync(int yearCode, int minimumSongCount, bool showAllSongs);       
    }
}


