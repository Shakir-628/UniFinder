using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using UniFinder;

namespace UniFinder.Controllers
{
    public class UniversityController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();

        // GET: University
        public ActionResult Index()
        {
            try
            {
                return View(db.UniversityTbls.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: University/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UniversityTbl universityTbl = db.UniversityTbls.Find(id);
                if (universityTbl == null)
                {
                    return HttpNotFound();
                }
                return View(universityTbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: University/Create
        public ActionResult Create()
        {
            try
            {
                TempData["uniname"] = null;
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: University/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UniID,UniName,CreateDate,Status")] UniversityTbl universityTbl, HttpPostedFileBase ImageFile)
        {
            try
            {
                string duplicateuniname = db.UniversityTbls.Where(c => c.UniName == universityTbl.UniName).Select(cc => cc.UniName).FirstOrDefault();
                if (string.IsNullOrEmpty(duplicateuniname))
                {
                    if (ModelState.IsValid)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                        string extension = Path.GetExtension(ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        universityTbl.Picture = "/Assets/website_assets/UniversityLogo/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Assets/website_assets/UniversityLogo/"), fileName);
                        ImageFile.SaveAs(fileName);
                        universityTbl.CreateDate = DateTime.Now;
                        universityTbl.Status = true;
                        db.UniversityTbls.Add(universityTbl);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(universityTbl);
                }

                else
                {
                    TempData["uniname"] = "Name already used.";
                    TempData.Keep();
                    return View(universityTbl);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: University/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                UniversityTbl universityTbl = db.UniversityTbls.Find(id);
                var duplicateuni = db.StudentAdmissionForms.Where(c => c.UniID == universityTbl.UniName).Select(cc => cc.UniID).FirstOrDefault();

                if (string.IsNullOrEmpty(duplicateuni))
                {
                    universityTbl = db.UniversityTbls.Find(id);
                    return View(universityTbl);
                }
                else
                {
                    TempData["deleteuniname"] = duplicateuni + " cannot be edit bcz it already used by student.";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: University/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UniID,UniName,CreateDate,Status")] UniversityTbl universityTbl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string duplicateuni = db.UniversityTbls.Where(c => c.UniName == universityTbl.UniName.ToLower() && c.UniID != universityTbl.UniID).Select(cc => cc.UniName.ToLower()).FirstOrDefault();
                    if (string.IsNullOrEmpty(duplicateuni))
                    {
                        db.Entry(universityTbl).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.DuplicateName = duplicateuni + " name already exist. Please enter new name.";
                        return View(universityTbl);
                    }
                }
                return View(universityTbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: University/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UniversityTbl universityTbl = db.UniversityTbls.Find(id);
                if (universityTbl == null)
                {
                    return HttpNotFound();
                }

                db.UniversityTbls.Remove(universityTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["deleteuniname"] = "University cannot be deleted, because it was part of application process.";
                TempData.Keep();
                return RedirectToAction("Index");
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
