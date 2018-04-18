using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DSupportWebApp.Models;

namespace DSupportWebApp.Controllers
{
    public class att_subController : Controller
    {
        private dsupportwebappEntities db = new dsupportwebappEntities();

        // GET: att_sub
        public ActionResult Index()
        {
            return View(db.att_sub.ToList());
        }

        // GET: att_sub/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            att_sub att_sub = db.att_sub.Find(id);
            if (att_sub == null)
            {
                return HttpNotFound();
            }
            return View(att_sub);
        }

        // GET: att_sub/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: att_sub/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAttSub,IDAttMain,SortOrder,NameNL,NameEN,IDUserCreated,IDUserModified,DateCreated,DateModified")] att_sub att_sub)
        {
            if (ModelState.IsValid)
            {
                db.att_sub.Add(att_sub);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(att_sub);
        }

        // GET: att_sub/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            att_sub att_sub = db.att_sub.Find(id);
            if (att_sub == null)
            {
                return HttpNotFound();
            }
            return View(att_sub);
        }

        // POST: att_sub/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAttSub,IDAttMain,SortOrder,NameNL,NameEN,IDUserCreated,IDUserModified,DateCreated,DateModified")] att_sub att_sub)
        {
            if (ModelState.IsValid)
            {
                db.Entry(att_sub).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(att_sub);
        }

        // GET: att_sub/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            att_sub att_sub = db.att_sub.Find(id);
            if (att_sub == null)
            {
                return HttpNotFound();
            }
            return View(att_sub);
        }

        // POST: att_sub/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            att_sub att_sub = db.att_sub.Find(id);
            db.att_sub.Remove(att_sub);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
