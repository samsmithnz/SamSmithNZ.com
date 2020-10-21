using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.Models.GuitarTab;
using SamSmithNZ.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services
{
    public class GuitarTabServiceAPIClient : BaseServiceAPIClient, IGuitarTabServiceAPIClient
    {
        private readonly IConfiguration _configuration;

        public GuitarTabServiceAPIClient(IConfiguration configuration)
        {
            _configuration = configuration;
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(_configuration["AppSettings:WebServiceURL"])
            };
            base.SetupClient(client);
        }

        public async Task<List<Album>> GetAlbums()
        {
            Uri url = new Uri($"api/GuitarTab/Album/GetAlbums", UriKind.Relative);
            List<Album> results = await base.ReadMessageList<Album>(url);
            if (results == null)
            {
                return new List<Album>();
            }
            else
            {
                return results;
            }
        }

        public async Task<Album> GetAlbum(int albumCode)
        {
            Uri url = new Uri($"api/GuitarTab/Album/GetAlbum?AlbumCode=" + albumCode, UriKind.Relative);
            Album result = await base.ReadMessageItem<Album>(url);
            if (result == null)
            {
                return new Album();
            }
            else
            {
                return result;
            }
        }

        public async Task<bool> SaveAlbum(Album item)
        {
            Uri url = new Uri($"api/GuitarTab/Album/SaveAlbum", UriKind.Relative);
            bool result = await base.SaveMessageItem<Album>(url, item);
            return result;
        }

        public async Task<List<Artist>> GetArtists()
        {
            Uri url = new Uri($"api/GuitarTab/Artist/GetArtists", UriKind.Relative);
            List<Artist> results = await base.ReadMessageList<Artist>(url);
            if (results == null)
            {
                return new List<Artist>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Rating>> GetRatings()
        {
            Uri url = new Uri($"api/GuitarTab/Rating/GetRatings", UriKind.Relative);
            List<Rating> results = await base.ReadMessageList<Rating>(url);
            if (results == null)
            {
                return new List<Rating>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Search>> GetSearchResults()
        {
            Uri url = new Uri($"api/GuitarTab/Search/GetSearchResults", UriKind.Relative);
            List<Search> results = await base.ReadMessageList<Search>(url);
            if (results == null)
            {
                return new List<Search>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Tab>> GetTabs(int albumCode, int sortOrder = 0)
        {
            Uri url = new Uri($"api/GuitarTab/Tab/GetTabs?albumCode=" + albumCode + "&sortOrder=" + sortOrder, UriKind.Relative);
            List<Tab> results = await base.ReadMessageList<Tab>(url);
            if (results == null)
            {
                return new List<Tab>();
            }
            else
            {
                return results;
            }
        }

        public async Task<Tab> GetTab(int tabCode)
        {
            Uri url = new Uri($"api/GuitarTab/Tab/GetTab?tabCode=" + tabCode, UriKind.Relative);
            Tab results = await base.ReadMessageItem<Tab>(url);
            if (results == null)
            {
                return new Tab();
            }
            else
            {
                return results;
            }
        }

        public async Task<bool> SaveTab(Tab item)
        {
            Uri url = new Uri($"api/GuitarTab/Tab/SaveTab", UriKind.Relative);
            return await base.SaveMessageItem<Tab>(url, item);
        }

        public async Task<bool> DeleteTab(int tabCode)
        {
            Uri url = new Uri($"api/GuitarTab/Tab/DeleteTab?tabCode=" + tabCode, UriKind.Relative);
            Tab tab = await base.ReadMessageItem<Tab>(url);
            return true;
        }

        public async Task<List<TrackOrder>> GetTrackOrders()
        {
            Uri url = new Uri($"api/GuitarTab/TrackOrder/GetTrackOrders", UriKind.Relative);
            List<TrackOrder> results = await base.ReadMessageList<TrackOrder>(url);
            if (results == null)
            {
                return new List<TrackOrder>();
            }
            else
            {
                return results;
            }
        }
        public async Task<List<Tuning>> GetTunings()
        {
            Uri url = new Uri($"api/GuitarTab/Tuning/GetTunings", UriKind.Relative);
            List<Tuning> results = await base.ReadMessageList<Tuning>(url);
            if (results == null)
            {
                return new List<Tuning>();
            }
            else
            {
                return results;
            }
        }

    }
}
