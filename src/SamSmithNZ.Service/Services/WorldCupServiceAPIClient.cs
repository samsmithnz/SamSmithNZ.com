using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Services
{
    public class WorldCupServiceAPIClient
    {
        private readonly HttpClient _client;

        public WorldCupServiceAPIClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Odds>> GetOddsForTournament(int tournamentCode)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/worldcup/odds/GetOddsForTournament?tournamentCode={tournamentCode}");
            response.EnsureSuccessStatusCode();
            List<Odds> odds = await response.Content.ReadAsAsync<List<Odds>>();
            return odds;
        }
    }
}
