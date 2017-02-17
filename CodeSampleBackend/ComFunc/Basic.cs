using CodeSampleBackend.DAL;
using CodeSampleBackend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;

namespace CodeSampleBackend.ComFunc
{
    public class Basic
    {
        public static Dictionary<string, object> ToDictionary<T>(T list)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            Type type = typeof(T);
            List<PropertyInfo> propertyList = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).ToList();

            foreach (PropertyInfo propertyInfo in propertyList)
            {
                if (propertyInfo.Name == "id") continue;
                dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(list, null));
            }

            return dictionary;
        }
        public static List<CommitView> ConvertCommitToCommitView(List<Commit> issues, BasicCRUD dal)
        {
            List<CommitView> views = new List<CommitView>();
            foreach (var item in issues)
            {
                CommitView view = new CommitView();
                view.id = item.id;

                view.CreateAt = item.CreateAt;
                view.Author = item.Author;
                view.CodeID = item.CodeID;
                view.GitHubUrl = item.GitHubUrl;
                view.id = item.id;
                view.IsNew = item.IsNew;
                view.Message = item.Message;
                var process = dal.GetEntities<ProcessLog>(c => c.FkId == item.id && c.Type == "commit").OrderByDescending(c => c.LogAT).FirstOrDefault();
                if (process != null)
                {
                    view.Process = dal.GetEntities<Process>(c => c.id == process.ProcessID).First().name;
                }

                view.PSha = item.PSha;
                view.Sha = item.Sha;
                view.Type = item.Author;
                view.URL = item.URL;
                var uts = dal.GetEntities<UTLog>(c => c.FkId == item.id && c.Type == "commit");// context.UTLogs.Where(c => c.FkId == item.id && c.Type == "commit");
                int? utValue = 0;
                foreach (var ut in uts)
                {
                    utValue += ut.UT;
                }
                view.UT = utValue;
                var aliasEntity = dal.GetEntities<CodeOwnership>(c => c.FkId == item.id && c.Type == "commit").OrderByDescending(p => p.LogAt).FirstOrDefault();
                if (aliasEntity != null)
                {
                    view.Alias = aliasEntity.support_alias;
                }
                views.Add(view);
            }

            return views;
        }
        /// <summary>
        /// str format for example test1;test2;
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> stringToList(string str)
        {

            if (str == null) return null;
            return str.Split(';').ToList();

        }

        public static List<IssueView> ConvertIssueToIssueView(List<Issue> issues, BasicCRUD dal)
        {
            List<IssueView> views = new List<IssueView>();
            foreach (var item in issues)
            {
                IssueView view = new IssueView();
                view.id = item.id;
                view.Title = item.Title;
                view.CreateAt = item.CreateAt;
                view.Number = item.Number;
                view.Url = item.Url;
                view.UnicodeId = item.UnicodeId;
                view.Replies = item.Replies;
                view.Author = item.Author;
                view.CodeID = item.CodeID;
                view.Type = item.Type;
                var aliasEntity = dal.GetEntities<CodeOwnership>(c => c.FkId == item.id && c.Type == "issue").OrderByDescending(p => p.LogAt).FirstOrDefault();
                if (aliasEntity != null)
                {
                    view.alias = aliasEntity.support_alias;
                }
                var uts = dal.GetEntities<UTLog>(c => c.FkId == item.id && c.Type == "issue");
                int? utValue = 0;
                foreach (var ut in uts)
                {
                    utValue += ut.UT;
                }
                view.UT = utValue;

                var process = dal.GetEntities<IssueStatusLog>(c => c.IssueID == item.id).OrderByDescending(c => c.LogAt).FirstOrDefault(); // DALProcessLog.GetLatestIssueProcess(item.id);
                if (process != null)
                {
                    view.process = dal.GetEntities<IssueStatu>(c => c.id == process.IssueStatusID).First().name;
                }

                views.Add(view);
            }

            return views;
        }
        public static string GitHttpRequest(string url)
        {
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
                return content;

            }
            catch (Exception e)
            {
                if (e.Message.Contains("Page not found"))
                {
                    ErrorLog.WriteError(e.Message, $"GetGitHubJsonObject error with {url}");
                }
                return "error";
            }
        }
        public static List<T> GetGitHubData<T>(string url, string type) where T : IBody
        {
            url = url.Replace("github.com", "api.github.com/repos");
            url = url.EndsWith("/") ? $"{url}{type}" : $"{url}/{type}";
            List<T> entities = new List<T>();
            string content = Basic.GitHttpRequest(url);
            if (content != "error")
            {
                entities = JsonConvert.DeserializeObject<List<T>>(content);
            }
            return entities;
        }
    }
}