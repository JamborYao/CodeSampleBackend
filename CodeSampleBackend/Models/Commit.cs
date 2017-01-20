using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.Models
{
    public class CommitBody
    {
        [JsonProperty(PropertyName = "sha")]
        public string Sha { get; set; }
        [JsonProperty(PropertyName = "html_url")]
        public string Html_Url { get; set; }
        [JsonProperty(PropertyName = "commit")]
        public CommitCommit Commit { get; set; }
    }

    public class CommitAuthor
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "date")]
        public DateTime CommitDate { get; set; }
    }
    public class CommitCommit
    {
        [JsonProperty(PropertyName = "author")]
        public CommitAuthor Author { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}