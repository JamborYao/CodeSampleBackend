using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.Models
{
    public class IssueBody
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "html_url")]
        public string Html_Url { get; set; }

        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "user")]
        public IssueUser User { get; set; }
        [JsonProperty(PropertyName = "comments")]
        public string CommentsNumber { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreateAT { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
        [JsonProperty(PropertyName = "pull_request")]
        public Pull PullRequest { get; set; }

       
    }

    public class IssueUser
    {
        [JsonProperty(PropertyName = "login")]
        public string  Name { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }
    }
    public class Pull
    {
        [JsonProperty(PropertyName = "html_url")]
        public string Html_Url { get; set; }
    }
}