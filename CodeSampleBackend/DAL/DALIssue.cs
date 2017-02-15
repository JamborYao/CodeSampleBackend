using CodeSampleBackend.ComFunc;
using CodeSampleBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
    public class DALIssue
    {
        //public static bool AddCommitsIfNotExistedElseUpdate(List<Issue> issues)
        //{
        //    try
        //    {
        //        context = new MoonCakeCodeSampleEntities();
        //        foreach (var item in issues)
        //        {
        //            var issue = GetCommitByCreateAt(item.CreateAt);
        //            if (issue != null)
        //            {
        //                issue.CreateAt = item.CreateAt;
        //                issue.Author = item.Author;
        //                issue.Title = item.Title;
        //                issue.Number = item.Number;
        //                issue.Url = item.Url;
        //                issue.UnicodeId = item.UnicodeId;
        //                issue.Replies =item.Replies;
        //                issue.CodeID =item.CodeID ;
        //                issue.Body = item.Body;
        //                issue.Type = item.Type;
        //            }
        //            else
        //            {
        //                context.Issues.Add(item);
        //            }
        //            context.SaveChanges();


        //        }
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorLog.WriteError(e.Message, "AddCommitsIfNotExistedElseUpdate");
        //        return false; ;
        //    }

        //}
        public static MoonCakeCodeSampleEntities context;
        public static List<Issue> GetAllIssues()
        {
            context = new MoonCakeCodeSampleEntities();
            return context.Issues.ToList<Issue>();
        }
        public static List<Issue> GetNewIssue(int codeID,DateTime? takeTime)
        {
            context = new MoonCakeCodeSampleEntities();
            return context.Issues.Where(c => c.CodeID == codeID&&c.CreateAt>=takeTime).ToList<Issue>();
        }
        public static Issue GetCommitByCreateAt(DateTime? createAt)
        {
            context = new MoonCakeCodeSampleEntities();
            var commit = context.Issues.Where(c => c.CreateAt == createAt).FirstOrDefault();
            return commit;

        }

        public static List<Issue> GetGitHubIssueEntity(List<IssueBody> jsonContent, int id)
        {

            List<Issue> issues = new List<Issue>();
            foreach (var item in jsonContent)
            {
                Issue issue = new Issue();
                issue.CreateAt = item.CreateAT;
                issue.Author = item.User.Name;
                issue.Title = item.Title;
                issue.Number =Convert.ToInt32( item.Number);
                issue.Url = item.Html_Url;
                issue.UnicodeId = Convert.ToInt32(item.Id);
                issue.Replies = Convert.ToInt32(item.CommentsNumber);
                issue.CodeID = id;
                issue.Body = item.Body;
                if (item.PullRequest == null)
                {
                    issue.Type = "Issue";
                }
                else {
                    issue.Type = "PullRequest";
                }
                issues.Add(issue);

                //issue.CreateAt = item.issue.Author.CommitDate;
                //issue.Message = item.issue.Message;
                //issue.URL = item.Html_Url;
                //issue.id = id;
                //issue.Add(commit);
            }
            return issues;
        }
    }
}