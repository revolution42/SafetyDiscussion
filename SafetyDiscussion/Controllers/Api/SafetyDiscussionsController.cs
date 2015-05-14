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
using PrototypeApp;
using PrototypeApp.Models;

namespace PrototypeApp.Controllers.Api
{
    public class SafetyDiscussionsController : ApiController
    {
        private PrototypeAppContext db = new PrototypeAppContext();

        // GET: api/SafetyDiscussions
        public IQueryable<SafetyDiscussion> GetSafetyDiscussions()
        {
            return db.SafetyDiscussions;
        }

        // GET: api/SafetyDiscussions/5
        [ResponseType(typeof(SafetyDiscussion))]
        public IHttpActionResult GetSafetyDiscussion(int id)
        {
            SafetyDiscussion safetyDiscussion = db.SafetyDiscussions.Find(id);
            if (safetyDiscussion == null)
            {
                return NotFound();
            }

            return Ok(safetyDiscussion);
        }

        // PUT: api/SafetyDiscussions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSafetyDiscussion(int id, SafetyDiscussion safetyDiscussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != safetyDiscussion.Id)
            {
                return BadRequest();
            }

            db.Entry(safetyDiscussion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SafetyDiscussionExists(id))
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

        // POST: api/SafetyDiscussions
        [ResponseType(typeof(SafetyDiscussion))]
        public IHttpActionResult PostSafetyDiscussion(SafetyDiscussion safetyDiscussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SafetyDiscussions.Add(safetyDiscussion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = safetyDiscussion.Id }, safetyDiscussion);
        }

        // DELETE: api/SafetyDiscussions/5
        [ResponseType(typeof(SafetyDiscussion))]
        public IHttpActionResult DeleteSafetyDiscussion(int id)
        {
            SafetyDiscussion safetyDiscussion = db.SafetyDiscussions.Find(id);
            if (safetyDiscussion == null)
            {
                return NotFound();
            }

            db.SafetyDiscussions.Remove(safetyDiscussion);
            db.SaveChanges();

            return Ok(safetyDiscussion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SafetyDiscussionExists(int id)
        {
            return db.SafetyDiscussions.Count(e => e.Id == id) > 0;
        }
    }
}