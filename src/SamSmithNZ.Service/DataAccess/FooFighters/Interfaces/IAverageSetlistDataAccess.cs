using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters.Interfaces
{
    public interface IAverageSetlistDataAccess 
    {
        Task<List<AverageSetlist>> GetList(int yearCode, int minimumSongCount, bool showAllSongs);       
    }
}


