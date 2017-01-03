using CodeSampleBackend.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CodeSampleBackend.ComFunc
{
    public class GitHubHelper
    {
        public string GetLatestIssue(string link)
        {
            
            return "getted!";
        }
        public string GetLatestCommit(string link)
        {
            return "getted!";
        }
        public static object GetGitHubJsonObject(string url, string type)
        {
            if (url == null) return null;
            url = url.Replace("github.com", "api.github.com/repos");
            if (url.EndsWith("/"))
            {
                url = $"{url}{type}";
            }
            else
            {
                url = $"{url}/{type}";
            }
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "CodeSampleBackend";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("jamboryao" + ":" + "123Aking"));
            request.Headers.Add("Authorization", $"Basic {encoded}");
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                var content = reader.ReadToEnd();
                stream.Close();
                reader.Close();
                response.Close();
                var jsonObject = JsonConvert.DeserializeObject(content);
                return jsonObject;
            }
            catch(Exception e)
            {
                ErrorLog.WriteError(e.Message, "GetGitHubJsonObject");
                throw;
            }
        }

        public static List<Commit> GetGitHubCommitEntity(object jsonContent)
        { 
            List<Commit> commits = new List<Commit>();
            var items = ((Newtonsoft.Json.Linq.JContainer)jsonContent);
            int i = 0;
            foreach (var item in items)
            {
                Commit commit = new Commit();
                commit.Sha = item.Value<string>("sha");
                commit.URL = item.Value<string>("html_url");
                foreach (var subjson in item)
                {
                    switch (((Newtonsoft.Json.Linq.JProperty)(subjson)).Name)
                    {
                        case "parents":
                            commit.PSha = subjson.FirstOrDefault().First().Value<string>("sha");
                            break;
                        case "commit":
                            commit.Message = subjson.First().Value<string>("message");
                            commit.CreateAt = Convert.ToDateTime(((JValue)(((((subjson).First).First).First).Last).First).Value);
                            break;
                        case "author":
                            commit.Author = ((JValue)((JContainer)(subjson.FirstOrDefault().First())).First).Value.ToString();
                            break;
                        default:
                            break;
                    }
                }
                commits.Add(commit);
                i++;
                if (i > Constants.LatestNumber) break;
            }
            return commits;
        }
    }
}