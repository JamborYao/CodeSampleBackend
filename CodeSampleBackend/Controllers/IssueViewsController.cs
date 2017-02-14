using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CodeSampleBackend;
using System.Web.Http.Cors;

namespace CodeSampleBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IssueViewsController : ApiController
    {
        private MoonCakeCodeSampleEntities db = new MoonCakeCodeSampleEntities();

        // GET: api/IssueViews
        public IQueryable<IssueView> GetIssueViews()
        {
            return db.IssueViews;
        }

        // GET: api/IssueViews/5
        [ResponseType(typeof(IssueView))]
        public IHttpActionResult GetIssueView(int id)
        {
            IssueView issueView = db.IssueViews.Find(id);
            if (issueView == null)
            {
                return NotFound();
            }

            return Ok(issueView);
        }

        [ResponseType(typeof(IssueView))]
        public IHttpActionResult GetNewIssueView(int id,DateTime? taken)
        {
            return Ok(DAL.DALIssueView.GetNewIssueView(id, taken));
        }

        // PUT: api/IssueViews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIssueView(int id, IssueView issueView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != issueView.id)
            {
                return BadRequest();
            }

            db.Entry(issueView).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueViewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/IssueViews
        [ResponseType(typeof(IssueView))]
        public IHttpActionResult PostIssueView(IssueView issueView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IssueViews.Add(issueView);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (IssueViewExists(issueView.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = issueView.id }, issueView);
        }

        // DELETE: api/IssueViews/5
        [ResponseType(typeof(IssueView))]
        public IHttpActionResult DeleteIssueView(int id)
        {
            IssueView issueView = db.IssueViews.Find(id);
            if (issueView == null)
            {
                return NotFound();
            }

            db.IssueViews.Remove(issueView);
            db.SaveChanges();

            return Ok(issueView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IssueViewExists(int id)
        {
            return db.IssueViews.Count(e => e.id == id) > 0;
        }
    }
}