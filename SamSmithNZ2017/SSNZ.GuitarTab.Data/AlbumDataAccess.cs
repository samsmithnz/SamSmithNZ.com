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
    public class AlbumDataAccess : GenericDataAccess<Album>
    {
        public List<Album> GetItems(bool isAdmin)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@is_admin", isAdmin, DbType.Boolean);

            return base.GetList("spKS_Tab_GetAlbums", parameters).ToList<Album>();
        }

        public Album GetItem(int albumCode, bool isAdmin)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@album_code", albumCode, DbType.Int32);
            parameters.Add("@is_admin", isAdmin, DbType.Boolean);

            return base.GetItem("spKS_Tab_GetAlbums", parameters);
        }

        public Album Save(Album item)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@album_code", item.AlbumCode, DbType.Int32);
            parameters.Add("@artist_name", item.ArtistName, DbType.String);
            parameters.Add("@album_name", item.AlbumName, DbType.String);
            parameters.Add("@album_year", item.AlbumYear, DbType.Int32);
            parameters.Add("@is_bass_tab", item.IsBassTab, DbType.Boolean);
            parameters.Add("@is_new_album", item.IsNewAlbum, DbType.Boolean);
            parameters.Add("@is_misc_collection_album", item.IsMiscCollectionAlbum, DbType.Boolean);
            parameters.Add("@include_in_index", item.IncludeInIndex, DbType.Boolean);
            parameters.Add("@include_on_website", item.IncludeOnWebsite, DbType.Boolean);

            item.AlbumCode = base.GetScalar<short>("spKS_Tab_SaveAlbum", parameters);
            return item;
        }

    }
}