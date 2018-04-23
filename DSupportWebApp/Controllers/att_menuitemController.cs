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
    public class att_menuitemController : Controller
    {
        private dsupportwebappEntities db = new dsupportwebappEntities();

        public PartialViewResult GetMenuItem_att()

        {
            return PartialView("GetMenuItem_att", db.att_menuitem.ToList());
        }

        // GET: att_menuitem
        public ActionResult Index()
        {
            return View(db.att_menuitem.ToList());
        }

        // GET: att_menuitem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            att_menuitem att_menuitem = db.att_menuitem.Find(id);
            if (att_menuitem == null)
            {
                return HttpNotFound();
            }
            return View(att_menuitem);
        }

        // GET: att_menuitem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: att_menuitem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDMenuitem,IDMenu,IDUserGroup,SortID,Name_NL,Name_EN,Controller,Action,ID,IDUserCreated,IDUserModified,DateCreated,DateModified")] att_menuitem att_menuitem)
        {
            if (ModelState.IsValid)
            {
                db.att_menuitem.Add(att_menuitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(att_menuitem);
        }

        // GET: att_menuitem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            att_menuitem att_menuitem = db.att_menuitem.Find(id);
            if (att_menuitem == null)
            {
                return HttpNotFound();
            }
            return View(att_menuitem);
        }

        // POST: att_menuitem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDMenuitem,IDMenu,IDUserGroup,SortID,Name_NL,Name_EN,Controller,Action,ID,IDUserCreated,IDUserModified,DateCreated,DateModified")] att_menuitem att_menuitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(att_menuitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(att_menuitem);
        }

        // GET: att_menuitem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            att_menuitem att_menuitem = db.att_menuitem.Find(id);
            if (att_menuitem == null)
            {
                return HttpNotFound();
            }
            return View(att_menuitem);
        }

        // POST: att_menuitem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            att_menuitem att_menuitem = db.att_menuitem.Find(id);
            db.att_menuitem.Remove(att_menuitem);
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
