using CodeSampleBackend.ComFunc;
using CodeSampleBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
    public class DALCommit
    {
        public static bool AddCommitsIfNotExistedElseUpdate(List<Commit> commits)
        {
            try
            {
                context = new MoonCakeCodeSampleEntities();
                foreach (var item in commits)
                {
                    var commit = GetCommitByCreateAt(item.CreateAt);
                    if (commit != null)
                    {
                        commit.Author = item.Author;
                        commit.CreateAt = item.CreateAt;
                        commit.GitHubUrl = item.GitHubUrl;
                        commit.IsNew = item.IsNew;
                        commit.Message = item.Message;
                        commit.PSha = item.PSha;
                        commit.Sha = item.Sha;
                        commit.Type = item.Type;
                        commit.URL = item.URL;
                        commit.id = item.id;

                    }
                    else
                    {
                        context.Commits.Add(item);
                    }
                    context.SaveChanges();


                }
                return true;
            }
            catch (Exception e)
            {
                ErrorLog.WriteError(e.Message, "AddCommitsIfNotExistedElseUpdate");
                return false; ;
            }

        }
        public static MoonCakeCodeSampleEntities context;
        public static List<Commit> GetAllCommits()
        {
            context = new MoonCakeCodeSampleEntities();
            return context.Commits.ToList<Commit>();
        }
        public static List<Commit> GetNewCommits(string githubUrl, DateTime? takeTime)
        {
            context = new MoonCakeCodeSampleEntities();
            return context.Commits.Where(c => c.GitHubUrl == githubUrl && c.CreateAt >= takeTime).ToList<Commit>();
        }
        public static Commit GetCommitByCreateAt(DateTime? createAt)
        {
            context = new MoonCakeCodeSampleEntities();
            var commit = context.Commits.Where(c => c.CreateAt == createAt).FirstOrDefault();
            return commit;

        }
        public static List<CommitView> GetNewCommitsView(string githubUrl, DateTime? takeTime)
        {
            context = new MoonCakeCodeSampleEntities();
            var commits = context.Commits.Where(c => c.GitHubUrl == githubUrl && c.CreateAt >= takeTime).ToList<Commit>();
            return ConvertCommitToCommitView(commits, context);
        }
        public static List<CommitView> GetAllCommitsView()
        {
            context = new MoonCakeCodeSampleEntities();
            var commits = context.Commits.ToList<Commit>();
            return ConvertCommitToCommitView(commits, context);
        }
        public static List<CommitView> ConvertCommitToCommitView(List<Commit> issues, MoonCakeCodeSampleEntities context)
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
                var process = DALProcessLog.GetLatestProcess(item.id, "commit");
                view.Process = process;
                view.PSha = item.PSha;
                view.Sha = item.Sha;
                view.Type = item.Author;
                view.URL = item.URL;
                var uts = context.UTLogs.Where(c => c.FkId == item.id && c.Type == "commit");
                int? utValue = 0;
                foreach (var ut in uts)
                {
                    utValue += ut.UT;
                }
                view.UT = utValue;
                var aliasEntity = context.CodeOwnerships.Where(c => c.FkId == item.id && c.Type == "commit").OrderByDescending(p => p.LogAt).FirstOrDefault();
                if (aliasEntity != null)
                {
                    view.Alias = aliasEntity.support_alias;
                }
                views.Add(view);
            }

            return views;
        }
    }
}