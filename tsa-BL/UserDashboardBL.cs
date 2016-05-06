using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter.Common;
using LinqToTwitter.Json;
using LinqToTwitter.Security;
using LinqToTwitter.Serialization.Extensions;
using tsa_Model;
using tsa_DAL;

namespace tsa_BL
{
    public class UserDashboardBL
    {
        UserDashboardDAL UserDashboardDAL = new UserDashboardDAL();
        private List<LinqToTwitter.Status> currentTweets;

        public string GetTweets(string query, List<Tweets> TweetList)
        {
            //Tweets[] TweetArray = new Tweets[1000];
            //List<Tweets> TweetList = new List<Tweets>();
            // first authorize
            string Error = "";
            try
            {
                var auth = new LinqToTwitter.SingleUserAuthorizer
                {
                    Credentials = new LinqToTwitter.InMemoryCredentials
                    {
                        ConsumerKey = "p0pz3SD8mkXkEq5n14lxqkIVN",
                        ConsumerSecret = "Orz4fPUrjA9JBRVho07mKp0S7T5IYSO55Sajsp4JH907FcY1VJ",
                        OAuthToken = "66073655-vhoflNSL60jj99naIyEAa0rbnL47KwqHUXSi7y0WS",
                        AccessToken = "WVHdbG9uc419LzlIpXzwjOnAuDCCJRZ4DQ8OFzxfYSGMP"
                    }
                };

                string searchTerm = query;

                var twitterCtx = new LinqToTwitter.TwitterContext(auth);

                var srch =
                Enumerable.SingleOrDefault((from search in
                                        twitterCtx.Search
                                            where search.Type == LinqToTwitter.SearchType.Search &&
                                               search.Query == searchTerm &&
                                               search.Count == 1000
                                            select search));
                if (srch != null && srch.Statuses.Count > 0)
                {
                    var results = srch.Statuses.ToList();
                    //results.ForEach(Tweet => textBoxTweets.AppendText(Tweet.User.Name + ":" + Tweet.Text + "\n"));

                    foreach (LinqToTwitter.Status Tweet in results)
                    {
                        //dataGridViewTweet.Rows.Add(Tweet.User.Name, Tweet.Text);
                        Tweets TweetTemp = new Tweets();
                        TweetTemp.user_name = Tweet.User.Name;
                        TweetTemp.tweet_text = Tweet.Text;
                        TweetTemp.date_time = Tweet.CreatedAt.ToString();
                        TweetList.Add(TweetTemp);

                    }
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message.ToString();  
            }

            return Error;
        }

        public string GetOfflineTweets(string searchQuery, List<Tweets> TweetList)
        {
            string Error = "";
            int result = -1;

            result = UserDashboardDAL.SearchOfflineTweets(searchQuery, TweetList);
            
            if (result == -1)
            {
                Error = "Error while retrieving tweets";
            }
            return Error;
        }

        public int AddToResultBL(int positivecount, int negativecount, int neutralcount)
        {
            int resultId = -1;

            resultId = UserDashboardDAL.AddtoResultDAL(positivecount, negativecount, neutralcount);

            return resultId;
        }

        public int AddSearchBL(string query, int result_id)
        {
            int searchid = -1;

            searchid = UserDashboardDAL.AddSearchDAL(query, result_id);

            return searchid;
        }

        public bool AddTweets(List<Tweets> tweetList, int search_id)
        {
            bool isAdded = true;

            foreach (var item in tweetList)
            {
                isAdded = UserDashboardDAL.AddTweetDAL(item, search_id);
            }

            return isAdded;
        }

        public List<Search> GetSearchBL()
        {
            return UserDashboardDAL.GetSearchResults();
        }
    }
}