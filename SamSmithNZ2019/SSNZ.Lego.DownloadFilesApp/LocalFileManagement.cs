using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Lego.DownloadFilesApp
{
    public static class LocalFileManagement
    {
        public static bool PrepareTempFolderForDownloads(string tempFolderLocation)
        {
            //Check that the folder is empty, deleting it if it already exists
            if (Directory.Exists(tempFolderLocation) == true)
            {
                Directory.Delete(tempFolderLocation, true);
            }

            //Create the new folder
            Directory.CreateDirectory(tempFolderLocation);

            return true;
        }

        public async static Task<bool> DownloadFilesToTempFolder(string imageToDownloadURL, string tempFolderLocation, List<string> Files)
        {
            foreach (string file in Files)
            {
                string fileToDownload = imageToDownloadURL + file;
                Console.WriteLine("Downloading file '" + fileToDownload + "'");
                //TODO: Fix this retry loop
                for (int retries = 0; retries < 5; retries++)
                {
                    try
                    {
                        byte[] fileBytes = await DownloadFile(fileToDownload);
                        File.WriteAllBytes(tempFolderLocation + @"\" + file, fileBytes);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed saving file - let's try again... (" + (retries + 1).ToString() + "/5");
                    }
                }
            }

            return true;
        }

        //Download the file
        private static async Task<byte[]> DownloadFile(string url)
        {
            //TODO: Fix this retry loop
            for (int retries = 0; retries < 5; retries++)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage result = await client.GetAsync(url))
                        {
                            if (result.IsSuccessStatusCode)
                            {
                                return await result.Content.ReadAsByteArrayAsync();
                            }
                        }
                    }
                }
                catch
                {
                    //do nothing, try again!
                    Console.WriteLine("Failed download - let's try again... (" + (retries + 1).ToString() + "/5");
                }
            }

            return null;
        }
    }
}
