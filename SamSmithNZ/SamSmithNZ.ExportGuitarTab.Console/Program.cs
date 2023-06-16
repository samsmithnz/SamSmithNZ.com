using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.DataAccess.ITunes;
using SamSmithNZ.Service.Models.GuitarTab;

namespace SamSmithNZ.ExportGuitarTab.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DateTime startTime = DateTime.Now;
            string targetDirectory = @"c:\temp\GuitarTab";
            if (Directory.Exists(targetDirectory) == false)
            {
                System.Console.WriteLine($"Creating new directory '{targetDirectory}'");
                Directory.CreateDirectory(targetDirectory);
            }

            //Load the appsettings.json configuration file
            IConfigurationBuilder? builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false)
                 .AddUserSecrets<Program>(true);
            IConfigurationRoot configuration = builder.Build();

            //Get all artists and their albums
            //ArtistDataAccess artistDataAccess = new(configuration);
            AlbumDataAccess albumDataAccess = new(configuration);
            TabDataAccess tabDataAccess = new(configuration);
            //List<Artist> artists = await artistDataAccess.GetList(true, true);
            List<Album> albums = await albumDataAccess.GetList(true);

            foreach (Album album in albums)
            {
                System.Console.WriteLine($"Processing album {album.AlbumName}");
                //Get the tabs in order of track number
                List<Tab> tabs = await tabDataAccess.GetList(album.AlbumCode, 0);

                //Create the album directory if it doesn't exist
                //strip out invalid file name characters
                string artistName = StripOutInvalidWindowsNaming(album.ArtistName);
                string albumName = StripOutInvalidWindowsNaming(album.AlbumName);
                string albumDirectory = targetDirectory + "\\" + artistName + "\\" + albumName;
                if (Directory.Exists(albumDirectory) == false)
                {
                    System.Console.WriteLine($"Creating new directory '{albumDirectory}'");
                    Directory.CreateDirectory(albumDirectory);
                }

                //Create a file for each tab
                foreach (Tab tab in tabs)
                {
                    string tabName = tab.TabName;
                    //strip out invalid file name characters
                    tabName = StripOutInvalidWindowsNaming(tabName);
                    string tabFileName = albumDirectory + "\\" + tab.TabOrder.ToString("00") + "." + tabName + ".txt";

                    if (File.Exists(tabFileName) == false)
                    {
                        System.Console.WriteLine($"Creating new file '{tabFileName}'");
                        using (StreamWriter sw = File.CreateText(tabFileName))
                        {
                            sw.WriteLine(tab.TabText);
                        }
                    }
                    else
                    {
                        //Compare the existing file contents with the database contents
                        string existingFileContents = File.ReadAllText(tabFileName);
                        if (existingFileContents != tab.TabText + "\r\n")
                        {
                            System.Console.WriteLine($"Updating file '{tabFileName}'");
                            using (StreamWriter sw = File.CreateText(tabFileName))
                            {
                                sw.WriteLine(tab.TabText);
                            }
                        }
                    }

                }

            }

            TimeSpan timeSpan = DateTime.Now - startTime;
            System.Console.WriteLine($"Processing completed in {timeSpan.TotalSeconds.ToString()} seconds");
        }

        private static string StripOutInvalidWindowsNaming(string name)
        {
            name = name.Replace(":", "");
            name = name.Replace("?", "");
            name = name.Replace("\"", "");
            name = name.Replace("/", "");
            name = name.Replace("\\", "");
            name = name.Replace("*", "");
            name = name.Replace("<", "");
            name = name.Replace(">", "");
            name = name.Replace("|", "");
            return name;
        }
    }
}