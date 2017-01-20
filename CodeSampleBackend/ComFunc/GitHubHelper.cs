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
        public static List<CommitBody> GetGitHubCommitObject(string url )
        {
            string type = "commits";
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
            List<CommitBody> jsonObject = new List<CommitBody>();
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "CodeSampleBackend";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Constants.GitHubAccount + ":" + Constants.GitHubKey));
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
                jsonObject = JsonConvert.DeserializeObject<List<CommitBody>>(content);
               
            }
            catch(Exception e)
            {
                if (e.Message.Contains("Page not found"))
                {
                    ErrorLog.WriteError(e.Message, "GetGitHubJsonObject");
                }
            }
            return jsonObject;
        }

        public static List<Commit> GetGitHubCommitEntity(List<CommitBody> jsonContent,int id)
        {

            List<Commit> commits = new List<Commit>();
            foreach (var item in jsonContent)
            {
                Commit commit = new Commit();
                commit.Author = item.Commit.Author.Name;
                commit.CreateAt = item.Commit.Author.CommitDate;
                commit.Message = item.Commit.Message;
                commit.URL = item.Html_Url;
                commit.id = id;
                commits.Add(commit);
            }
            return commits;
        }

        public static List<IssueBody> GetGitHubIssueObject(string url)
        {
            // if (url == null) return null;
            string type = "issues";
            url = url.Replace("github.com", "api.github.com/repos");
            if (url.EndsWith("/"))
            {
                url = $"{url}{type}";
            }
            else
            {
                url = $"{url}/{type}";
            }
            List<IssueBody> jsonObject = new List<IssueBody>();
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "CodeSampleBackend";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Constants.GitHubAccount + ":" + Constants.GitHubKey));
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
                jsonObject = JsonConvert.DeserializeObject<List<IssueBody>>(content);

            }
            catch (Exception e)
            {
                if (e.Message.Contains("Page not found"))
                {
                    ErrorLog.WriteError(e.Message, "GetGitHubJsonObject");
                }
            }
            return jsonObject;
        }
    }
}