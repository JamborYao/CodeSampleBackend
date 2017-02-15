using CodeSampleBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
    public class DALIssueView
    {
        public static MoonCakeCodeSampleEntities context;
        public static List<IssueView> GetNewIssueView(int codeID, DateTime? takeTime)
        {
            context = new MoonCakeCodeSampleEntities();
            var issues = context.Issues.Where(c => c.CodeID == codeID && c.CreateAt >= takeTime).ToList<Issue>();
            return ConvertIssueToIssueView(issues,context);
        }

        public static List<IssueView> ConvertIssueToIssueView(List<Issue> issues, MoonCakeCodeSampleEntities context)
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
                var aliasEntity = context.CodeOwnerships.Where(c => c.FkId == item.id && c.Type == "issue").OrderByDescending(p => p.LogAt).FirstOrDefault();
                if (aliasEntity != null)
                {
                    view.alias = aliasEntity.support_alias;
                }
                var uts = context.UTLogs.Where(c => c.FkId == item.id && c.Type == "issue");
                int? utValue = 0;
                foreach (var ut in uts)
                {
                    utValue += ut.UT;
                }
                view.UT = utValue;
                var process = DALProcessLog.GetLatestIssueProcess(item.id);
                view.process = process;
                views.Add(view);
            }

            return views;
        }

        public static List<IssueView> GetAllIssueView()
        {
            context = new MoonCakeCodeSampleEntities();
            var issues = context.Issues.ToList<Issue>();
            return ConvertIssueToIssueView(issues, context);
        }

        public static IssueView GetIssueViewByID(int id)
        {
            context = new MoonCakeCodeSampleEntities();
            var issues = context.Issues.Where(c=>c.id==id).ToList<Issue>();
            return ConvertIssueToIssueView(issues, context).FirstOrDefault();
        }
    }
}