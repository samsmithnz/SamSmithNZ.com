using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using TweetSharp;

namespace SamSmithNZ2017.Controllers
{
    public class FantasyTweetsController : Controller
    {
        //
        // GET: /FantasyTweets/

        public ActionResult Index()
        {

            //// In v1.1, all API calls require authentication
            //var service = new TwitterService("LKh0dhrFjEC21EE1oFuzQ", "mCEbeLzmBMr41ew9nwOTWH3zJ8GDBY326YnHD8wP0");
            //service.AuthenticateWith("52390333-LHnSY4ovrw5JRIBCEiexfbkuYe54CKNYGjbTXDyJP", "gQ5D5Zie2BAFggjt984eQTjjt9u0ZXk114DZqDjv1A");
            //int page = 1;
            //List<TwitterStatus> tweets = new List<TwitterStatus>();

            //do
            //{
            //    System.Diagnostics.Debug.WriteLine("Requesting tweets. Page" + page);

            //    ListTweetsOnUserTimelineOptions options = new ListTweetsOnUserTimelineOptions();
            //    options.ScreenName = "alyssamae87";
            //    options.IncludeRts = true;
            //    options.Count = 50;

            //    if (page++ > 1)
            //    {
            //        options.MaxId = tweets.Last().Id;
            //    }

            //    List<TwitterStatus> res = (List<TwitterStatus>)service.ListTweetsOnUserTimeline(options);
            //    if (res == null || res.Count == 0)
            //    {
            //        break;
            //    }

            //    System.Diagnostics.Debug.WriteLine("Adding " + res.Count.ToString() + " tweets");

            //    tweets.AddRange(res);

            //} while (tweets.Count <= 50);


            ////IEnumerable<TwitterStatus> tweets = service.ListTweetsOnUserTimeline(options);
            ////IEnumerable<TwitterStatus> tweets = service.ListTweetsOnUserTimeline(0,200);

            //SamSmithNZ2015.Core.FantasyTweet.DataAccess.TweetDataAccess t = new Core.FantasyTweet.DataAccess.TweetDataAccess();

            //foreach (TwitterStatus tweet in tweets)
            //{
            //    if (t.GetItems(tweet.CreatedDate).Count == 0)
            //    {
            //        string[] splitWordList = tweet.Text.Split(' ');
            //        foreach (string splitWord in splitWordList)
            //        {
            //            t.Commit(new SamSmithNZ2015.Core.FantasyTweet.Tweet(tweet.CreatedDate, splitWord));
            //        }
            //    }
            //    //Console.WriteLine("{0} says '{1}'", tweet.User.ScreenName, tweet.Text);
            //}

            //SamSmithNZ2015.Core.FantasyTweet.DataAccess.TweetSummaryDataAccess t2 = new Core.FantasyTweet.DataAccess.TweetSummaryDataAccess();
            //return View(t2.GetItems());
            return View();
        }

    }
}
