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
    public class ProgramTblsController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();

        // GET: ProgramTbls
        public ActionResult Index()
        {
            try
            {
                ViewBag.DegreeData = new SelectList(db.Degrees, "Name", "Name");
                var programTbls = db.ProgramTbls.Include(p => p.Tradetbl).Include(p => p.UniversityTbl);
                return View(programTbls.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult GetTrade(string universityId)
        {
            try
            {
                int uniId;
                List<SelectListItem> tradeNames = new List<SelectListItem>();
                if (!string.IsNullOrEmpty(universityId))
                {
                    uniId = Convert.ToInt32(universityId);
                    List<Tradetbl> tradeList = db.Tradetbls.Where(x => x.UniID == uniId).ToList();
                    tradeList.ForEach(x =>
                    {
                        tradeNames.Add(new SelectListItem { Text = x.TradeName, Value = x.ID.ToString() });
                    });
                }
                return Json(tradeNames, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Create()
        {
            try
            {
                ViewBag.DegreeData = new SelectList(db.Degrees, "Name", "Name");
                //ViewBag.TradeID = new SelectList(db.Tradetbls, "ID", "TradeName");
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName");
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: ProgramTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgramID,ProgramName,Degree,TradeID,UniID,Status,creationdate")] ProgramTbl programTbl, HttpPostedFileBase ImageFile)
        {
            try
            {
                string duplicatename = db.ProgramTbls.Where(c => c.ProgramName == programTbl.ProgramName && c.TradeID == programTbl.TradeID && c.UniID == programTbl.UniID).Select(cc => cc.ProgramName).FirstOrDefault();
                if (string.IsNullOrEmpty(duplicatename))
                {
                    if (ModelState.IsValid)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                        string extension = Path.GetExtension(ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        programTbl.Picture = "/Assets/website_assets/ProgramImages/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Assets/website_assets/ProgramImages/"), fileName);
                        ImageFile.SaveAs(fileName);
                        programTbl.Status = true;
                        programTbl.creationdate = DateTime.Now;
                        db.ProgramTbls.Add(programTbl);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    ViewBag.DegreeData = new SelectList(db.Degrees, "Name", "Name", programTbl.Degree);
                    ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", programTbl.UniID);
                    ViewBag.TradeID = new SelectList(db.Tradetbls, "ID", "TradeName", programTbl.TradeID);
                    return View(programTbl);
                }
                else
                {
                    TempData["uniname"] = "Program name already exist with same university and trade.";
                    TempData.Keep();
                    ViewBag.DegreeData = new SelectList(db.Degrees, "Name", "Name", programTbl.Degree);
                    ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", programTbl.UniID);
                    ViewBag.TradeID = new SelectList(db.Tradetbls, "ID", "TradeName", programTbl.TradeID);
                    return View(programTbl);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: ProgramTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ProgramTbl programTbl = db.ProgramTbls.Find(id);
                if (programTbl == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DegreeData = new SelectList(db.Degrees, "Name", "Name", programTbl.Degree);
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", programTbl.UniID);
                ViewBag.TradeID = new SelectList(db.Tradetbls, "ID", "TradeName", programTbl.TradeID);
                return View(programTbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: ProgramTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProgramID,ProgramName,Degree,TradeID,UniID,Status,creationdate")] ProgramTbl programTbl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string duplicatename = db.ProgramTbls.Where(c => c.ProgramName == programTbl.ProgramName.ToLower() && c.ProgramID != programTbl.ProgramID).Select(cc => cc.ProgramName.ToLower()).FirstOrDefault();
                    if (string.IsNullOrEmpty(duplicatename))
                    {
                        db.Entry(programTbl).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.DuplicateName = duplicatename + " name already exist. Please enter new name.";
                        return View(programTbl);
                    }
                }
                ViewBag.DegreeData = new SelectList(db.Degrees, "Name", "Name", programTbl.Degree);
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", programTbl.UniID);
                ViewBag.TradeID = new SelectList(db.Tradetbls, "ID", "TradeName", programTbl.TradeID);
                return View(programTbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: ProgramTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ProgramTbl programTbl = db.ProgramTbls.Find(id);
                if (programTbl == null)
                {
                    return HttpNotFound();
                }

                db.ProgramTbls.Remove(programTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["deletename"] = "Program cannot be deleted, because it was part of application process.";
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
