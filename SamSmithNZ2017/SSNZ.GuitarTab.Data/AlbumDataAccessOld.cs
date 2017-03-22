using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SSNZ.GuitarTab.Models;
using Dapper;
using System.Data;

namespace SSNZ.GuitarTab.Data
{
    public class AlbumDataAccessOld : GenericDataAccessOld<Album>
    {
        public List<Album> GetData(bool isAdmin)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IsAdmin", isAdmin, DbType.Boolean);

            return base.GetList("Tab_GetAlbums", parameters).ToList<Album>();
        }

        public Album GetItem(int albumCode, bool isAdmin)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AlbumCode", albumCode, DbType.Int32);
            parameters.Add("@IsAdmin", isAdmin, DbType.Boolean);

            return base.GetItem("Tab_GetAlbums", parameters);
        }

        public Album SaveItem(Album item)
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

            item.AlbumCode = base.GetScalar<short>("Tab_SaveAlbum", parameters);
            return item;
        }

    }
}