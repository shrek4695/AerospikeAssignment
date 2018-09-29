using Aerospike.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TweetsAPI.Models;

namespace TweetsAPI.Controllers
{
    public class TweetController : ApiController
    {
        [HttpGet]
        //[Route("api/Tweet/GetTweetsbyId/{id}")]
        public List<Record> GetTweetsbyId([FromUri]String[] ids)
        {
            Record record;
            var client = new AerospikeClient("18.235.70.103", 3000);
            string nameSpace = "AirEngine";
            string setName = "Shreea";
            List<Record> records = new List<Record>();
            int counter = 0;
            while (counter<ids.Length)
            {
                var key = new Key(nameSpace, setName, ids[counter]);
                record = client.Get(new WritePolicy(), key);
                records.Add(record);
                counter++;
            }
            return records;
        }
        [HttpPut]
        [Route("api/Tweet/Update/")]
        public void EditTweets([FromBody] TweetsData updatedetails)
        {
            var client = new AerospikeClient("18.235.70.103", 3000);
            string nameSpace = "AirEngine";
            string setName = "Shreea";
            var key = new Key(nameSpace, setName, updatedetails.tweet_id);
            client.Put(new WritePolicy(), key, new Bin(updatedetails.bin_name,updatedetails.update_value));
        }
        [HttpDelete]
        [Route("api/Tweet/Delete/")]
        public void DeleteTweets([FromUri]String[] ids)
        {
            var client = new AerospikeClient("18.235.70.103", 3000);
            string nameSpace = "AirEngine";
            string setName = "Shreea";
            int counter=0;
            while(counter<ids.Length)
            {
            var key = new Key(nameSpace, setName, ids[counter]);
            client.Delete(new WritePolicy(), key);
            counter++;
            }
        }
    }
}
