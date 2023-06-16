
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



            TimeSpan timeSpan = DateTime.Now - startTime;
            System.Console.WriteLine($"Processing completed in {timeSpan.TotalSeconds.ToString()} seconds");
        }
    }
}