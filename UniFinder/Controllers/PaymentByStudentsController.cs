using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using UniFinder;

namespace UniFinder.Controllers
{
    public class PaymentByStudentsController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();

        // GET: PaymentByStudents
        public ActionResult Index()
        {
            try
            {
                var paymentByStudents = db.PaymentByStudents;
                return View(paymentByStudents.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: PaymentByStudents/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PaymentByStudent paymentByStudent = db.PaymentByStudents.Find(id);
                if (paymentByStudent == null)
                {
                    return HttpNotFound();
                }
                return View(paymentByStudent);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: PaymentByStudents/Create
        public ActionResult Create(int id)
        {
            try
            {

                //ViewBag.TradeID = new SelectList(db.Tradetbls, "ID", "TradeName");
                //ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName");
                paymentType();
                StudentAdmissionForm studentAdmissionForm = db.StudentAdmissionForms.Where(x => x.ApplicationID == id).FirstOrDefault();
                PaymentByStudent paymentByStudent = new PaymentByStudent();
                paymentByStudent.Uni = studentAdmissionForm.UniID;
                paymentByStudent.Program = studentAdmissionForm.ProgramID;
                ViewBag.FormID = new SelectList(db.StudentAdmissionForms.Where(x => x.ApplicationID == id), "ApplicationID", "FormID");
                return View(paymentByStudent);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: PaymentByStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransID,StdID,FormCode,Uni,Program,ApttCharges,ServiceCharges,TotalAmount,PaymentOption,CreateDate,Status")] PaymentByStudent paymentByStudent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StudentAdmissionForm form = db.StudentAdmissionForms.Where(x => x.ApplicationID.ToString() == paymentByStudent.FormCode).FirstOrDefault();

                    if (paymentByStudent.PaymentOption != null)
                    {
                        form.PaymentStatus = "Submitted";
                        db.Entry(form).State = EntityState.Modified;
                        db.SaveChanges();
                        paymentByStudent.Status = false;
                        paymentByStudent.StdID = Convert.ToInt32(Session["UserID"]);
                        paymentByStudent.CreateDate = DateTime.Now;
                        paymentByStudent.FormCode = form.FormID;
                        paymentByStudent.TotalAmount = paymentByStudent.ApttCharges + paymentByStudent.ServiceCharges;
                        paymentByStudent.Remarks = "Pending";
                        //paymentByStudent.UniID = Convert.ToInt32(Session["UniID"]);
                        //paymentByStudent.TradeID = Convert.ToInt32(Session["TradeID"]);
                        db.PaymentByStudents.Add(paymentByStudent);
                        db.SaveChanges();
                        return RedirectToAction("StudenApplicationFormTable", "Website");
                    }
                    else
                    {
                        TempData["lblMsg"] = "Please select payment method";
                        TempData.Keep();
                        return RedirectToAction("Create", "PaymentByStudents", form.ApplicationID);
                    }

                }

                //ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", paymentByStudent.UniID);
                //ViewBag.TradeID = new SelectList(db.Tradetbls, "ID", "TradeName", paymentByStudent.TradeID);
                ViewBag.FormID = new SelectList(db.FormStatus, "FormID", "FormCode", paymentByStudent.FormCode);
                return View(paymentByStudent);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: PaymentByStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PaymentByStudent paymentByStudent = db.PaymentByStudents.Find(id);
                if (paymentByStudent == null)
                {
                    return HttpNotFound();
                }
                //ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", paymentByStudent.UniID);
                //ViewBag.TradeID = new SelectList(db.Tradetbls, "ID", "TradeName", paymentByStudent.TradeID);
                ViewBag.FormID = new SelectList(db.FormStatus, "FormID", "FormCode", paymentByStudent.FormCode);
                return View(paymentByStudent);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: PaymentByStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransID,StdID,FormID,UniId,TradeId,ApttCharges,ServiceCharges,TotalAmount,PaymentOption,CreateDate,Status")] PaymentByStudent paymentByStudent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(paymentByStudent).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //ViewBag.UniID = new SelectList(db.UniversityTbls, "UniID", "UniName", paymentByStudent.UniID);
                //ViewBag.TradeID = new SelectList(db.Tradetbls, "ID", "TradeName", paymentByStudent.TradeID);
                ViewBag.FormID = new SelectList(db.FormStatus, "FormID", "FormCode", paymentByStudent.FormCode);
                return View(paymentByStudent);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: PaymentByStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PaymentByStudent paymentByStudent = db.PaymentByStudents.Find(id);
                if (paymentByStudent == null)
                {
                    return HttpNotFound();
                }
                return View(paymentByStudent);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: PaymentByStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PaymentByStudent paymentByStudent = db.PaymentByStudents.Find(id);
                db.PaymentByStudents.Remove(paymentByStudent);
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
        public ActionResult ApprovePayment(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PaymentByStudent payment = db.PaymentByStudents.Find(id);
                if (payment == null)
                {
                    return HttpNotFound();
                }
                if (payment.Remarks == "Rejected" || payment.Remarks == "Approved")
                {
                    TempData["lblMsg"] = "Action already taken,(" + payment.Remarks + ")";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }
                else
                {
                    payment.Remarks = "Approved";
                    payment.Status = true;
                    db.Entry(payment).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["lblMsg"] = "Payment has been approved";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult RejectPayment(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PaymentByStudent payment = db.PaymentByStudents.Find(id);
                if (payment == null)
                {
                    return HttpNotFound();
                }
                if (payment.Remarks == "Rejected" || payment.Remarks == "Approved")
                {
                    TempData["lblMsg"] = "Action already taken,(" + payment.Remarks + ")";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }
                else
                {
                    payment.Status = false;
                    payment.Remarks = "Rejected";
                    db.Entry(payment).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["lblMsg"] = "Payment has been rejected";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void paymentType()
        {
            ViewBag.paymentTypelist = new SelectList(new[]
{
    new { ID = "Easy Paisa", Name = "Easy Paisa" },
    new { ID = "Jazz Cash", Name = "Jazz Cash" },
    new { ID = "UBL OMNI", Name = "UBL OMNI" },
    new { ID = "IBFT", Name = "IBFT" },
},
"ID", "Name");
        }
    }
}
