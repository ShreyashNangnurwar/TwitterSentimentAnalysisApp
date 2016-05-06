using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tsa_BL
{
    public class NaiveBayesClassifier
    {
        public Dictionary<string, int> positiveWordDictionary = new Dictionary<string, int>();
        public Dictionary<string, int> negativeWordDictionary = new Dictionary<string, int>();
        public Dictionary<string, int> neutralWordDictionary = new Dictionary<string, int>();

        double positiveProbability;
        double NegativeProbability;
        double NeutralProbability;
        int positiveTweetCount = 0;
        int negativeTweetCount = 0;
        int neutralTweetCount = 0;

        public NaiveBayesClassifier()
        {

        }

        public void TrainClassifier()
        {
            PreprocessingBL pr = new PreprocessingBL();
            var reader = new StreamReader(File.OpenRead(@"C:\Users\SHREYA$H\Documents\Visual Studio 2015\Projects\tsa-webapp\tsa-BL\full_training_dataset.csv"));
            List<string> listcategory = new List<string>();
            List<string> listTweet = new List<string>();
            string positiveTweet = "";
            string negativeTweet = "";
            string neutralTweet = "";
            string cleanedTweet = "";
            int positiveTweetCount = 0;
            int negativeTweetCount = 0;
            int neutralTweetCount = 0;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var temp = line.Replace("\"", "");
                line = temp;
                var values = line.Split(',');

                listcategory.Add(values[0]);
                listTweet.Add(values[1]);

                List<string> stopwords = pr.GetStopWords();

                switch (values[0])
                {
                    case "positive":
                        positiveTweetCount++;
                        cleanedTweet = pr.cleanTweet(values[1], stopwords);
                        string[] tokens = pr.TokenizeTweet(cleanedTweet);
                        //string[] temptokens = string.Join(" ", tokens, 1, tokens.Length - 2).Split(' ');
                        foreach (var item in tokens)
                        {
                            if (item != "")
                            {
                                if (positiveWordDictionary.ContainsKey(item))
                                {
                                    positiveWordDictionary[item]++;
                                }
                                else
                                {
                                    positiveWordDictionary.Add(item, 1);
                                }
                            }
                        }
                        break;

                    case "negative":
                        negativeTweetCount++;
                        cleanedTweet = pr.cleanTweet(values[1], stopwords);
                        string[] tokensNeg = pr.TokenizeTweet(cleanedTweet);
                        //string[] temptokensNeg = string.Join(" ", tokensNeg, 1, tokensNeg.Length - 2).Split(' ');
                        foreach (var item in tokensNeg)
                        {
                            if (item != "")
                            {
                                if (negativeWordDictionary.ContainsKey(item))
                                {
                                    negativeWordDictionary[item]++;
                                }
                                else
                                {
                                    negativeWordDictionary.Add(item, 1);
                                }
                            }
                        }
                        break;

                    case "neutral":
                        neutralTweetCount++;
                        cleanedTweet = pr.cleanTweet(values[1], stopwords);
                        string[] tokensNeu = pr.TokenizeTweet(cleanedTweet);
                        //string[] temptokensNeu = string.Join(" ", tokensNeu, 1, tokensNeu.Length - 2).Split(' ');
                        foreach (var item in tokensNeu)
                        {
                            if (item != "")
                            {
                                if (neutralWordDictionary.ContainsKey(item))
                                {
                                    neutralWordDictionary[item]++;
                                }
                                else
                                {
                                    neutralWordDictionary.Add(item, 1);
                                }
                            }
                        }
                        break;
                }
            }
        }

        public int GetPositiveWordCount(string word)
        {
            int count = 1;

            if (positiveWordDictionary.ContainsKey(word))
            {
                count = positiveWordDictionary[word];
            }

            return count;
        }

        public int GetNegativeWordCount(string word)
        {
            int count = 1;
            if (negativeWordDictionary.ContainsKey(word))
            {
                count = negativeWordDictionary[word];
            }

            return count;
        }

        public int GetNeutralWordCount(string word)
        {
            int count = 1;

            if (neutralWordDictionary.ContainsKey(word))
            {
                count = neutralWordDictionary[word];
            }
            return count;
        }

        public int GetTotalNumberOfWords(Dictionary<string, int> dict)
        {
            int count = 1;

            foreach (KeyValuePair<string, int> record in dict)
            {
                count += record.Value;
            }

            return count;
        }

        public string NaiveBayesClassifierAlgorithm(string[] wordsList)
        {
            positiveProbability = 1.0;
            NegativeProbability = 1.0;
            NeutralProbability = 1.0;

            int length = wordsList.Length;
            string category;

            // calculate all dictionary word count
            int allDictionaryWordCount = positiveWordDictionary.Count + negativeWordDictionary.Count + neutralWordDictionary.Count;

            for (int i = 0; i < length; i++)
            {
                if (wordsList[i] != "")
                {
                    // calculate positive word prabability
                    positiveProbability *= Convert.ToDouble(GetPositiveWordCount(wordsList[i]) + 1) / (Convert.ToDouble(GetTotalNumberOfWords(positiveWordDictionary) + allDictionaryWordCount));

                    NegativeProbability *= Convert.ToDouble(GetNegativeWordCount(wordsList[i]) + 1) / (Convert.ToDouble(GetTotalNumberOfWords(negativeWordDictionary) + allDictionaryWordCount));

                    NeutralProbability *= Convert.ToDouble(GetNeutralWordCount(wordsList[i]) + 1) / (Convert.ToDouble(GetTotalNumberOfWords(neutralWordDictionary) + allDictionaryWordCount));
                }
            }

            if ((positiveProbability >= NegativeProbability) && (positiveProbability >= NeutralProbability))
            {
                category = "Positive";
            }
            else if ((NegativeProbability > positiveProbability) && (NegativeProbability > NeutralProbability))
            {
                category = "Negative";
            }
            else
            {
                category = "Neutral";
            }

            return category;
        }
    }

}
