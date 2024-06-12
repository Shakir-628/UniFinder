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
    public class PaymentToUniversityController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();

        // GET: PaymentToUniversity
        public ActionResult Index()
        {
            try
            {
                var paymentToUnis = db.PaymentToUnis.Include(p => p.FormStatu).Include(p => p.UniversityTbl);
                return View(paymentToUnis.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: PaymentToUniversity/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PaymentToUni paymentToUni = db.PaymentToUnis.Find(id);
                if (paymentToUni == null)
                {
                    return HttpNotFound();
                }
                return View(paymentToUni);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: PaymentToUniversity/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.FormID = new SelectList(db.FormStatus, "FormID", "FormCode");
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName");
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: PaymentToUniversity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UniTransID,CID,FormID,Amount,UniID,PaymentOption,CreateDate")] PaymentToUni paymentToUni)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PaymentToUnis.Add(paymentToUni);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.FormID = new SelectList(db.FormStatus, "FormID", "FormCode", paymentToUni.FormID);
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", paymentToUni.UniID);
                return View(paymentToUni);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: PaymentToUniversity/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PaymentToUni paymentToUni = db.PaymentToUnis.Find(id);
                if (paymentToUni == null)
                {
                    return HttpNotFound();
                }
                ViewBag.FormID = new SelectList(db.FormStatus, "FormID", "FormCode", paymentToUni.FormID);
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", paymentToUni.UniID);
                return View(paymentToUni);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: PaymentToUniversity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UniTransID,CID,FormID,Amount,UniID,PaymentOption,CreateDate")] PaymentToUni paymentToUni)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(paymentToUni).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.FormID = new SelectList(db.FormStatus, "FormID", "FormCode", paymentToUni.FormID);
                ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", paymentToUni.UniID);
                return View(paymentToUni);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: PaymentToUniversity/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PaymentToUni paymentToUni = db.PaymentToUnis.Find(id);
                if (paymentToUni == null)
                {
                    return HttpNotFound();
                }
                return View(paymentToUni);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: PaymentToUniversity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PaymentToUni paymentToUni = db.PaymentToUnis.Find(id);
                db.PaymentToUnis.Remove(paymentToUni);
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
