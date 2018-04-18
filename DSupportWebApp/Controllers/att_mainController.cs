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
    public class att_mainController : Controller
    {
        private dsupportwebappEntities db = new dsupportwebappEntities();

        // GET: att_main
        public ActionResult Index()
        {
            return View(db.att_main.ToList());
        }

        // GET: att_main/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            att_main att_main = db.att_main.Find(id);
            if (att_main == null)
            {
                return HttpNotFound();
            }
            return View(att_main);
        }

        // GET: att_main/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: att_main/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAttMain,NameNL,NameEN,IDUserCreated,IDUserModified,DateCreated,DateModified")] att_main att_main)
        {
            if (ModelState.IsValid)
            {
                db.att_main.Add(att_main);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(att_main);
        }

        // GET: att_main/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            att_main att_main = db.att_main.Find(id);
            if (att_main == null)
            {
                return HttpNotFound();
            }
            return View(att_main);
        }

        // POST: att_main/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAttMain,NameNL,NameEN,IDUserCreated,IDUserModified,DateCreated,DateModified")] att_main att_main)
        {
            if (ModelState.IsValid)
            {
                db.Entry(att_main).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(att_main);
        }

        // GET: att_main/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            att_main att_main = db.att_main.Find(id);
            if (att_main == null)
            {
                return HttpNotFound();
            }
            return View(att_main);
        }

        // POST: att_main/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            att_main att_main = db.att_main.Find(id);
            db.att_main.Remove(att_main);
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
