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
    public class CampaignController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();

        // GET: Campaign
        public ActionResult Index()
        {
            try
            {
                int userid = Convert.ToInt32(Session["UserID"]);
                dynamic campaigntbls = null;
                if (Session["Usertype"].ToString() == "Institute")
                {
                    campaigntbls = db.Campaigntbls.Where(x => x.userid == userid).ToList();
                }
                else
                {
                    campaigntbls = db.Campaigntbls.ToList();
                }
                return View(campaigntbls);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Campaign/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Campaigntbl campaigntbl = db.Campaigntbls.Find(id);
                if (campaigntbl == null)
                {
                    return HttpNotFound();
                }
                return View(campaigntbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Campaign/Create
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

        // POST: Campaign/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CampID,CampRequestID,UniID,PostingDate,StartDate,EndDate,Description,Status,UniDepartment,CreateDate,Image,City,UpdateDate,userid")] Campaigntbl campaigntbl, HttpPostedFileBase ImageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    campaigntbl.Image = "~/Assets/Images_Campaign/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Assets/Images_Campaign/"), fileName);
                    ImageFile.SaveAs(fileName);
                    campaigntbl.userid = Convert.ToInt32(Session["UserID"]);
                    db.Campaigntbls.Add(campaigntbl);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", campaigntbl.UniID);
                return View(campaigntbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Campaign/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Campaigntbl campaigntbl = db.Campaigntbls.Find(id);
                if (campaigntbl == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", campaigntbl.UniID);
                return View(campaigntbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Campaign/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CampID,CampRequestID,UniID,PostingDate,StartDate,EndDate,Description,Status,UniDepartment,CreateDate,Image,City,UpdateDate,userid")] Campaigntbl campaigntbl, HttpPostedFileBase ImageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    campaigntbl.Image = "~/Assets/Images_Campaign/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Assets/Images_Campaign/"), fileName);
                    ImageFile.SaveAs(fileName);
                    db.Entry(campaigntbl).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", campaigntbl.UniID);
                return View(campaigntbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Campaign/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Campaigntbl campaigntbl = db.Campaigntbls.Find(id);
                if (campaigntbl == null)
                {
                    return HttpNotFound();
                }
                db.Campaigntbls.Remove(campaigntbl);
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
