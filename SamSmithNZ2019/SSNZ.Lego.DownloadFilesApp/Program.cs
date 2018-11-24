using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SSNZ.Lego.DownloadFilesApp
{
    class Program
    {
                static async Task Main(string[] args)
        {
            try
            {
                //Get configuration values
                IConfiguration config = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", true, true)
                      .Build();            
                string legoPartFilesToDownloadURL = config["legoPartFilesToDownloadURL"];
                string csvFilesToDownloadURL = config["csvFilesToDownloadURL"];
                string storageConnectionString = config["storageConnectionString"];
                string tempFolderLocationLegoParts = Path.GetTempPath() + config["tempFolderLocationLegoParts"];
                string tempFolderLocationCSVFiles = Path.GetTempPath() + config["tempFolderLocationCSVFiles"];

                //Confirm that the temp folder can be accessed and is clear
                if (LocalFileManagement.PrepareTempFolderForDownloads(tempFolderLocationLegoParts) == false || LocalFileManagement.PrepareTempFolderForDownloads(tempFolderLocationCSVFiles) == false)
                {
                    PressAKeyToExit("Temp folder preparation failed - process aborted");
                }
                else
                {
                    //Get list of colors from database
                    List<string> csvFiles = GetLegoCSVFiles();
                    //Download all files from URL to temp folder
                    await LocalFileManagement.DownloadFilesToTempFolder(csvFilesToDownloadURL, tempFolderLocationCSVFiles, csvFiles);
                    //Upload files to Azure Blob, if they have changed (checksum?)
                    await AzureBlobManagement.UploadFilesToStorageAccountBlobs(storageConnectionString, "legocsvfiles", tempFolderLocationCSVFiles, csvFiles);

                    //Get list of colors from database
                    List<string> colorFiles = GetColorFiles();
                    //Download all files from URL to temp folder
                    await LocalFileManagement.DownloadFilesToTempFolder(legoPartFilesToDownloadURL, tempFolderLocationLegoParts, colorFiles);
                    //Upload files to Azure Blob, if they have changed (checksum?)
                    await AzureBlobManagement.UploadFilesToStorageAccountBlobs(storageConnectionString, "legopartimages", tempFolderLocationLegoParts, colorFiles);

                    //Bonus: Trigger azure function to start data factory import
                    //Bonus: If new color detected, run this process again

                    PressAKeyToExit("Successfully uploaded!");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                PressAKeyToExit(ex.ToString());
            }
        }

        private static List<string> GetLegoCSVFiles()
        {
            List<string> csvFiles = new List<string>
            {
                "colors.csv",
                "inventories.csv",
                "inventory_parts.csv",
                "inventory_sets.csv",
                "part_categories.csv",
                "part_relationships.csv",
                "parts.csv",
                "sets.csv",
                "themes.csv"
            };
            return csvFiles;
        }

        //TODO: Use a database connection string instead of a static list
        private static List<string> GetColorFiles()
        {
            List<string> colorFiles = new List<string>
            {
                "parts_-1.zip",
                "parts_0.zip",
                "parts_1.zip",
                "parts_2.zip",
                "parts_3.zip",
                "parts_4.zip",
                "parts_5.zip",
                "parts_6.zip",
                "parts_7.zip",
                "parts_8.zip",
                "parts_9.zip",
                "parts_10.zip",
                "parts_11.zip",
                "parts_12.zip",
                "parts_13.zip",
                "parts_14.zip",
                "parts_15.zip",
                "parts_17.zip",
                "parts_18.zip",
                "parts_19.zip",
                "parts_20.zip",
                "parts_21.zip",
                "parts_22.zip",
                "parts_23.zip",
                "parts_25.zip",
                "parts_26.zip",
                "parts_27.zip",
                "parts_28.zip",
                "parts_29.zip",
                "parts_30.zip",
                "parts_31.zip",
                "parts_32.zip",
                "parts_33.zip",
                "parts_34.zip",
                "parts_35.zip",
                "parts_36.zip",
                "parts_40.zip",
                "parts_41.zip",
                "parts_42.zip",
                "parts_43.zip",
                "parts_45.zip",
                "parts_46.zip",
                "parts_47.zip",
                "parts_52.zip",
                "parts_54.zip",
                "parts_57.zip",
                "parts_60.zip",
                "parts_61.zip",
                "parts_62.zip",
                "parts_63.zip",
                "parts_64.zip",
                "parts_68.zip",
                "parts_69.zip",
                "parts_70.zip",
                "parts_71.zip",
                "parts_72.zip",
                "parts_73.zip",
                "parts_74.zip",
                "parts_75.zip",
                "parts_76.zip",
                "parts_77.zip",
                "parts_78.zip",
                "parts_79.zip",
                "parts_80.zip",
                "parts_81.zip",
                "parts_82.zip",
                "parts_84.zip",
                "parts_85.zip",
                "parts_86.zip",
                "parts_89.zip",
                "parts_92.zip",
                "parts_100.zip",
                "parts_110.zip",
                "parts_112.zip",
                "parts_114.zip",
                "parts_115.zip",
                "parts_117.zip",
                "parts_118.zip",
                "parts_120.zip",
                "parts_125.zip",
                "parts_129.zip",
                "parts_132.zip",
                "parts_133.zip",
                "parts_134.zip",
                "parts_135.zip",
                "parts_137.zip",
                "parts_142.zip",
                "parts_143.zip",
                "parts_148.zip",
                "parts_150.zip",
                "parts_151.zip",
                "parts_158.zip",
                "parts_178.zip",
                "parts_179.zip",
                "parts_182.zip",
                "parts_183.zip",
                "parts_191.zip",
                "parts_212.zip",
                "parts_216.zip",
                "parts_226.zip",
                "parts_230.zip",
                "parts_232.zip",
                "parts_236.zip",
                "parts_272.zip",
                "parts_288.zip",
                "parts_294.zip",
                "parts_297.zip",
                "parts_308.zip",
                "parts_313.zip",
                "parts_320.zip",
                "parts_321.zip",
                "parts_322.zip",
                "parts_323.zip",
                "parts_326.zip",
                "parts_334.zip",
                "parts_335.zip",
                "parts_351.zip",
                "parts_366.zip",
                "parts_373.zip",
                "parts_378.zip",
                "parts_379.zip",
                "parts_383.zip",
                "parts_450.zip",
                "parts_462.zip",
                "parts_484.zip",
                "parts_503.zip",
                "parts_1000.zip",
                "parts_1001.zip",
                "parts_1002.zip",
                "parts_1003.zip",
                "parts_1004.zip",
                "parts_1005.zip",
                "parts_1006.zip",
                "parts_1007.zip",
                "parts_1008.zip",
                "parts_1009.zip",
                "parts_1010.zip",
                "parts_1011.zip",
                "parts_9999.zip"
            };

            return colorFiles;
        }

        private static void PressAKeyToExit(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
