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
    public class asis_controllerview1Controller : Controller
    {
        private dsupportwebappEntities db = new dsupportwebappEntities();


        // GET: asis_controllerview1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asis_controllerview asis_controllerview = db.asis_controllerview.Find(id);
            if (asis_controllerview == null)
            {
                return HttpNotFound();
            }
            return View(asis_controllerview);
        }

        // GET: asis_controllerview1/Create
        public ActionResult Create()
        {
            return View();
        }



        // GET: asis_controllerview1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asis_controllerview asis_controllerview = db.asis_controllerview.Find(id);
            if (asis_controllerview == null)
            {
                return HttpNotFound();
            }
            return View(asis_controllerview);
        }

        // POST: asis_controllerview1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDControllerView,IDController,SortID,ViewName,IDUserCreated,IDUserModified,DateCreated,DateModified")] asis_controllerview asis_controllerview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asis_controllerview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asis_controllerview);
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
