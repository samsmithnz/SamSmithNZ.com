using SSNZ.FooFighters.Data;
using SSNZ.FooFighters.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using HtmlAgilityPack;
using System.Linq;

namespace SSNZ.FooFighters.SetlistFFLScraperWin
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            btnImport.Enabled = false;

            try
            {
                ShowDataAccess da = new ShowDataAccess();
                List<Show> shows = new List<Show>();
                shows = AsyncHelper.RunSync<List<Show>>(() => da.GetListByFFLCodeAsync());

                int i = 0;
                foreach (Show item in shows)
                {
                    //Process the show
                    Show processedShow = ProcessURL(item);
                    //Save the show
                    //AsyncHelper.RunSync<bool>(() => da.SaveItemAsync(processedShow));

                    int percent = Convert.ToInt32((Convert.ToDecimal(i + 1) / Convert.ToDecimal(shows.Count) * Convert.ToDecimal(100)));
                    UpdateProgress("Processing show", percent, "");
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnImport.Enabled = true;
            }
        }

        private static Show ProcessURL(Show item)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = htmlWeb.Load(item.FFLURL);


            //<h1 class="show-hero-venue">The Belly Up</h1>
            HtmlNodeCollection locationHTML = doc.DocumentNode.SelectNodes("//h1[contains(@class, 'show-hero-venue')]");
            if (locationHTML.Count > 0)
            {
                item.ShowLocation = locationHTML[0].InnerText;
            }

            //<span class="show-hero-details">8th January 2015 • Solana Beach, CA, United States</span>
            HtmlNodeCollection dateCityCountryHTML = doc.DocumentNode.SelectNodes("//span[contains(@class, 'show-hero-details')]");
            if (dateCityCountryHTML.Count > 0)
            {
                string[] dateAndCity = dateCityCountryHTML[0].InnerText.Split('•');
                string cityAndCountryString = dateAndCity[1];
                string[] cityandCountryArray = cityAndCountryString.Split(',');
                if (cityandCountryArray.Length == 2)
                {
                    Debug.WriteLine("What the million");
                }
                else if (cityandCountryArray.Length == 3)
                {

                }

                //item.ShowCity = "";
                //item.ShowCountry = "";
            }

            //<ol class="setlist-entry">
            //<li><a href="/song/times-like-these-104">Times Like These</a></li><li><a href="/song/learn-to-fly-51">Learn to Fly</a></li><li><a href="/song/arlandria-162">Arlandria</a></li><li><a href="/song/my-hero-19">My Hero</a></li><li><a href="/song/monkey-wrench-14">Monkey Wrench</a></li><li><a href="/song/congregation-315">Congregation</a></li><li><a href="/song/walk-168">Walk</a></li><li><a href="/song/cold-day-in-the-sun-99">Cold Day In The Sun</a></li><li><a href="/song/in-the-clear-318">In The Clear</a></li><li><a href="/song/big-me-3">Big Me</a></li><li><a href="/song/miss-you-311">Miss You</a></li><li><a href="/song/breakdown-299">Breakdown</a></li><li><a href="/song/ain-t-talkin-bout-love-221">Ain't Talkin' 'Bout Love</a></li><li><a href="/song/under-pressure-210">Under Pressure</a></li><li><a href="/song/all-my-life-101">All My Life</a></li><li><a href="/song/this-is-a-call-1">This Is A Call</a></li><li><a href="/song/trombone-shorty-jam-337">Trombone Shorty jam</a></li><li><a href="/song/everlong-23">Everlong</a></li>      </ol>

            //<ol class="setlist-entry">
            //<div class="center"><p>No setlist data available for this performance.</p><p>Please <a href="#">contact us</a> if you can help.</p></div>      </ol>
            HtmlNodeCollection setlistHTML = doc.DocumentNode.SelectNodes("//h1[contains(@class, 'show-hero-venue')]");
            if (setlistHTML.Count > 0)
            {
                //item.ShowLocation = setlistHTML[0].InnerText;
            }


            return item;
        }

        private void UpdateProgress(string status, int percent, string something)
        {
            lblStatus.Text = status + " " + percent.ToString() + "%";
            prgStatus.Value = percent;
            Debug.WriteLine(percent.ToString() + ":" + lblStatus.Text);
            Application.DoEvents();
        }
    }
}



internal static class AsyncHelper
{
    private static readonly TaskFactory _myTaskFactory = new
      TaskFactory(CancellationToken.None,
                  TaskCreationOptions.None,
                  TaskContinuationOptions.None,
                  TaskScheduler.Default);

    public static TResult RunSync<TResult>(Func<Task<TResult>> func)
    {
        return AsyncHelper._myTaskFactory
          .StartNew<Task<TResult>>(func)
          .Unwrap<TResult>()
          .GetAwaiter()
          .GetResult();
    }

    public static void RunSync(Func<Task> func)
    {
        AsyncHelper._myTaskFactory
          .StartNew<Task>(func)
          .Unwrap()
          .GetAwaiter()
          .GetResult();
    }
}