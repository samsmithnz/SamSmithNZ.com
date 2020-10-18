using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services
{
    public class BaseServiceAPIClient
    {
        private HttpClient _client;
        public void SetupClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<T>> ReadMessageList<T>(Uri url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode == true)
            {
                return await JsonSerializer.DeserializeAsync<List<T>>(await response.Content.ReadAsStreamAsync());
            }
            else
            {
                //Handle when the url is missing, preventing a 400 error.
#pragma warning disable CS8603 // Possible null reference return.
                return default;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        public async Task<T> ReadMessageItem<T>(Uri url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode == true)
            {
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
            }
            else
            {
                //Handle when the url is missing, preventing a 400 error.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
                return default;
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.
#pragma warning restore CS8603 // Possible null reference return.
            }
        }
    }
}
