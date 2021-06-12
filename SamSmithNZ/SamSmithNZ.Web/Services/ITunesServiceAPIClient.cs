using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.Models.ITunes;
using SamSmithNZ.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services
{
    public class ITunesServiceAPIClient : BaseServiceAPIClient, IITunesServiceAPIClient
    {
        private readonly IConfiguration _configuration;

        public ITunesServiceAPIClient(IConfiguration configuration)
        {
            _configuration = configuration;
            HttpClient client = new HttpClient
            {
                BaseAddress = new(_configuration["AppSettings:WebServiceURL"])
            };
            base.SetupClient(client);
        }

        public async Task<List<Movement>> GetMovementsByPlaylist(int playlistCode, bool showJustSummary)
        {
            Uri url = new($"api/itunes/Movement/GetMovementsByPlaylist?playlistCode=" + playlistCode + "&showJustSummary=" + showJustSummary, UriKind.Relative);
            List<Movement> results = await base.ReadMessageList<Movement>(url);
            if (results == null)
            {
                return new List<Movement>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Movement>> GetMovementsSummary(bool showJustSummary)
        {
            Uri url = new($"api/itunes/Movement/GetMovementsSummary?showJustSummary=" + showJustSummary, UriKind.Relative);
            List<Movement> results = await base.ReadMessageList<Movement>(url);
            if (results == null)
            {
                return new List<Movement>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Playlist>> GetPlaylists(bool showJustSummary)
        {
            Uri url = new($"api/itunes/Playlist/GetPlaylists?showJustSummary=" + showJustSummary, UriKind.Relative);
            List<Playlist> results = await base.ReadMessageList<Playlist>(url);
            if (results == null)
            {
                return new List<Playlist>();
            }
            else
            {
                return results;
            }
        }

        public async Task<Playlist> GetPlaylist(int playlistCode)
        {
            Uri url = new($"api/itunes/Playlist/GetPlaylist?playlistCode=" + playlistCode, UriKind.Relative);
            Playlist result = await base.ReadMessageItem<Playlist>(url);
            if (result == null)
            {
                return new Playlist();
            }
            else
            {
                return result;
            }
        }

        public async Task<List<TopArtists>> GetTopArtistsByPlaylist(int playlistCode, bool showJustSummary)
        {
            Uri url = new($"api/itunes/TopArtists/GetTopArtistsByPlaylist?playlistCode=" + playlistCode + "&showJustSummary=" + showJustSummary, UriKind.Relative);
            List<TopArtists> results = await base.ReadMessageList<TopArtists>(url);
            if (results == null)
            {
                return new List<TopArtists>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<TopArtists>> GetTopArtistsSummary(bool showJustSummary)
        {
            Uri url = new($"api/itunes/TopArtists/GetTopArtistsSummary?showJustSummary=" + showJustSummary, UriKind.Relative);
            List<TopArtists> results = await base.ReadMessageList<TopArtists>(url);
            if (results == null)
            {
                return new List<TopArtists>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Track>> GetTracks(int playlistCode, bool showJustSummary)
        {
            Uri url = new($"api/itunes/Track/GetTracks?playlistCode=" + playlistCode + "&showJustSummary=" + showJustSummary, UriKind.Relative);
            List<Track> results = await base.ReadMessageList<Track>(url);
            if (results == null)
            {
                return new List<Track>();
            }
            else
            {
                return results;
            }
        }

       
    }
}
