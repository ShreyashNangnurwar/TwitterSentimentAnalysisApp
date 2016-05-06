using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.HtmlControls;
using tsa_Model;
using tsa_BL;
using System.Web.UI.DataVisualization.Charting;

namespace tsa_webapp.User
{
    public partial class UserDashboard : System.Web.UI.Page
    {
        //Tweets[] TweetsArray = new Tweets[1000];
        static List<Tweets> TweetList = new List<Tweets>();
        UserDashboardBL UserDashboardBL = new UserDashboardBL();
        PreprocessingBL Preprocessor = new PreprocessingBL();
        NaiveBayesClassifier Classifier = new NaiveBayesClassifier();
        static int isTrained = 0;
        static string searchQuery;

        protected void Page_Load(object sender, EventArgs e)
        {
            //DropDownListGraphType.DataSource = Enum.GetNames(typeof(SeriesChartType));
            //DropDownListGraphType.DataBind();
        }

        protected void ButtonSearchQuery_Click(object sender, EventArgs e)
        {
            searchQuery = TextBoxSearchQuery.Text;
            TweetList.Clear();
            if (searchQuery == "")
            {
                LabelSearchTweetsError.Text = "Query Empty.";
                SearchTweetsErrorDiv.Visible = true;
                SearchTweetsSuccessDiv.Visible = false;
            }
            else
            {
                string Error = UserDashboardBL.GetTweets(searchQuery, TweetList);

                if (Error != "")
                {
                    LabelSearchTweetsError.Text = Error;
                    SearchTweetsErrorDiv.Visible = true;
                    SearchTweetsSuccessDiv.Visible = false;
                }
                else
                {
                    LabelSearchSuccess.Text = "Fetched " + TweetList.Count + " tweets related to " + searchQuery;
                    SearchTweetsSuccessDiv.Visible = true;
                    SearchTweetsErrorDiv.Visible = false;

                    foreach (var tweet in TweetList)
                    {
                        HtmlTableRow row = new HtmlTableRow();

                        HtmlTableCell user_name = new HtmlTableCell();
                        user_name.Controls.Add(new LiteralControl(tweet.user_name));
                        row.Cells.Add(user_name);

                        HtmlTableCell tweet_text = new HtmlTableCell();
                        tweet_text.Controls.Add(new LiteralControl(tweet.tweet_text));
                        row.Cells.Add(tweet_text);

                        HtmlTableCell date_time = new HtmlTableCell();
                        date_time.Controls.Add(new LiteralControl(tweet.date_time));
                        row.Cells.Add(date_time);

                        TweetsTable.Rows.Add(row);
                    }
                }
            }
        }

        protected void ButtonShowPreprocessedTweets_Click(object sender, EventArgs e)
        {
            string[] cleanedtweetarray = new string[200];

            int i = 0;
            List<string> stopwords = Preprocessor.GetStopWords();
            foreach (var item in TweetList)
            {
                cleanedtweetarray[i] = Preprocessor.cleanTweet(item.tweet_text, stopwords);
                i++;
            }

            foreach (var str in cleanedtweetarray)
            {
                HtmlTableRow row = new HtmlTableRow();

                HtmlTableCell tweet_text = new HtmlTableCell();
                tweet_text.Controls.Add(new LiteralControl(str));
                row.Cells.Add(tweet_text);

                processedTable.Rows.Add(row);
            }
        }

        protected void ButtonShowGraph_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> GraphData = new Dictionary<string, int>();
            
            if (isTrained == 0)
            {
                Classifier.TrainClassifier();
                //isTrained = 1;
            }

            int positiveTweetCount = 0, NegativeTweetCount = 0, NeutralTweetCount=0;
            List<string> stopwords = Preprocessor.GetStopWords();
            foreach (var item in TweetList)
            {
                string tweet = item.tweet_text;
                string cleanedTweet = Preprocessor.cleanTweet(tweet,stopwords);
                string[] tokens = Preprocessor.TokenizeTweet(cleanedTweet);
                string result = Classifier.NaiveBayesClassifierAlgorithm(tokens);
                item.category = result;
                if (result == "Positive")
                {
                    positiveTweetCount++;   
                }
                else if (result == "Negative")
                {
                    NegativeTweetCount++;
                }
                else
                {
                    NeutralTweetCount++;
                }
            }
            GraphData.Clear();
            // show graph
            GraphData.Add("positive", positiveTweetCount);
            GraphData.Add("negative", NegativeTweetCount);
            GraphData.Add("neutral", NeutralTweetCount);

            tsaChart.Series["tsaSeries"].Points.DataBind(GraphData, "Key", "Value", string.Empty);
            tsaChart.Series["tsaSeries"].ChartTypeName = SeriesChartType.Pie.ToString();

            // show result table
            HtmlTableRow row = new HtmlTableRow();

            HtmlTableCell positivesCat = new HtmlTableCell();
            positivesCat.Controls.Add(new LiteralControl("Positive"));
            row.Cells.Add(positivesCat);

