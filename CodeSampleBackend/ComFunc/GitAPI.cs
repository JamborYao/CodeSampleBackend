using CodeSampleBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.ComFunc
{
    public class GitAPI
    {
        public GitAPI GetGitHubCommitObject(string url)
        {
            this.CommitBody = Basic.GetGitHubData<CommitBody>(url, "commits");
            return this;
        }
        public GitAPI GetGitHubIssueObject(string url)
        {
            this.issueBody = Basic.GetGitHubData<IssueBody>(url, "issues");
            return this;
        }
        public GitAPI ConvertToCommit(List<CommitBody> commitBody, int id)
        {
            List<Commit> commits = new List<Commit>();
            foreach (var item in commitBody)
            {
                Commit commit = new Commit();
                commit.Author = item.Commit.Author.Name;
                commit.CreateAt = item.Commit.Author.CommitDate;
                commit.Message = item.Commit.Message;
                commit.URL = item.Html_Url;
                commit.CodeID = id;
                commit.SyncDate = DateTime.UtcNow;
                commits.Add(commit);
            }
            this.Commits = commits;
            return this;
        }
        public GitAPI ConvertToIssue(List<IssueBody> issueBody, int id)
        {
            List<Issue> issues = new List<Issue>();
            foreach (var item in issueBody)
            {
                Issue issue = new Issue();
                issue.CreateAt = item.CreateAT;
                issue.Author = item.User.Name;
                issue.Title = item.Title;
                issue.Number = Convert.ToInt32(item.Number);
                issue.Url = item.Html_Url;
                issue.UnicodeId = Convert.ToInt32(item.Id);
                issue.Replies = Convert.ToInt32(item.CommentsNumber);
                issue.CodeID = id;
                issue.SyncDate = DateTime.UtcNow;
                issue.Body = item.Body;
                if (item.PullRequest == null)
                {
                    issue.Type = "Issue";
                }
                else
                {
                    issue.Type = "PullRequest";
                }
                issues.Add(issue);

            }
            this.issues = issues;
            return this;
        }
        public GitAPI InsertToDatabase(List<Commit> commits, BasicCRUD dal, string githubUrl)
        {
            foreach (var commit in commits)
            {
               
                commit.GitHubUrl = githubUrl;
                dal.AddOrUpdate<Commit>(commit, c => c.CreateAt == commit.CreateAt, Basic.ToDictionary<Commit>(commit));
            }
            return this;
        }
        public GitAPI InsertToDatabase(List<Issue> issues, BasicCRUD dal)
        {
            foreach (var issue in issues)
            {
                dal.AddOrUpdate<Issue>(issue, c => c.CreateAt == issue.CreateAt, Basic.ToDictionary<Issue>(issue));
            }
            return this;
        }
        public List<CommitBody> CommitBody { get; set; }
        public List<IssueBody> issueBody { get; set; }
        public List<Commit> Commits { get; set; }
        public List<Issue> issues { get; set; }
    }
}