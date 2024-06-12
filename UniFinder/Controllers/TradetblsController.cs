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
    public class TradetblsController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();

        // GET: Tradetbls
        public ActionResult Index()
        {
            try
            {
                var uniTbls = db.Tradetbls.Include(p => p.UniversityTbl);
                return View(db.Tradetbls.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Tradetbls/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Tradetbl tradetbl = db.Tradetbls.Find(id);
                if (tradetbl == null)
                {
                    return HttpNotFound();
                }
                return View(tradetbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Tradetbls/Create
        public ActionResult Create()
        {
            try
            {
                TempData["tradename"] = null;

                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName");
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Tradetbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TradeName,UniID,Status")] Tradetbl tradetbl, HttpPostedFileBase ImageFile)
        {
            try
            {
                string duplicatename = db.Tradetbls.Where(c => c.TradeName == tradetbl.TradeName && c.UniID == tradetbl.UniID).Select(cc => cc.TradeName).FirstOrDefault();
                if (string.IsNullOrEmpty(duplicatename))
                {
                    if (ModelState.IsValid)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                        string extension = Path.GetExtension(ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        tradetbl.Picture = "/Assets/website_assets/TradeImages/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Assets/website_assets/TradeImages/"), fileName);
                        ImageFile.SaveAs(fileName);
                        tradetbl.Status = true;
                        tradetbl.creationdate = DateTime.Now;
                        db.Tradetbls.Add(tradetbl);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", tradetbl.UniID);
                    return View(tradetbl);
                }
                else
                {
                    TempData["tradename"] = "Trade name already exist with same university.";
                    TempData.Keep();
                    ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", tradetbl.UniID);
                    return View(tradetbl);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Tradetbls/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Tradetbl tradetbl = db.Tradetbls.Find(id);
                if (tradetbl == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", tradetbl.UniID);
                return View(tradetbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Tradetbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TradeName,UniID,Status")] Tradetbl tradetbl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string duplicatetrade = db.Tradetbls.Where(c => c.TradeName == tradetbl.TradeName.ToLower() && c.ID != tradetbl.ID).Select(cc => cc.TradeName.ToLower()).FirstOrDefault();
                    if (string.IsNullOrEmpty(duplicatetrade))
                    {
                        db.Entry(tradetbl).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.DuplicateName = duplicatetrade + " name already exist. Please enter new name.";
                        return View(tradetbl);
                    }
                }
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", tradetbl.UniID);
                return View(tradetbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Tradetbls/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Tradetbl tradetbl = db.Tradetbls.Find(id);
                if (tradetbl == null)
                {
                    return HttpNotFound();
                }

                db.Tradetbls.Remove(tradetbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["deletename"] = "Trade cannot be deleted, because it was part of application process.";
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
