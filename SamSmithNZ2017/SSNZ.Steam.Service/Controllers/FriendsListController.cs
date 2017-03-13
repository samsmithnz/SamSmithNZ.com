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
    public class FriendsListController : ApiController
    {
        public List<Friend> GetFriendList(string steamId)
        {
            FriendsListDA da = new FriendsListDA();
            return da.GetFriendsList(steamId);
        }
    }
}
