using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace SSNZ.Steam2019.Service.DataAccess
{
    public static class Utility
    {
        //https://steamcommunity.com/dev
        //https://developer.valvesoftware.com/wiki/Steam_Web_API#GetPlayerSummaries_.2v0001.29
        //https://portablesteamwebapi.codeplex.com/documentation
        public static string MySteamWebAPIKey = "35D42236AAC777BEDB12CDEB625EF289";

        public static string ConvertMinutesToFriendlyTime(long minutes)
        {
            string result = "";

            long hh = minutes / (long)60;
            result = hh + " hrs";

            return result;
        }

        //A generic object
        public async static Task<string> GetPageAsStringAsync(Uri address)
        {
            string result = "";
            // Create the web request  
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
            // Get response  
            try
            {
                HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream());
                // Read the whole contents and return as a string  
                result = reader.ReadToEnd();
                //Convert all http calls to https
                //result = result.Replace("http://", "https://");
                Debug.WriteLine("Getting item from external API: " + address.ToString());
            }
            catch (WebException e)
            {
                //An error occured, capture the error response 
                WebResponse response = e.Response;
                HttpWebResponse httpResponse = (HttpWebResponse)response;
                if (httpResponse != null)
                {
                    System.Diagnostics.Debug.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    Stream data = null;

                    data = response.GetResponseStream();
                    StreamReader reader = new StreamReader(data);

                    string text = reader.ReadToEnd();
                    Console.WriteLine(text);
                    result = text;
                }
            }
            return result;
        }

        //public static string GetPageAsStringOld(Uri address)
        //{
        //    string result = "";
        //    // Create the web request  
        //    HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
        //    // Get response  
        //    try
        //    {
        //        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        //        // Get the response stream  
        //        StreamReader reader = new StreamReader(response.GetResponseStream());
        //        // Read the whole contents and return as a string  
        //        result = reader.ReadToEnd();
        //        //Convert all http calls to https
        //        //result = result.Replace("http://", "https://");
        //    }
        //    catch (WebException e)
        //    {
        //        //An error occured, capture the error response 
        //        WebResponse response = e.Response;
        //        HttpWebResponse httpResponse = (HttpWebResponse)response;
        //        if (httpResponse != null)
        //        {
        //            System.Diagnostics.Debug.WriteLine("Error code: {0}", httpResponse.StatusCode);
        //            Stream data = null;

        //            data = response.GetResponseStream();
        //            StreamReader reader = new StreamReader(data);

        //            string text = reader.ReadToEnd();
        //            Console.WriteLine(text);
        //            result = text;
        //        }
        //    }
        //    return result;
        //}
    }
}
