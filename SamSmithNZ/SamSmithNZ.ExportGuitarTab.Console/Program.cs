using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.Models.GuitarTab;

namespace SamSmithNZ.ExportGuitarTab.Console
{
    internal class Program
    {
        private async static Task Main()
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
            //List<Artist> artists = await artistDataAccess.GetList(true, true);
            List<Album> albums = await albumDataAccess.GetList(true);

            foreach (Album album in albums)
            {
                if (album.ArtistName == "Foo Fighters")
                {
                    continue;
                }
                System.Console.WriteLine($"Processing album {album.AlbumName}");

                await ProcessAlbum(targetDirectory, album, configuration);
                if (album.BassAlbumCode != 0)
                {
                    Album baseAlbum = await albumDataAccess.GetItem(album.BassAlbumCode, true);
                    await ProcessAlbum(targetDirectory, baseAlbum, configuration);
                }
            }

            TimeSpan timeSpan = DateTime.Now - startTime;
            System.Console.WriteLine($"Processing completed in {timeSpan.TotalSeconds} seconds");
        }

        private async static Task ProcessAlbum(string targetDirectory,
            Album album,
            IConfiguration configuration)
        {
            //Get the tabs in order of track number
            TabDataAccess tabDataAccess = new(configuration);
            List<Tab> tabs = await tabDataAccess.GetList(album.AlbumCode, 0);

            //strip out invalid directory characters
            string artistName = StripOutInvalidWindowsNaming(album.ArtistName);
            string albumName = StripOutInvalidWindowsNaming(album.AlbumName);
            string isBassAlbum = "";
            if (album.IsBassTab == true)
            {
                isBassAlbum = "_Bass";
            }
            string albumDirectory = targetDirectory + "\\" + artistName + "\\" + albumName + isBassAlbum;
            //Create the album directory if it doesn't exist
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