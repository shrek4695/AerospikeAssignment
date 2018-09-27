using Aerospike.Client;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataDumperTool
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            string csvFile = @"C:\Users\skapoor\Desktop\2018-08-charlottesville-twitter-trolls\data\tweets1.csv";

            var client = new AerospikeClient("18.235.70.103", 3000);
            string nameSpace = "AirEngine";
            string setName = "Shreea";


            List<string[]> results = new List<string[]>();
            var reader = new StreamReader(File.OpenRead(csvFile));


            using (TextFieldParser csvParser = new TextFieldParser(csvFile))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvParser.ReadLine();
                while (count<=2000)
                {
                    string[] fields = csvParser.ReadFields();
                    results.Add(fields);
                    count++;
                }
            }
            for (int i = 0; i <=2000; i++)
            {
                var key = new Key(nameSpace, setName, results[i][15]);
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("author",results[i][0]), new Bin("Content", results[i][1]), new Bin("Region", results[i][2]), new Bin("Language", results[i][3]), new Bin("tweetDate", results[i][4]), new Bin("Tweet_time", results[i][5]), new Bin("Year", results[i][6]), new Bin("Month", results[i][7]), new Bin("Hour", results[i][8]), new Bin("Minute", results[i][9]), new Bin("Following", results[i][10]), new Bin("Followers", results[i][11]), new Bin("Post_url", results[i][12]), new Bin("Post_Type", results[i][13]), new Bin("Retweet", results[i][14]), new Bin("tweet_Id", results[i][15]), new Bin("Author_id", results[i][16]), new Bin("accCategory", results[i][17]), new Bin("newjune2018", results[i][18]) });
            }
            Console.ReadKey();
        }
    }
}
