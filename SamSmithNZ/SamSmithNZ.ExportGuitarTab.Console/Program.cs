
using Microsoft.Extensions.Configuration;

namespace SamSmithNZ.ExportGuitarTab.Console
{
    internal class Program
    {
        static void Main(string[] args)
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


            TimeSpan timeSpan = DateTime.Now - startTime;
            System.Console.WriteLine($"Processing completed in {timeSpan.TotalSeconds.ToString()} seconds");
        }
    }
}