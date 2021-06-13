namespace SamSmithNZ.Web.Models.GameDev
{
    public class Utility
    {

        /// <summary>
        /// Generate a random number (int) within lower and upper bounds
        /// </summary>
        private readonly static System.Random rnd = new();
        public static int GenerateRandomNumber(int lowerBound, int upperBound)
        {
            int result = rnd.Next(lowerBound, upperBound + 1);//+1 because the upperbound is never used
            //Debug.LogWarning("GenerateRandomNumber (range " + lowerBound + "-" + upperBound + "): " + result);
            return result;
        }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        //public static string GetPageAsString(Uri address)
        //{
        //    string result = "";
        //    // Create the web request  
        //    HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
        //    // Get response  
        //    try
        //    {
        //        using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        //        {
        //            // Get the response stream  
        //            StreamReader reader = new StreamReader(response.GetResponseStream());
        //            // Read the whole contents and return as a string  
        //            result = reader.ReadToEnd();
        //        }
        //    }
        //    catch (WebException e)
        //    {
        //        using (WebResponse response = e.Response)
        //        {
        //            HttpWebResponse httpResponse = (HttpWebResponse)response;
        //            if (httpResponse != null)
        //            {
        //                System.Diagnostics.Debug.WriteLine("Error code: {0}", httpResponse.StatusCode);
        //                using (Stream data = response.GetResponseStream())
        //                using (StreamReader reader = new StreamReader(data))
        //                {
        //                    string text = reader.ReadToEnd();
        //                    Console.WriteLine(text);
        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}

        //#region " Steam Helper Functions"

        //public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        //{
        //    // Unix timestamp is seconds past epoch
        //    System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //    dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
        //    return dtDateTime;
        //}

        //public static bool SaveRequestURL(Guid batchID, string rawRequestString)
        //{
        //    //SteamWebAPIUsageDataAccess da = new SteamWebAPIUsageDataAccess();
        //    //da.Save(batchID, rawRequestString);
        //    //System.Diagnostics.Debug.WriteLine(APICounter + ". " + rawRequestString);

        //    return true;
        //}

        //public static T LoadXML<T>(string xmlString)
        //{
        //    if (string.IsNullOrEmpty(xmlString) == false)
        //    {
        //        // convert string to stream
        //        byte[] byteArray = Encoding.UTF8.GetBytes(xmlString);
        //        MemoryStream stream = new MemoryStream(byteArray);

        //        //load into a streamreader and then deserialize
        //        StreamReader streamReader = new StreamReader(stream);
        //        XmlSerializer reader = new XmlSerializer(typeof(T));
        //        return (T)reader.Deserialize(streamReader);
        //    }
        //    else
        //    {
        //        return default;
        //    }
        //}

        //#endregion

    }
}
