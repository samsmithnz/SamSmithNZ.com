using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Service.Controllers
{
    public class FriendsController : ApiController
    {
        public List<Friend> GetFriends(string steamId)
        {
            FriendsDA da = new FriendsDA();
            return da.GetFriends(steamId);
        }

        public List<Friend> GetFriendsWithSameGame(string steamId, string appId)
        {
            FriendsDA da = new FriendsDA();
            return da.GetFriendsWithSameGame(steamId, appId);
        }
    }
}
