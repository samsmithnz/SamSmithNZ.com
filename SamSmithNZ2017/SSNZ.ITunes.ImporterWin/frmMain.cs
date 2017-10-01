using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using SSNZ.ITunes.Models;
using System.Threading;

namespace SSNZ.ITunes.ImporterWin
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private bool _runningMultipleMonths = false;

        private void btnImport_Click(object sender, EventArgs e)
        {
            btnImport.Enabled = false;
            XmlDocument objXML = new XmlDocument();
            XmlNodeList objXMLNodeList = default(XmlNodeList);
            List<Track> trackList = new List<Track>();
            System.IO.StreamReader objSR = default(System.IO.StreamReader);
            string strXML = null;

            try
            {
                //if (MessageBox.Show("Are you sure you want to import this?", Application.ProductName, MessageBoxButtons.YesNo) != DialogResult.Yes)
                //{
                //    return;
                //}

                UpdateProgress("Importing Playlist...", 0, "");

                objSR = System.IO.File.OpenText(txtFile.Text);
                strXML = objSR.ReadToEnd();
                objSR.Close();

                //Load the Xml Document
                objXML.LoadXml(strXML);

                //Get all the track nodes
                objXMLNodeList = objXML.SelectNodes("/plist/dict/dict");

                for (int i = 0; i <= objXMLNodeList.Count - 1; i++)
                {
                    XmlDocument xmlAllTracks = new XmlDocument();
                    xmlAllTracks.LoadXml(objXMLNodeList.Item(i).OuterXml);

                    for (int j = 0; j <= xmlAllTracks.SelectSingleNode("/dict").ChildNodes.Count - 1; j++)
                    {
                        int percent = Convert.ToInt32((Convert.ToDecimal(j + 1) / Convert.ToDecimal(xmlAllTracks.SelectSingleNode("/dict").ChildNodes.Count) * Convert.ToDecimal(100)));
                        UpdateProgress("importing from xml", percent, "");
                        XmlDocument xmlTrack = new XmlDocument();
                        xmlTrack.LoadXml(xmlAllTracks.SelectSingleNode("/dict").ChildNodes[j].OuterXml);

                        //If it's the track data, then load it up.
                        if ((xmlTrack.SelectSingleNode("/dict") != null))
                        {
                            Track track = new Track();

                            for (int x = 0; x <= xmlTrack.SelectSingleNode("/dict").ChildNodes.Count - 1; x++)
                            {
                                switch (xmlTrack.SelectSingleNode("/dict").ChildNodes[x].InnerText)
                                {
                                    case "Name":
                                        track.TrackName = xmlTrack.SelectSingleNode("/dict").ChildNodes[x + 1].InnerText;
                                        break;
                                    case "Artist":
                                        track.ArtistName = xmlTrack.SelectSingleNode("/dict").ChildNodes[x + 1].InnerText;
                                        break;
                                    case "Album":
                                        track.AlbumName = xmlTrack.SelectSingleNode("/dict").ChildNodes[x + 1].InnerText;
                                        break;
                                    case "Play Count":
                                        int playListCountResult = 0;
                                        if (int.TryParse(xmlTrack.SelectSingleNode("/dict").ChildNodes[x + 1].InnerText, out playListCountResult) == false)
                                        {
                                            playListCountResult = 0;
                                        }
                                        track.PlayCount = playListCountResult;
                                        break;
                                    case "Rating":
                                        int ratingResult = 0;
                                        if (int.TryParse(xmlTrack.SelectSingleNode("/dict").ChildNodes[x + 1].InnerText, out ratingResult) == false)
                                        {
                                            ratingResult = 0;
                                        }
                                        track.Rating = ratingResult;
                                        break;
                                }
                            }
                            trackList.Add(track);
                        }
                    }
                }

                System.IO.FileInfo file = new System.IO.FileInfo(txtFile.Text);
                string[] fileParts = file.Name.Replace(".xml", "").Split('-');
                DateTime newDate = new DateTime(Convert.ToInt32(fileParts[0]), Convert.ToInt32(fileParts[1]), 1);
                int newPlayListCode = 0;
                newPlayListCode = AsyncHelper.RunSync<int>(() => DataAccess.CreateNewPlayList(newDate));
                AsyncHelper.RunSync<bool>(() => DataAccess.DeletePlaylistTracks(newPlayListCode));
                newPlayListCode = AsyncHelper.RunSync<int>(() => DataAccess.CreateNewPlayList(newDate)); //Ok - so we just deleted the playlist. lets recreate it.

                for (int i = 0; i <= trackList.Count - 1; i++)
                {
                    int percent = Convert.ToInt32((Convert.ToDecimal(i + 1) / Convert.ToDecimal(trackList.Count) * Convert.ToDecimal(100)));
                    UpdateProgress("inserting into database", percent, "");
                    trackList[i].PlaylistCode = newPlayListCode;
                    AsyncHelper.RunSync<bool>(() => DataAccess.InsertTrack(trackList[i]));
                }

                UpdateProgress("validating track list...", 90, "");
                List<Track> duplicateTracks = new List<Track>();
                //duplicateTracks = AsyncHelper.RunSync<List<Track>>(() => DataAccess.ValidateTracksForDuplicates(newPlayListCode));

                if (duplicateTracks.Count > 0)
                {
                    string strResult = "";
                    for (int i = 0; i <= duplicateTracks.Count - 1; i++)
                    {
                        strResult = strResult + duplicateTracks[i].ArtistName.ToString() + " - " + duplicateTracks[i].AlbumName.ToString() + " - " + duplicateTracks[i].TrackName.ToString() + Environment.NewLine;
                    }
                    //MsgBox("Error: there are duplicate tracks: " & vbCr & strResult & vbCr & vbCr & "Correct these to import in this playlist...")
                    Debug.WriteLine("Duplicate tracks detected " + Environment.NewLine + strResult);
                    //DataAccess.DeletePlaylistAndTracks(newPlayListCode)
                    //UpdateProgress("Duplicate tracks detected, removing playlist...", 95, "")
                    //MsgBox("Playlist and tracks successfully deleted!")
                    //Me.Close()
                    //Exit Sub
                }

                UpdateProgress("updating ranking...", 100, "");
                AsyncHelper.RunSync<bool>(() => DataAccess.SetTrackRanks(newPlayListCode));

                if (_runningMultipleMonths == false)
                {
                    MessageBox.Show("Done!");
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (_runningMultipleMonths == false)
                {
                    btnImport.Enabled = true;
                }
            }

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.InitialDirectory = txtFile.Text;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = OpenFileDialog1.FileName;
            }
        }

        private void UpdateProgress(string status, int percent, string something)
        {
            lblStatus.Text = status + " " + percent.ToString() + "%";
            prgStatus.Value = percent;
            Debug.WriteLine(percent.ToString() + ":" + lblStatus.Text);
            Application.DoEvents();
        }

        private void btnFillInPlayListGaps_Click(object sender, EventArgs e)
        {
            try
            {
                btnFillInPlayListGaps.Enabled = false;
                btnImport.Enabled = false;
                if (MessageBox.Show("Are you sure you want to fill in all gaps? This could take awhile...", Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _runningMultipleMonths = true;
                    string originalPath = txtFile.Text;

                    //Get all playlists
                    List<Playlist> playLists = AsyncHelper.RunSync<List<Playlist>>(() => DataAccess.GetPlayLists());

                    //Get all files
                    string[] files = System.IO.Directory.GetFiles(txtFile.Text);

                    //Loop through playlists compared to the files in the folder, looking for files that haven't ben uploaded
                    foreach (string file in files)
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                        string fileNameWithNoExtension = fileInfo.Name.Replace(".xml", "");
                        int year = int.Parse(fileNameWithNoExtension.Split('-')[0]);
                        if (year >= 2015) //Because the old file format is MMM instead of MM
                        {
                            int month = int.Parse(fileNameWithNoExtension.Split('-')[1]);
                            Playlist playList = FindPlayList(year, month, playLists);

                            if (playList == null)
                            {
                                //load these missing files
                                txtFile.Text = txtFile.Text + year.ToString() + "-" + month.ToString("00") + ".xml";
                                btnImport_Click(null, null);
                            }
                        }
                        txtFile.Text = originalPath;
                    }
                    MessageBox.Show("Done!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnFillInPlayListGaps.Enabled = true;
                btnImport.Enabled = true;
                _runningMultipleMonths = false;
            }


        }
        private static Playlist FindPlayList(int year, int month, List<Playlist> playLists)
        {
            foreach (Playlist item in playLists)
            {
                if (item.PlaylistDate.Year == year && item.PlaylistDate.Month == month)
                {
                    return item;
                }
            }
            return null;
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
