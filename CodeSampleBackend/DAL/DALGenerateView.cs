using CodeSampleBackend.ComFunc;
using CodeSampleBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleBackend.DAL
{
    public class DALGenerateView
    {
        public static PageCodeView GetCodeView(List<Code> codes,string pageStr,string limitStr,string product,string platform)
        {
            PageCodeView pageview = new PageCodeView();
            BasicCRUD dal = new BasicCRUD();
            try
            {
                int page = Convert.ToInt32(pageStr);
                int limit = Convert.ToInt32(limitStr);
              
                List<CodeView> views = new List<CodeView>();
                IEnumerable<Code> subCode;
                if (page == 0 && limit == 0)
                {
                    subCode = codes;
                }
                else
                {
                    page = page == 0 ? -1 : page;
                    subCode = codes.Skip((page - 1) * limit).Take(limit);
                }
                if (product != null)
                {
                    subCode= subCode.Where(c => (c.Products!=null?c.Products.ToLower().Contains(product):false));
                }
                if (platform != null)
                {
                    subCode = subCode.Where(c => (c.Platform != null ? c.Platform.ToLower().Contains(platform) : false));
                }
                pageview.Total = subCode.Count();
                CodeView view = new CodeView();
                foreach (var item in subCode)
                {
                    view = new CodeView();
                    view.ID = item.id;
                    view.Author = item.Author;
                    view.Description = item.Description;
                    view.GitHubUrl = item.GitHubUrl;
                    view.LastUpateDate = item.LastUpdateDate;
                    view.Link = item.Link;
                    var takeTimeEntity = dal.GetEntities<CodeOwnership>(c => c.FkId == item.id && c.Type == "code").OrderByDescending(c => c.LogAt).FirstOrDefault();
                    if (takeTimeEntity != null)
                    {
                        var takeTime = takeTimeEntity.LogAt;
                        view.NewCommit = Basic.ConvertCommitToCommitView(dal.GetEntities<Commit>(c => c.GitHubUrl == item.GitHubUrl && c.CreateAt >= takeTime), dal);
                        view.NewIssue = Basic.ConvertIssueToIssueView(dal.GetEntities<Issue>(c => c.CodeID == item.id && c.CreateAt >= takeTime), dal);
                    }

                    view.Platforms = Basic.stringToList(item.Platform);
                    view.Products = Basic.stringToList(item.Products);
                    view.SyncDate = item.SyncDate;
                    view.Title = item.Title;
                    var alias = dal.GetEntities<CodeOwnership>(c => c.FkId == item.id && c.Type == "code").ToList().FirstOrDefault();
                    view.Alias = alias != null ? alias.support_alias : null;

                    var process = dal.GetEntities<ProcessLog>(c => c.FkId == item.id && c.Type == "code").OrderByDescending(c => c.LogAT).FirstOrDefault();
                    if (process != null)
                    {
                        view.Process = dal.GetEntities<Process>(c => c.id == process.ProcessID).First().name;
                    }

                    views.Add(view);
                }
                pageview.Views = views;
            }
            catch (Exception)
            {

                throw;
            }
           

           return pageview;
        }
    
    }
}