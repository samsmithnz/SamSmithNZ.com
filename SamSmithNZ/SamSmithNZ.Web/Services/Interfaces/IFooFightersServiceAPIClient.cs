using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.Models.FooFighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services.Interfaces
{
    public interface IFooFightersServiceAPIClient
    {

        Task<List<Album>> GetAlbums();
        Task<Album> GetAlbum(int albumCode);
        Task<List<AverageSetlist>> GetAverageSetlist(int yearCode, int minimumSongCount = 0, bool showAllSongs = false);
        Task<List<Show>> GetShowsByYear(int yearCode);
        Task<List<Show>> GetShowsBySong(int songCode);
        Task<Show> GetShow(int showCode);
        Task<List<Song>> GetSongs();
        Task<List<Song>> GetSongsByAlbum(int albumCode);
        Task<List<Song>> GetSongsByShow(int showCode);
        Task<Song> GetSong(int songCode);
        Task<List<Year>> GetYears();

    }
}
