using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace SSNZ.Steam.Data
{
    public static class Utility
    {
        public static string ConvertMinutesToFriendlyTime(long minutes)
        {
            string result = "";

            long hh = minutes / (long)60;
            result = hh + " hrs";

            return result;
        }

        public static string GetPageAsString(Uri address)
        {
            string result = "";
            // Create the web request  
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
            // Get response  
            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream());
                // Read the whole contents and return as a string  
                result = reader.ReadToEnd();

            }
            catch (WebException e)
            {
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
    }
}
