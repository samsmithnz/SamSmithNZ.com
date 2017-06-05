using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;
using System.Threading.Tasks;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SteamPlayerDetailUnitTest
    {
        [TestMethod]
        public async Task SteamPlayerDetailExistUnitTest()
        {
            //Arrange
            string steamId = "76561197974118008";

            //Act
            SteamPlayerDetailDA da = new SteamPlayerDetailDA();
            SteamPlayerDetail result = await da.GetDataAsync(steamId);

            //Asset
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.response != null);
            Assert.IsTrue(result.response.players != null);
            Assert.IsTrue(result.response.players.Count >= 1);
            Assert.IsTrue(result.response.players[0].steamid == "76561197974118008");
            Assert.IsTrue(result.response.players[0].communityvisibilitystate == 3);
            Assert.IsTrue(result.response.players[0].profilestate == 1);
            Assert.IsTrue(result.response.players[0].personaname == "nosilleg");
            Assert.IsTrue(result.response.players[0].lastlogoff >= 1392847930);
            Assert.IsTrue(result.response.players[0].commentpermission == 2);
            Assert.IsTrue(result.response.players[0].profileurl == "https://steamcommunity.com/id/nosilleg/");
            Assert.IsTrue(result.response.players[0].avatar == "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/85/8522d758971df1d6c4046cca81e8a364315388f2.jpg");
            Assert.IsTrue(result.response.players[0].avatarmedium == "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/85/8522d758971df1d6c4046cca81e8a364315388f2_medium.jpg");
            Assert.IsTrue(result.response.players[0].avatarfull == "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/85/8522d758971df1d6c4046cca81e8a364315388f2_full.jpg");
            Assert.IsTrue(result.response.players[0].personastate >= 0);
            Assert.IsTrue(result.response.players[0].primaryclanid == "103582791433470748");
            Assert.IsTrue(result.response.players[0].timecreated >= 1108190390);
            Assert.IsTrue(result.response.players[0].personastateflags >= 0);


            //<response> 
            //    <players> 
            //        <player> 
            //            <steamid>76561197974118008</steamid> 
            //            <communityvisibilitystate>3</communityvisibilitystate> 
            //            <profilestate>1</profilestate> 
            //            <personaname>nosilleg</personaname> 
            //            <lastlogoff>1392847930</lastlogoff> 
            //            <commentpermission>2</commentpermission> 
            //            <profileurl>https://steamcommunity.com/id/nosilleg/</profileurl> 
            //            <avatar>https://media.steampowered.com/steamcommunity/public/images/avatars/85/8522d758971df1d6c4046cca81e8a364315388f2.jpg</avatar> 
            //            <avatarmedium>https://media.steampowered.com/steamcommunity/public/images/avatars/85/8522d758971df1d6c4046cca81e8a364315388f2_medium.jpg</avatarmedium> 
            //            <avatarfull>https://media.steampowered.com/steamcommunity/public/images/avatars/85/8522d758971df1d6c4046cca81e8a364315388f2_full.jpg</avatarfull> 
            //            <personastate>0</personastate> 
            //            <primaryclanid>103582791433470748</primaryclanid> 
            //            <timecreated>1108190390</timecreated> 
            //            <personastateflags>0</personastateflags> 
            //        </player>
            //    </playesr>
            //</response>
        }
    }
}
