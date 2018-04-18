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
    public class asis_paramController : AsisBaseController
    {
        public override Type GetAsisObjectModelType()
        {
            base.GetAsisObjectModelType();
            return typeof(asis_param);
        }

        // GET: asis_param
        public ActionResult Index()
        {
            

            var result = View(db.asis_param.ToList());
            //return base.Intialize(result);
            return View();
        }

        // GET: asis_param/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asis_param asis_param = db.asis_param.Find(id);
            if (asis_param == null)
            {
                return HttpNotFound();
            }
            return View(asis_param);
        }

        // GET: asis_param/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: asis_param/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDParam,KeyNL,KeyEN,Value,IDUserCreated,IDUserModified,DateCreated,DateModified")] asis_param asis_param)
        {
            if (ModelState.IsValid)
            {
                db.asis_param.Add(asis_param);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asis_param);
        }

        // GET: asis_param/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asis_param asis_param = db.asis_param.Find(id);
            if (asis_param == null)
            {
                return HttpNotFound();
            }

            //base.BeforeEdit(asis_param);

            return View(asis_param);
        }

        // POST: asis_param/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDParam,KeyNL,KeyEN,Value,IDUserCreated,IDUserModified,DateCreated,DateModified")] asis_param asis_param)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asis_param).State = EntityState.Modified;
                db.SaveChanges();
                //base.AfterEdit(asis_param,1, Convert.ToInt32(Session["IDUser"]), "asis_param");
               return RedirectToAction("Index");
            }
            return View(asis_param);
        }

        // GET: asis_param/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asis_param asis_param = db.asis_param.Find(id);
            if (asis_param == null)
            {
                return HttpNotFound();
            }
            return View(asis_param);
        }

        // POST: asis_param/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            asis_param asis_param = db.asis_param.Find(id);
            db.asis_param.Remove(asis_param);
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
