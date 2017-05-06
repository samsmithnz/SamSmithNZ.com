using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
//using SamSmithNZ2015.Core.FooFighters;
//using SamSmithNZ2015.Core.FooFighters.DataAccess;
//using SamSmithNZ2015.Models.FooFighters;
//using SamSmithNZ2015.Core.Graph;

namespace SamSmithNZ2017.Controllers
{
    public class FooFightersController : Controller
    {
        //
        // GET: /FooFighters/
        //Map: http://blog.cartodb.com/post/39680106243/cartodb-makes-d3-maps-a-breeze

        public ActionResult Index()
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/FooFighters/Index");

            //SongDataAccess da = new SongDataAccess();
            //return View(da.GetItems());
        }

        public ActionResult ShowYearList()
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/FooFighters/ShowYearList");

            //ShowYearDataAccess da = new ShowYearDataAccess();
            //IEnumerable<SelectListItem> selectItems;
            //selectItems = da.GetItems()
            //          .Select(x =>
            //              new SelectListItem
            //              {
            //                  Text = x.YearText,
            //                  Value = x.YearCode.ToString()
            //              });


            //return View(selectItems);
        }

        [HttpPost]
        public ActionResult ShowListPartial(string cboShowYear2)
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/FooFighters/ShowListPartial?cboShowYear2=" + cboShowYear2);

            //ShowDataAccess da = new ShowDataAccess();
            //int yearCode = Convert.ToInt16(cboShowYear2);

            //return PartialView(new ShowsPartialViewModel(da.GetItemsByYear(yearCode), "ShowList"));
        }

        public ActionResult AlbumList()
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/FooFighters/AlbumList");

            //AlbumDataAccess da = new AlbumDataAccess();
            //GraphNodeGeneration graph = GetAlbumGraph();

            //return View(new SamSmithNZ2015.Models.FooFighters.AlbumsSongGraphViewModel(da.GetItems(), graph.GetVerticeScriptForNodes(graph.VerticeList), graph.GetVerticeEdgeScriptForNodes(graph.VerticeEdgeList)));
        }

        public ActionResult Song(int songKey)
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/FooFighters/Song?songKey=" + songKey);

            //SongDataAccess da = new SongDataAccess();
            //ShowDataAccess da2 = new ShowDataAccess();
            //return View(new SongShowsViewModel(da.GetItem(songKey), da2.GetItemsBySong(songKey)));
        }

        public ActionResult Show(int showKey)
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/FooFighters/Show?showKey="+ showKey);

            //ShowDataAccess da = new ShowDataAccess();
            //SongDataAccess da2 = new SongDataAccess();
            //return View(new ShowSongsViewModel(da.GetItem(showKey), da2.GetSongsForShow(showKey)));
        }

        public ActionResult Album(int albumKey)
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/FooFighters/Album?albumKey=" + albumKey);

            //AlbumDataAccess da = new AlbumDataAccess();
            //SongDataAccess da2 = new SongDataAccess();
            //return View(new AlbumSongsViewModel(da.GetItem(albumKey), da2.GetSongsForAlbum(albumKey)));
        }

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
        //        if (item.AlbumKey == 1 || item.AlbumKey == 2 || item.AlbumKey == 3 || item.AlbumKey == 6 || item.AlbumKey == 7 || item.AlbumKey == 8 || item.AlbumKey == 9 || item.AlbumKey == 11)
        //        {
        //            SongDataAccess da2 = new SongDataAccess();
        //            List<Song> songs = da2.GetSongsForAlbum(item.AlbumKey);
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
