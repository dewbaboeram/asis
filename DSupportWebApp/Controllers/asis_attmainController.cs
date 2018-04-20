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
    public class asis_attmainController : AsisBaseController
    {
          public override Type GetAsisObjectModelType()
        {

            base.GetAsisObjectModelType();
            return typeof(asis_attmain);
        }


        // GET: asis_attmain
        public ActionResult Index()
        {
            return View(db.asis_attmain.ToList());
        }

        // GET: asis_attmain/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asis_attmain asis_attmain = db.asis_attmain.Find(id);
            if (asis_attmain == null)
            {
                return HttpNotFound();
            }
            return View(asis_attmain);
        }

        // GET: asis_attmain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: asis_attmain/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAttMain,Name_NL,Name_EN,IDUserCreated,IDUserModified,DateCreated,DateModified")] asis_attmain asis_attmain)
        {
            if (ModelState.IsValid)
            {
                db.asis_attmain.Add(asis_attmain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asis_attmain);
        }

        // GET: asis_attmain/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asis_attmain asis_attmain = db.asis_attmain.Find(id);
            if (asis_attmain == null)
            {
                return HttpNotFound();
            }
            return View(asis_attmain);
        }

        // POST: asis_attmain/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAttMain,Name_NL,Name_EN,IDUserCreated,IDUserModified,DateCreated,DateModified")] asis_attmain asis_attmain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asis_attmain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asis_attmain);
        }

        // GET: asis_attmain/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asis_attmain asis_attmain = db.asis_attmain.Find(id);
            if (asis_attmain == null)
            {
                return HttpNotFound();
            }
            return View(asis_attmain);
        }

        // POST: asis_attmain/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            asis_attmain asis_attmain = db.asis_attmain.Find(id);
            db.asis_attmain.Remove(asis_attmain);
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
