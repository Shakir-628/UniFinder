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
    public class CampusTblsController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();

        // GET: CampusTbls
        public ActionResult Index()
        {
            try
            {
                var campusTbls = db.CampusTbls.Include(c => c.UniversityTbl);
                return View(campusTbls.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: CampusTbls/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CampusTbl campusTbl = db.CampusTbls.Find(id);
                if (campusTbl == null)
                {
                    return HttpNotFound();
                }
                return View(campusTbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: CampusTbls/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName");
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: CampusTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CampusID,CampusName,Location,UniID,Status,CreationDate")] CampusTbl campusTbl)
        {
            try
            {
                string duplicatename = db.CampusTbls.Where(c => c.CampusName == campusTbl.CampusName && c.UniID == campusTbl.UniID).Select(cc => cc.CampusName).FirstOrDefault();
                if (string.IsNullOrEmpty(duplicatename))
                {
                    if (ModelState.IsValid)
                    {
                        campusTbl.Status = true;
                        db.CampusTbls.Add(campusTbl);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", campusTbl.UniID);
                    return View(campusTbl);
                }
                else
                {
                    TempData["campusname"] = "Campus name already exist with same university.";
                    TempData.Keep();
                    ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", campusTbl.UniID);
                    return View(campusTbl);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: CampusTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CampusTbl campusTbl = db.CampusTbls.Find(id);
                if (campusTbl == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", campusTbl.UniID);
                return View(campusTbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: CampusTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CampusID,CampusName,Location,UniID,Status,CreationDate")] CampusTbl campusTbl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(campusTbl).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", campusTbl.UniID);
                return View(campusTbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: CampusTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CampusTbl campusTbl = db.CampusTbls.Find(id);
                if (campusTbl == null)
                {
                    return HttpNotFound();
                }
                db.CampusTbls.Remove(campusTbl);
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
