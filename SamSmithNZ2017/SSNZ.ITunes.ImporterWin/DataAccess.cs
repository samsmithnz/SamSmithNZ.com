using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SSNZ.ITunes.ImporterWin
{
  public static  class DataAccess
    {

        public static int CreateNewPlayList(DateTime dteDate)
        {
            int functionReturnValue = 0;
            SqlConnection objSQLConn = new SqlConnection();
            SqlCommand objSQLCmd = default(SqlCommand);

            //try
            //{
            //    //create a connection
            //    objSQLConn = CreateConnection();

            //    objSQLCmd = new SqlCommand();
            //    var _with1 = objSQLCmd;
            //    _with1.Connection = objSQLConn;
            //    _with1.CommandTimeout = 60;
            //    _with1.CommandType = CommandType.StoredProcedure;
            //    _with1.CommandText = "spIT_CreateNewPlayList";
            //    _with1.Parameters.Add("@playlist_date", SqlDbType.DateTime).Value = dteDate;
            //    functionReturnValue = Convert.ToInt32(_with1.ExecuteScalar());

            //}
            //finally
            //{
            //    //close the connection
            //    if (objSQLConn.State == ConnectionState.Open)
            //    {
            //        objSQLConn.Close();
            //    }
            //}
            return functionReturnValue;
        }

        public static bool DeletePlaylistTracks(int intPlayListCode)
        {

            SqlConnection objSQLConn = new SqlConnection();
            SqlCommand objSQLCmd = default(SqlCommand);

            //try
            //{
            //    //create a connection
            //    objSQLConn = CreateConnection();

            //    objSQLCmd = new SqlCommand();
            //    var _with2 = objSQLCmd;
            //    _with2.Connection = objSQLConn;
            //    _with2.CommandType = CommandType.StoredProcedure;
            //    _with2.CommandText = "spIT_DeletePlaylistTracks";
            //    _with2.Parameters.Add("@playlist_code", SqlDbType.Int).Value = intPlayListCode;
            //    _with2.ExecuteNonQuery();

            //}
            //finally
            //{
            //    //close the connection
            //    if (objSQLConn.State == ConnectionState.Open)
            //    {
            //        objSQLConn.Close();
            //    }
            //}
            return true;

        }

        public static bool InsertTrack(int intPlayListCode, ITunesTrack objTrack)
        {

            SqlConnection objSQLConn = new SqlConnection();
            SqlCommand objSQLCmd = default(SqlCommand);

            //try
            //{
            //    //create a connection
            //    objSQLConn = CreateConnection();

            //    objSQLCmd = new SqlCommand();
            //    var _with3 = objSQLCmd;
            //    _with3.Connection = objSQLConn;
            //    _with3.CommandTimeout = 60;
            //    _with3.CommandType = CommandType.StoredProcedure;
            //    _with3.CommandText = "spIT_InsertTrack";
            //    _with3.Parameters.Add("@playlist_code", SqlDbType.Int).Value = intPlayListCode;
            //    _with3.Parameters.Add("@track_name", SqlDbType.VarChar, 50).Value = objTrack.TrackName;
            //    _with3.Parameters.Add("@album_name", SqlDbType.VarChar, 50).Value = objTrack.AlbumName;
            //    _with3.Parameters.Add("@artist_name", SqlDbType.VarChar, 50).Value = objTrack.ArtistName;
            //    _with3.Parameters.Add("@play_count", SqlDbType.SmallInt).Value = objTrack.PlayCount;
            //    _with3.Parameters.Add("@ranking", SqlDbType.SmallInt).Value = 1;
            //    _with3.Parameters.Add("@rating", SqlDbType.SmallInt).Value = objTrack.Rating;
            //    _with3.ExecuteNonQuery();

            //}
            //finally
            //{
            //    //close the connection
            //    if (objSQLConn.State == ConnectionState.Open)
            //    {
            //        objSQLConn.Close();
            //    }
            //}

            return true;
        }

        public static DataSet ValidateTracksForDuplicates(int intPlayListCode)
        {

            SqlConnection objSQLConn = new SqlConnection();
            SqlCommand objSQLCmd = default(SqlCommand);

            //try
            //{
            //    //create a connection
            //    objSQLConn = CreateConnection();

            //    objSQLCmd = new SqlCommand();
            //    var _with4 = objSQLCmd;
            //    _with4.Connection = objSQLConn;
            //    _with4.CommandType = CommandType.StoredProcedure;
            //    _with4.CommandText = "spIT_ValidateTracksForDuplicates";
            //    _with4.Parameters.Add("@playlist_code", SqlDbType.Int).Value = intPlayListCode;
            //    return GetDataSet(objSQLCmd, "count");

            //}
            //finally
            //{
            //    //close the connection
            //    if (objSQLConn.State == ConnectionState.Open)
            //    {
            //        objSQLConn.Close();
            //    }
            //}
            return null;
        }

        public static bool SetTrackRanks(int intPlayListCode)
        {

            SqlConnection objSQLConn = new SqlConnection();
            SqlCommand objSQLCmd = default(SqlCommand);

            //try
            //{
            //    //create a connection
            //    objSQLConn = CreateConnection();

            //    objSQLCmd = new SqlCommand();
            //    var _with5 = objSQLCmd;
            //    _with5.Connection = objSQLConn;
            //    _with5.CommandTimeout = 3600;
            //    // 1 hour
            //    _with5.CommandType = CommandType.StoredProcedure;
            //    _with5.CommandText = "spIT_SetTrackRanks";
            //    _with5.Parameters.Add("@playlist_code", SqlDbType.Int).Value = intPlayListCode;

            //    _with5.ExecuteNonQuery();

            //}
            //finally
            //{
            //    //close the connection
            //    if (objSQLConn.State == ConnectionState.Open)
            //    {
            //        objSQLConn.Close();
            //    }
            //}
            return true;

        }

    }
}
