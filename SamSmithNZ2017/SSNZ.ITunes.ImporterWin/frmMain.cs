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

namespace SSNZ.ITunes.ImporterWin
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            txtFile.Text = txtFile.Text + "2014-08.xml";
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            XmlDocument objXML = new XmlDocument();
            XmlNodeList objXMLNodeList = default(XmlNodeList);
            List<ITunesTrack> iTunesTrackList = new List<ITunesTrack>();
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
                            ITunesTrack itunesTrack = new ITunesTrack();

                            for (int x = 0; x <= xmlTrack.SelectSingleNode("/dict").ChildNodes.Count - 1; x++)
                            {
                                switch (xmlTrack.SelectSingleNode("/dict").ChildNodes[x].InnerText)
                                {
                                    case "Name":
                                        itunesTrack.TrackName = xmlTrack.SelectSingleNode("/dict").ChildNodes[x + 1].InnerText;
                                        break;
                                    case "Artist":
                                        itunesTrack.ArtistName = xmlTrack.SelectSingleNode("/dict").ChildNodes[x + 1].InnerText;
                                        break;
                                    case "Album":
                                        itunesTrack.AlbumName = xmlTrack.SelectSingleNode("/dict").ChildNodes[x + 1].InnerText;
                                        break;
                                    case "Play Count":
                                        itunesTrack.PlayCount = xmlTrack.SelectSingleNode("/dict").ChildNodes[x + 1].InnerText;
                                        if (string.IsNullOrEmpty(itunesTrack.PlayCount))
                                        {
                                            itunesTrack.PlayCount = "0";
                                        }
                                        break;
                                    case "Rating":
                                        itunesTrack.Rating = xmlTrack.SelectSingleNode("/dict").ChildNodes[x + 1].InnerText;
                                        break;
                                }
                            }
                            iTunesTrackList.Add(itunesTrack);
                        }
                    }
                }

                //clsOldDataAccess DataAccess = new clsOldDataAccess();
                System.IO.FileInfo file = new System.IO.FileInfo(txtFile.Text);
                string[] fileParts = file.Name.Replace(".xml", "").Split('-');
                DateTime newDate = new DateTime(Convert.ToInt32(fileParts[0]), Convert.ToInt32(fileParts[1]), 1);
                int intNewCode = 0;
                intNewCode = DataAccess.CreateNewPlayList(newDate);

                DataAccess.DeletePlaylistTracks(intNewCode);
                //TODO: Enable again
                //for (int i = 0; i <= objArray.Count - 1; i++)
                //{
                //    UpdateProgress("inserting into database", Convert.ToInt32(((i + 1) / objArray.Count * 100)), "");
                //    DataAccess.InsertTrack(intNewCode, (ITunesTrack)objArray[i]);
                //}

                UpdateProgress("validating track list...", 90, "");
                //TODO: Enable again
                //DataSet dsData = DataAccess.ValidateTracksForDuplicates(intNewCode);
                //if (dsData.Tables[0].Rows.Count > 0)
                //{
                //    string strResult = "";
                //    for (int i = 0; i <= dsData.Tables[0].Rows.Count - 1; i++)
                //    {
                //        var _with2 = dsData.Tables[0].Rows[i];
                //        strResult = strResult + _with2.Item["artist_name"].ToString + " - " + _with2.Item["album_name"].ToString + " - " + _with2.Item["track_name"].ToString + Environment.NewLine;
                //    }
                //    //MsgBox("Error: there are duplicate tracks: " & vbCr & strResult & vbCr & vbCr & "Correct these to import in this playlist...")
                //    Debug.WriteLine("Duplicate tracks detected " + Environment.NewLine + strResult);
                //    //DataAccess.DeletePlaylistAndTracks(intNewCode)
                //    //UpdateProgress("Duplicate tracks detected, removing playlist...", 95, "")
                //    //MsgBox("Playlist and tracks successfully deleted!")
                //    //Me.Close()
                //    //Exit Sub
                //}

                UpdateProgress("updating ranking...", 100, "");
                DataAccess.SetTrackRanks(intNewCode);

                MessageBox.Show("Done!");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

    }
}
