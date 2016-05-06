using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tsa_Model;

namespace tsa_DAL
{
    public class UserDashboardDAL
    {
        public string connectionString = "Data Source=MORYA-PC\\SQLEXPRESS;Initial Catalog=tsa-db;Integrated Security=True;";

        public int AddtoResultDAL(int positivecount, int negativecount, int neutralcount)
        {
            int result_id = -1;

            // add to result table and fetch last entered result id
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Result(positive_score, negative_score, neutral_score) VALUES(" + "@pscore, @nscore, @neuscore)", conn);
                cmd.Parameters.AddWithValue("@pscore", positivecount);
                cmd.Parameters.AddWithValue("@nscore", negativecount);
                cmd.Parameters.AddWithValue("@neuscore", neutralcount);

                int rows = cmd.ExecuteNonQuery();

                if (rows < 0)
                {
                    return -1;
                }

                SqlCommand Selectcmd = new SqlCommand(@"SELECT IDENT_CURRENT('Result')", conn);
                SqlDataReader reader = Selectcmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result_id = Convert.ToInt32(reader[""].ToString());
                        break;
                    }
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                
            }

            return result_id;
        }

        public int SearchOfflineTweets(string searchQuery, List<Tweets> TweetList)
        {
            int result = -1;

            // find search_id then use the search id to find tweets from tweet table
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("select search_id from Search where search_string=" + "\'" + searchQuery + "\'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                int searchid=-1;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        searchid = Convert.ToInt32(reader["search_id"].ToString());
                        break;
                    }
                }
                reader.Close();
                if (searchid != -1)
                {
                    SqlCommand selectcmd = new SqlCommand("select * from Tweets where tweet_search_id=" + searchid, conn);
                    SqlDataReader selectReader = selectcmd.ExecuteReader();
                    if (selectReader.HasRows)
                    {
                        while (selectReader.Read())
                        {
                            Tweets tt = new Tweets();
                            //tt.user_name = selectReader["user_name"].ToString();
                            tt.tweet_text = selectReader["tweet_text"].ToString();
                            //tt.date_time = selectReader["date_time"].ToString();
                            TweetList.Add(tt);
                        }
                    }
                    result = 1;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                result = -1;
            }
            finally
            {
                
            }
            return result;
        }

        public int AddSearchDAL(string query, int result_id)
        {
            int searchid = -1;
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Search(search_string, search_datetime, search_result_id) VALUES(" + "@search_string, @date,@resultid)", conn);
                cmd.Parameters.AddWithValue("@search_string", query);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@resultid", result_id);

                int rows = cmd.ExecuteNonQuery();

                if (rows < 0)
                {
                    return -1;
                }

                SqlCommand Selectcmd = new SqlCommand(@"SELECT IDENT_CURRENT('Search')", conn);
                SqlDataReader reader = Selectcmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        searchid = Convert.ToInt32(reader[""].ToString());
                        break;
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            return searchid;
        }

        public bool AddTweetDAL(Tweets tweet, int search_id)
        {
            bool isAdded = true;

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Tweets(tweet_text, tweet_search_id) VALUES(" + "@text, @search)", conn);
                cmd.Parameters.AddWithValue("@text", tweet.tweet_text);
                cmd.Parameters.AddWithValue("@search", search_id);
                
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isAdded = true;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }


            return isAdded;
        }

        public List<Search> GetSearchResults()
        {
            List<Search> searchList = new List<Search>();

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("select search_string, search_datetime, positive_score, negative_score, neutral_score from Search, Result where search_result_id = result_id;", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //SearchedUser.user_id = Convert.ToInt32(reader["user_id"].ToString());
                        Search sr = new Search();
                        sr.search_string = reader["search_string"].ToString();
                        sr.search_datetime = Convert.ToDateTime(reader["search_datetime"].ToString());
                        sr.positive_score = Convert.ToInt32(reader["positive_score"].ToString());
                        sr.negative_score = Convert.ToInt32(reader["negative_score"].ToString());
                        sr.neutral_score = Convert.ToInt32(reader["neutral_score"].ToString());
                        searchList.Add(sr);
                    }
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

            return searchList;
        }
    }
}
