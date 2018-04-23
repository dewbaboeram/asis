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
    public class asis_menuitemController : Controller
    {
        private dsupportwebappEntities db = new dsupportwebappEntities();

        public PartialViewResult GetMenuItem_asis()

        {
            return PartialView("GetMenuItem_asis",db.asis_menuitem.ToList());
        }



        // GET: asis_menuitem
        public ActionResult Index()
        {
            return View(db.asis_menuitem.ToList());
        }

        // GET: asis_menuitem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asis_menuitem asis_menuitem = db.asis_menuitem.Find(id);
            if (asis_menuitem == null)
            {
                return HttpNotFound();
            }
            return View(asis_menuitem);
        }

        // GET: asis_menuitem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: asis_menuitem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDMenuItem,IDMenu,IDUserGroup,NameNL,NameEN,Controller,Action,ImgLevel,IDUserCreated,IDUserModified,DateCreated,DateModified")] asis_menuitem asis_menuitem)
        {
            if (ModelState.IsValid)
            {
                db.asis_menuitem.Add(asis_menuitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asis_menuitem);
        }

        // GET: asis_menuitem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asis_menuitem asis_menuitem = db.asis_menuitem.Find(id);
            if (asis_menuitem == null)
            {
                return HttpNotFound();
            }
            return View(asis_menuitem);
        }

        // POST: asis_menuitem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDMenuItem,IDMenu,IDUserGroup,NameNL,NameEN,Controller,Action,ImgLevel,IDUserCreated,IDUserModified,DateCreated,DateModified")] asis_menuitem asis_menuitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asis_menuitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asis_menuitem);
        }

        // GET: asis_menuitem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asis_menuitem asis_menuitem = db.asis_menuitem.Find(id);
            if (asis_menuitem == null)
            {
                return HttpNotFound();
            }
            return View(asis_menuitem);
        }

        // POST: asis_menuitem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            asis_menuitem asis_menuitem = db.asis_menuitem.Find(id);
            db.asis_menuitem.Remove(asis_menuitem);
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
