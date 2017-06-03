using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ActionParameterAlias;

namespace SamSmithNZ2017.Controllers
{
    public class FooFightersController : Controller
    {
        //
        // GET: /FooFighters/
        //Map: http://blog.cartodb.com/post/39680106243/cartodb-makes-d3-maps-a-breeze

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowList()
        {
            return View();
        }
        
        public ActionResult Song(int songCode)
        {
            return View();
        }

        
        public ActionResult Show(int showCode)
        {
            return View();
        }

        public ActionResult Album(int albumCode)
        {
            return View();
        }

        public ActionResult AlbumList()
        {
            return View();
        }



        //public ActionResult Song(short songKey)
        //{
        //    return RedirectToAction("Song", new { songCode = songKey });
        //}

        public ActionResult ShowYearList()
        {
            return RedirectToAction("ShowList");
        }

        //public ActionResult Show(int showKey)
        //{
        //    return RedirectToAction("Show", new { showCode = showKey });
        //}

        public ActionResult SetlistProcessor()
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/FooFighters/SetlistProcessor");

            //return View();
        }

        //private GraphNodeGeneration GetAlbumGraph()
        //{
        //    AlbumDataAccess da = new AlbumDataAccess();
        //    List<Album> albums = da.GetItems();

        //    DataTable data = new DataTable();
        //    data.TableName = "song";
        //    data.Columns.Add("Song");
        //    data.Columns.Add("Album");
        //    data.Columns.Add("Size");

        //    List<List<Song>> albumSongList = new List<List<Song>>();
        //    foreach (Album item in albums)
        //    {
        //        //If the album was released and is not the greatest hits, include it.
        //        if (item.AlbumCode == 1 || item.AlbumCode == 2 || item.AlbumCode == 3 || item.AlbumCode == 6 || item.AlbumCode == 7 || item.AlbumCode == 8 || item.AlbumCode == 9 || item.AlbumCode == 11)
        //        {
        //            SongDataAccess da2 = new SongDataAccess();
        //            List<Song> songs = da2.GetSongsForAlbum(item.AlbumCode);
        //            albumSongList.Add(songs);
        //        }
        //    }

        //    foreach (List<Song> songs in albumSongList)
        //    {
        //        foreach (Song item in songs)
        //        {
        //            DataRow dr = data.NewRow();
        //            dr["Song"] = item.SongOrder.ToString() + ". " + item.SongName + " (" + item.SongTimesPlayed.ToString() + ")";
        //            dr["Album"] = item.AlbumName;
        //            dr["Size"] = item.SongTimesPlayed;
        //            data.Rows.Add(dr);
        //        }
        //    }

        //    GraphNodeGeneration graph = new GraphNodeGeneration();

        //    //Populate the Vertices and Relationships
        //    graph.GetVertices(data);

        //    //Convert the codes to text
        //    graph.TransformCodesToText();

        //    return graph;
        //}

    }
}
