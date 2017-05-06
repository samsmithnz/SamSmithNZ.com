using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using SamSmithNZ2015.Core.Valor;
//using SamSmithNZ2015.Models.Valor;

namespace SamSmithNZ2017.Controllers
{
    public class ValorController : Controller
    {
        //
        // GET: /Valor/

        public ActionResult Index()
        {

            return Redirect("http://samsmithnz2015.azurewebsites.net/Valor/Index");
            //SamSmithNZ2015.Core.iTunes.DataAccess.PlaylistDataAccess t = new SamSmithNZ2015.Core.iTunes.DataAccess.PlaylistDataAccess();
            //IList<Playlist> PlaylistList = t.GetItems();

            //return View();
        }

        public ActionResult EnemySleepTimesList()
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/Valor/EnemySleepTimesList");
            ////SamSmithNZ2015.Core.iTunes.DataAccess.PlaylistDataAccess t = new SamSmithNZ2015.Core.iTunes.DataAccess.PlaylistDataAccess();
            //List<EnemySleepTime> enemySleepTimeList = new List<EnemySleepTime>();
            //return View(enemySleepTimeList);
        }

        public ActionResult ClaimsList()
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/Valor/EnemySleepTimesList");
            //SamSmithNZ2015.Core.Valor.DataAccess.ClaimDataAccess t = new SamSmithNZ2015.Core.Valor.DataAccess.ClaimDataAccess();
            //List<Claim> claimList = t.GetItems();
            //return View(claimList);
        }

        [HttpPost]
        public ActionResult ClaimsList(string txtSearch)
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/Valor/ClaimsList");
            //SamSmithNZ2015.Core.Valor.DataAccess.ClaimDataAccess t = new SamSmithNZ2015.Core.Valor.DataAccess.ClaimDataAccess();

            ////Get X/Y coords
            //List<Claim> xyClaimList = new List<Claim>();
            //if (txtSearch.IndexOf(',') >= 0)
            //{
            //    string[] results = txtSearch.Split(',');
            //    int x = -1;
            //    int y = -1;
            //    if (results.Length == 2)
            //    {
            //        if (int.TryParse(results[0], out x) == false)
            //        {
            //            x = -1;
            //        }
            //        if (int.TryParse(results[1], out y) == false)
            //        {
            //            y = -1;
            //        }
            //    }
            //    if (x >= 0 && y >= 0)
            //    {
            //        xyClaimList = t.GetItemsByXAndY(x, y);
            //    }
            //}

            ////Get Owners
            //List<Claim> ownerClaimList = t.GetItemsByOwner(txtSearch);

            ////Get Added By
            //List<Claim> addedByClaimList = t.GetItemsByAddedBy(txtSearch);

            ////Combine and return the list
            //List<Claim> claimList = new List<Claim>();
            //foreach (Claim c in xyClaimList)
            //{
            //    claimList.Add(c);
            //}
            //foreach (Claim c in ownerClaimList)
            //{
            //    claimList.Add(c);
            //}
            //foreach (Claim c in addedByClaimList)
            //{
            //    claimList.Add(c);
            //}
            //return View(claimList);
        }

        public ActionResult AddClaim()
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/Valor/AddClaim");
            //SamSmithNZ2015.Core.Valor.DataAccess.GuildDataAccess t = new SamSmithNZ2015.Core.Valor.DataAccess.GuildDataAccess();
            //List<Guild> guildList = t.GetItems();

            //return View(new SamSmithNZ2015.Models.Valor.AddClaimViewModel("", guildList));
        }

        [HttpPost]
        public ActionResult AddClaim(string txtXLocation, string txtYLocation, string txtOwner, string txtAddedBy, string cboGuild)
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/Valor/AddClaim");
            //string errorMessage = "";
            //SamSmithNZ2015.Core.Valor.DataAccess.ClaimDataAccess t = new SamSmithNZ2015.Core.Valor.DataAccess.ClaimDataAccess();
            //Claim c = new Claim();
            //int x = -1;
            //int y = -1;
            //if (int.TryParse(txtXLocation, out x) == true)
            //{
            //    c.ClaimX = x;
            //}
            //else
            //{
            //    x = -1;
            //}
            //if (int.TryParse(txtYLocation, out y) == true)
            //{
            //    c.ClaimY = y;
            //}
            //else
            //{
            //    y = -1;
            //}
            //c.ClaimOwner = txtOwner;
            //c.ClaimAddedBy = txtAddedBy;
            //int guild = -1;
            //if (int.TryParse(cboGuild, out guild) == true)
            //{
            //    c.GuildCode = guild;
            //}
            //else
            //{
            //    guild = -1;
            //}
            //c.ClaimWorldNumber = 1;
            //int timeOffset = 6; //The server is 6 hours behind valor time
            //if (System.Web.HttpContext.Current.Request.IsLocal == true)
            //{
            //    timeOffset = 2; //My laptop is only 4 hours behind.
            //}
            //c.ClaimDate = DateTime.Now.AddHours(timeOffset);

            //if (x == -1 || y == -1)
            //{
            //    errorMessage = "Please add in the coords in the format x,y: for example: 1,2.";
            //}
            //else if (string.IsNullOrEmpty(txtOwner) == true)
            //{
            //    errorMessage = "Please add an owner.";
            //}
            //else if (string.IsNullOrEmpty(txtAddedBy) == true)
            //{
            //    errorMessage = "Please add your name.";
            //}
            //else if (string.IsNullOrEmpty(cboGuild) == true || cboGuild == "0" || guild == -1)
            //{
            //    errorMessage = "Please select a guild.";
            //}
            //else
            //{
            //    SamSmithNZ2015.Core.Valor.DataAccess.ValorTextDataAccess t2 = new SamSmithNZ2015.Core.Valor.DataAccess.ValorTextDataAccess();
            //    ValorText valorMessage = t2.ValidateSave(c);
            //    if (string.IsNullOrEmpty(valorMessage.Result) == true)
            //    {
            //        t.Commit(c);
            //        return RedirectToAction("ClaimsList", "Valor");
            //    }
            //    else
            //    {
            //        errorMessage = valorMessage.Result;
            //    }
            //}

            ////If there is an error message, return it with the guild list
            //SamSmithNZ2015.Core.Valor.DataAccess.GuildDataAccess t3 = new SamSmithNZ2015.Core.Valor.DataAccess.GuildDataAccess();
            //List<Guild> guildList = t3.GetItems();
            //return View(new SamSmithNZ2015.Models.Valor.AddClaimViewModel(errorMessage, guildList));
        }

        public ActionResult DeleteClaim(int ClaimCode)
        {
            //SamSmithNZ2015.Core.Valor.DataAccess.ClaimDataAccess t = new SamSmithNZ2015.Core.Valor.DataAccess.ClaimDataAccess();
            //t.Delete(ClaimCode);
            return RedirectToAction("ClaimsList", "Valor");
        }

        [ChildActionOnly]
        public ActionResult ValorSideBar()
        {
            // Return partial view
            return PartialView();
        }


    }
}
