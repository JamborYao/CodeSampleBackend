using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.Models
{
    public class Code
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "link")]
        public string  Link { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "last_update")]
        public DateTime LastUpateDate { get; set; }

        [JsonProperty(PropertyName = "products")]
        public List<string> Products { get; set; }
        [JsonProperty(PropertyName = "platform")]
        public List<string> Platform { get; set; }

        [JsonProperty(PropertyName = "sync_date")]
        public DateTime SyncDate { get; set; }

        [JsonProperty(PropertyName = "process")]
        public string Process { get; set; }

        [JsonProperty(PropertyName = "girhub_url")]
        public string GitHubUrl { get; set; }
    }
}