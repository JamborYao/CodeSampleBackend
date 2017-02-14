using CodeSampleBackend.ComFunc;
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
        public static List<Commit> GetNewCommits(string githubUrl,DateTime? takeTime)
        {
            context = new MoonCakeCodeSampleEntities();
            return context.Commits.Where(c=>c.GitHubUrl==githubUrl&&c.CreateAt>= takeTime).ToList<Commit>();
        }
        public static Commit GetCommitByCreateAt(DateTime? createAt)
        {
            context = new MoonCakeCodeSampleEntities();
            var commit = context.Commits.Where(c => c.CreateAt == createAt).FirstOrDefault();
            return commit;
          
        }
       
    }
}