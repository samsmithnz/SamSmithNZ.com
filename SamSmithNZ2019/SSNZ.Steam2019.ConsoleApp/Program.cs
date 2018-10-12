using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using StackExchange.Redis;
using System.Threading.Tasks;
using SSNZ.Steam2019.Service.Models;
using SSNZ.Steam2019.Service.Services;

namespace SSNZ.Steam2019.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config["CacheConnection"];

            using (var cache = ConnectionMultiplexer.Connect(connectionString))
            {
                var db = cache.GetDatabase();

                //bool setValue = db.StringSet("test:key", "some value");
                //Console.WriteLine($"SET: {setValue}");

                //string getValue = db.StringGet("test:key");
                //Console.WriteLine($"GET: {getValue}");

                //setValue = await db.StringSetAsync("test", "100");
                //Console.WriteLine($"SET: {setValue}");

                //getValue = await db.StringGetAsync("test");
                //Console.WriteLine($"GET: {getValue}");

                //var result = await db.ExecuteAsync("ping");
                //Console.WriteLine($"PING = {result.Type} : {result}");

                //result = await db.ExecuteAsync("flushdb");
                //Console.WriteLine($"FLUSHDB = {result.Type} : {result}");

                //// We could use the Newtonsoft.Json library to turn an instance of this object into a string:
                //Steam2019.DA.SteamFriendDA friend = new DA.SteamFriendDA();
                //SteamFriend stat = new SteamFriend();
                //stat.steamid = "abc123";
                //stat.relationship = "relationship";
                //stat.friend_since = 14;

                //string serializedValue = Newtonsoft.Json.JsonConvert.SerializeObject(stat);
                //bool added = await db.StringSetAsync("steamfriend:abc123", serializedValue, expiry: new TimeSpan(0, 0, 10));

                //// We could retrieve it and turn it back into an object using the reverse process:
                //var newresult = await db.StringGetAsync("steamfriend:abc123");
                //var newstat = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamFriend>(newresult.ToString());
                //Console.WriteLine(stat.steamid);
                //Console.WriteLine(stat.relationship);
                //Console.WriteLine(stat.friend_since);


                //Arrange
                SteamGameDetails da = new SteamGameDetails();
                string steamId = "76561197971691578";
                string appId = "200510"; //Xcom
                RedisService redisService = new RedisService(db);
                await da.GetGameDetails(redisService, steamId, appId);
            }
        }
    }
}