            HtmlTableCell positives = new HtmlTableCell();
            positives.Controls.Add(new LiteralControl(positiveTweetCount.ToString()));
            row.Cells.Add(positives);

            TableResults.Rows.Add(row);

            HtmlTableRow row1 = new HtmlTableRow();
            HtmlTableCell NegativeCat = new HtmlTableCell();
            NegativeCat.Controls.Add(new LiteralControl("Negative"));
            row1.Cells.Add(NegativeCat);

            HtmlTableCell negatives = new HtmlTableCell();
            negatives.Controls.Add(new LiteralControl(NegativeTweetCount.ToString()));
            row1.Cells.Add(negatives);
            TableResults.Rows.Add(row1);

            HtmlTableRow row2 = new HtmlTableRow();
            HtmlTableCell NeutralCat = new HtmlTableCell();
            NeutralCat.Controls.Add(new LiteralControl("Neutral"));
            row2.Cells.Add(NeutralCat);

            HtmlTableCell neutral = new HtmlTableCell();
            neutral.Controls.Add(new LiteralControl(NeutralTweetCount.ToString()));
            row2.Cells.Add(neutral);
            TableResults.Rows.Add(row2);

            // show table with result
            int i = 1;
            foreach (var item in TweetList)
            {
                HtmlTableRow row3 = new HtmlTableRow();

                HtmlTableCell SerialNumber = new HtmlTableCell();
                SerialNumber.Controls.Add(new LiteralControl(i.ToString()));
                row3.Cells.Add(SerialNumber);

                HtmlTableCell text = new HtmlTableCell();
                text.Controls.Add(new LiteralControl(item.tweet_text));
                row3.Cells.Add(text);

                HtmlTableCell cat = new HtmlTableCell();
                cat.Controls.Add(new LiteralControl(item.category));
                row3.Cells.Add(cat);

                TableFinalResult.Rows.Add(row3);
                i++;
            }

            // add result id to database
            int result_id = UserDashboardBL.AddToResultBL(positiveTweetCount, NegativeTweetCount, NeutralTweetCount);

            // add search id to database
            int search_id = UserDashboardBL.AddSearchBL(searchQuery, result_id);

            // add tweets to database
            bool isAdded = UserDashboardBL.AddTweets(TweetList, search_id);
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            List<Search> history = new List<Search>();
            history = UserDashboardBL.GetSearchBL();

            foreach (var hist in history)
            {
                HtmlTableRow row = new HtmlTableRow();

                HtmlTableCell search_string = new HtmlTableCell();
                search_string.Controls.Add(new LiteralControl(hist.search_string));
                row.Cells.Add(search_string);

                HtmlTableCell datetime = new HtmlTableCell();
                datetime.Controls.Add(new LiteralControl(hist.search_datetime.ToString()));
                row.Cells.Add(datetime);

                HtmlTableCell positives = new HtmlTableCell();
                positives.Controls.Add(new LiteralControl(hist.positive_score.ToString()));
                row.Cells.Add(positives);

                HtmlTableCell negatives = new HtmlTableCell();
                negatives.Controls.Add(new LiteralControl(hist.negative_score.ToString()));
                row.Cells.Add(negatives);

                HtmlTableCell neutral = new HtmlTableCell();
                neutral.Controls.Add(new LiteralControl(hist.neutral_score.ToString()));
                row.Cells.Add(neutral);

                searchTable.Rows.Add(row);
            }
        }

        protected void ButtonOfflineSearchQuery_Click(object sender, EventArgs e)
        {
            searchQuery = TextBoxSearchQuery.Text;
            TweetList.Clear();
            if (searchQuery == "")
            {
                LabelSearchTweetsError.Text = "Query Empty.";
                SearchTweetsErrorDiv.Visible = true;
                SearchTweetsSuccessDiv.Visible = false;
            }
            else
            {
                string Error = UserDashboardBL.GetOfflineTweets(searchQuery, TweetList);

                if (Error != "")
                {
                    LabelSearchTweetsError.Text = Error;
                    SearchTweetsErrorDiv.Visible = true;
                    SearchTweetsSuccessDiv.Visible = false;
                }
                else
                {
                    LabelSearchSuccess.Text = "Fetched " + TweetList.Count + " tweets related to " + searchQuery;
                    SearchTweetsSuccessDiv.Visible = true;
                    SearchTweetsErrorDiv.Visible = false;

                    foreach (var tweet in TweetList)
                    {
                        HtmlTableRow row = new HtmlTableRow();

                        HtmlTableCell user_name = new HtmlTableCell();
                        user_name.Controls.Add(new LiteralControl("offline-db"));
                        row.Cells.Add(user_name);

                        HtmlTableCell tweet_text = new HtmlTableCell();
                        tweet_text.Controls.Add(new LiteralControl(tweet.tweet_text));
                        row.Cells.Add(tweet_text);

                        HtmlTableCell date_time = new HtmlTableCell();
                        date_time.Controls.Add(new LiteralControl(DateTime.Now.ToString()));
                        row.Cells.Add(date_time);

                        TweetsTable.Rows.Add(row);
                        
                    }
                }
            }
        }
    }
}