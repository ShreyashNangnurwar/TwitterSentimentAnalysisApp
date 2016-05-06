using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace tsa_BL
{
    public class PreprocessingBL
    {
        public List<string> GetStopWords()
        {
            // remove stopwords 1. read stop words file 2. get that into list 3. remove stopwords from cleaned tweet
            string fileLocation = @"C:\Users\SHREYA$H\Documents\Visual Studio 2015\Projects\tsa-webapp\tsa-BL\stopwords_en.txt";
            List<string> stopwords = new List<string>();
            using (StreamReader str = new StreamReader(fileLocation))
            {
                string line;
                while ((line = str.ReadLine()) != null)
                {
                    stopwords.Add(line.Split('\n')[0]);
                }
            }
            return stopwords;
        }

        public string cleanTweet(string rawTweet, List<string> stopwords)
        {
            string cleanedTweet = "";

            // chenge to lower case
            cleanedTweet = rawTweet.ToLower();

            // remove urls
            string urlFreeTweet = Regex.Replace(cleanedTweet, @"http[^\s]+", "");

            // remove punctuations
            var sb = new StringBuilder();
            foreach (char c in urlFreeTweet)
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }
            string punctuationFreeTweet = sb.ToString();

            string[] tokens = System.Text.RegularExpressions.Regex.Split(punctuationFreeTweet, @"\W+");
            for (int i = 0; i < tokens.Length; i++)
            {
                if (stopwords.Contains(tokens[i]))
                {
                    tokens[i] = "";
                }
            }

            string temp = string.Join(" ", tokens);
            punctuationFreeTweet = temp;
            cleanedTweet = punctuationFreeTweet;
            return cleanedTweet;
        }

        public string[] TokenizeTweet(string cleanedTweet)
        {
            string[] tokens = new string[100];

            tokens = System.Text.RegularExpressions.Regex.Split(cleanedTweet, @"\W+");

            return tokens;
        }
    }
}
