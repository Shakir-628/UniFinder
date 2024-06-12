using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniFinder;

namespace UniFinder.Controllers
{
    public class DegreesController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();

        // GET: Degrees
        public ActionResult Index()
        {
            try
            {
                return View(db.Degrees.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Degrees/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Degree degree = db.Degrees.Find(id);
                if (degree == null)
                {
                    return HttpNotFound();
                }
                return View(degree);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Degrees/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Degrees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CreationDate,Status")] Degree degree)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Degrees.Add(degree);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(degree);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Degrees/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Degree degree = db.Degrees.Find(id);
                if (degree == null)
                {
                    return HttpNotFound();
                }
                return View(degree);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Degrees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CreationDate,Status")] Degree degree)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(degree).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(degree);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Degrees/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Degree degree = db.Degrees.Find(id);
                if (degree == null)
                {
                    return HttpNotFound();
                }
                return View(degree);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Degrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Degree degree = db.Degrees.Find(id);
                db.Degrees.Remove(degree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
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
