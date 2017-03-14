using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SSNZ.Steam.Models;
using System.Net;
using System.IO;

namespace SSNZ.Steam.Data
{
    public class SteamPlayerAchievementsForAppDA
    {
        //http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=200510&key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&l=en&format=xml
        public SteamPlayerAchievementsForApp GetData(string steamID, string appID)
        {

            string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=" + appID.ToString() + "&key=" + Global.MySteamWebAPIKey + "&steamid=" + steamID + "&l=en";
            //WebClient newClient = new WebClient();
            //newClient.Encoding = UTF8Encoding.UTF8;
            //string jsonData = newClient.DownloadString(jsonRequestString);
            string jsonData = GetPageAsString(new Uri(jsonRequestString));

            if (jsonData.IndexOf("{\n\t\t\"error\": \"Requested app has no stats\",\n\t\t\"success\": false\n\t}\n}") >= 0)
            {
                return null;
            }
            else
            {
                SteamPlayerAchievementsForApp result = JsonConvert.DeserializeObject<SteamPlayerAchievementsForApp>(jsonData);
                return result;
            }
        }

        public static string GetPageAsString(Uri address)
        {
            string result = "";
            // Create the web request  
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
            // Get response  
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream  
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    // Read the whole contents and return as a string  
                    result = reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    if (httpResponse != null)
                    {
                        System.Diagnostics.Debug.WriteLine("Error code: {0}", httpResponse.StatusCode);
                        using (Stream data = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            Console.WriteLine(text);
                            return text;
                        }
                    }
                }
            }
            return result;
        }
    }
}
