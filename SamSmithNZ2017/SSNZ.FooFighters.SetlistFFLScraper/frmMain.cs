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

                SongDataAccess da2 = new SongDataAccess();
                List<Song> songs = new List<Song>();
                songs = AsyncHelper.RunSync<List<Song>>(() => da2.GetListAsync());

                int i = 0;
                foreach (Show item in shows)
                {
                    if (item.NumberOfSongsPlayed == 0)
                    {
                        //Process the show
                        Tuple<Show, List<int>> processedShow = ProcessURL(item, songs);

                        if (processedShow.Item1 != null)
                        {
                            //Save the show
                            AsyncHelper.RunSync<bool>(() => da.SaveItemAsync(processedShow.Item1));

                            //Save the setlist for the show
                            int j = 0;
                            if (processedShow.Item2 != null)
                            {
                                foreach (int songCode in processedShow.Item2)
                                {
                                    j++;
                                    AsyncHelper.RunSync<bool>(() => da2.SaveItemAsync(songCode, processedShow.Item1.ShowCode, j));
                                }
                            }
                            Debug.Write("Added " + j + " songs to show " + processedShow.Item1.ShowDate);
                        }
                    }

                    int percent = Convert.ToInt32((Convert.ToDecimal(i + 1) / Convert.ToDecimal(shows.Count) * Convert.ToDecimal(100)));
                    UpdateProgress("Processing show", percent, "");
                    i++;
                }
                MessageBox.Show("All done!");
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

        private static Tuple<Show, List<int>> ProcessURL(Show item, List<Song> songs)
        {
            bool processingShowSuccessful = true;
            bool processingSetlistSuccessful = true;
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = htmlWeb.Load(item.FFLURL);

            //<h1 class="show-hero-venue">The Belly Up</h1>
            HtmlNodeCollection locationHTML = doc.DocumentNode.SelectNodes("//h1[contains(@class, 'show-hero-venue')]");
            if (locationHTML == null)
            {
                Debug.WriteLine("What the million?");
            }
            else if (locationHTML.Count > 0)
            {
                item.ShowLocation = locationHTML[0].InnerText;
            }

            //<span class="show-hero-details">8th January 2015 • Solana Beach, CA, United States</span>
            HtmlNodeCollection dateCityCountryHTML = doc.DocumentNode.SelectNodes("//span[contains(@class, 'show-hero-details')]");
            if (dateCityCountryHTML.Count > 0)
            {
                string[] dateAndCity = dateCityCountryHTML[0].InnerText.Split(new string[] { "&bull;" }, StringSplitOptions.None);
                if (dateAndCity.Length <= 1)
                {
                    processingShowSuccessful = false;
                    Debug.WriteLine("What the million?");
                }
                string cityAndCountryString = dateAndCity[1];
                string[] cityandCountryArray = cityAndCountryString.Split(',');
                if (cityandCountryArray.Length == 3)
                {
                    item.ShowCity = (cityandCountryArray[0] + ", " + cityandCountryArray[1]).Trim();
                    item.ShowCountry = cityandCountryArray[2].Trim();
                }
                else if (cityandCountryArray.Length == 2)
                {
                    item.ShowCity = cityandCountryArray[0].Trim();
                    item.ShowCountry = cityandCountryArray[1].Trim();
                }
                else //if (cityandCountryArray.Length == 2)
                {
                    processingShowSuccessful = false;
                    Debug.WriteLine("What the million?");
                }
            }

            //<ol class="setlist-entry">
            //<li><a href="/song/times-like-these-104">Times Like These</a></li>
            //<li><a href="/song/learn-to-fly-51">Learn to Fly</a></li>
            //<li><a href="/song/arlandria-162">Arlandria</a></li><li><a href="/song/my-hero-19">My Hero</a></li><li><a href="/song/monkey-wrench-14">Monkey Wrench</a></li><li><a href="/song/congregation-315">Congregation</a></li><li><a href="/song/walk-168">Walk</a></li><li><a href="/song/cold-day-in-the-sun-99">Cold Day In The Sun</a></li><li><a href="/song/in-the-clear-318">In The Clear</a></li><li><a href="/song/big-me-3">Big Me</a></li><li><a href="/song/miss-you-311">Miss You</a></li><li><a href="/song/breakdown-299">Breakdown</a></li><li><a href="/song/ain-t-talkin-bout-love-221">Ain't Talkin' 'Bout Love</a></li><li><a href="/song/under-pressure-210">Under Pressure</a></li><li><a href="/song/all-my-life-101">All My Life</a></li><li><a href="/song/this-is-a-call-1">This Is A Call</a></li><li><a href="/song/trombone-shorty-jam-337">Trombone Shorty jam</a></li><li><a href="/song/everlong-23">Everlong</a></li>      </ol>

            //<ol class="setlist-entry">
            //<div class="center"><p>No setlist data available for this performance.</p><p>Please <a href="#">contact us</a> if you can help.</p></div>      </ol>

            HtmlNodeCollection setlistHTML = doc.DocumentNode.SelectNodes("//ol[contains(@class, 'setlist-entry')]");
            List<int> setListSongs = new List<int>();
            if (setlistHTML.Count > 0 && setlistHTML[0].ChildNodes.Count > 0)
            {
                foreach (HtmlNode nodeItem in setlistHTML[0].ChildNodes)
                {
                    string songName = nodeItem.InnerText;

                    if (songName.Trim().Length > 0 &&
                        songName.Trim() != "\n" &&
                        songName.Trim() != "No setlist data available for this performance.Please contact us if you can help.")
                    {

                        songName = songName.Replace("Part Of", "").Replace("Jam", "");
                        switch (songName)
                        {
                            //case "Darling NikkiPart Of":
                            //    songName = "Darling Nikki";
                            //    break;
                            case "What Did I Do? / God As My Witness":
                            case "What Did I Do? /God As My Witness":
                                songName = "What Did I Do?/God as My Witness";
                                break;
                            case "The Pretenderw/ part of 'Outside' in the middle":
                                songName = "The Pretender";
                                break;
                            case "Breakoutw/ part of 'Message In a Bottle' in the middle":
                                songName = "Breakout";
                                break;
                        }

                        int songCode = FindSong(songName, songs);
                        if (songCode > 0)
                        {
                            setListSongs.Add(songCode);
                        }
                        else
                        {
                            processingSetlistSuccessful = false;
                            Debug.WriteLine("What the million? Song " + songName + " is not known");
                        }
                    }
                }
                //item.ShowLocation = setlistHTML[0].InnerText;
            }
            if (processingShowSuccessful == false)
            {
                item = null;
            }
            if (processingSetlistSuccessful == false)
            {
                setListSongs = null;
            }
            return new Tuple<Show, List<int>>(item, setListSongs);
        }

        private static int FindSong(string songName, List<Song> songs)
        {
            foreach (Song item in songs)
            {
                if (item.SongName.ToLower() == songName.ToLower())
                {
                    return item.SongCode;
                }
            }
            return 0;
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