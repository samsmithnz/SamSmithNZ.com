using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SSNZ.GuitarTab.Models;
using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.GuitarTab.Data
{
    public class AlbumDataAccess : GenericDataAccess<Album>
    {
        public async Task<List<Album>> GetListAsync(bool isAdmin)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IsAdmin", isAdmin, DbType.Boolean);

            return await base.GetListAsync("Tab_GetAlbums", parameters);
        }

        public async Task<Album> GetItemAsync(int albumCode, bool isAdmin)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AlbumCode", albumCode, DbType.Int32);
            parameters.Add("@IsAdmin", isAdmin, DbType.Boolean);

            return await base.GetItemAsync("Tab_GetAlbums", parameters);
        }

        public async Task<Album> SaveItemAsync(Album item)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AlbumCode", item.AlbumCode, DbType.Int32);
            parameters.Add("@ArtistName", item.ArtistName, DbType.String);
            parameters.Add("@AlbumName", item.AlbumName, DbType.String);
            parameters.Add("@AlbumYear", item.AlbumYear, DbType.Int32);
            parameters.Add("@IsBassTab", item.IsBassTab, DbType.Boolean);
            parameters.Add("@IsNewAlbum", item.IsNewAlbum, DbType.Boolean);
            parameters.Add("@IsMiscCollectionAlbum", item.IsMiscCollectionAlbum, DbType.Boolean);
            parameters.Add("@IncludeInIndex", item.IncludeInIndex, DbType.Boolean);
            parameters.Add("@IncludeOnWebsite", item.IncludeOnWebsite, DbType.Boolean);

            item.AlbumCode = await base.GetScalarAsync<int>("Tab_SaveAlbum", parameters);
            return item;
        }

    }
}