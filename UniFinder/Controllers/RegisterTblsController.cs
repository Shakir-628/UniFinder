using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using UniFinder;
using UniFinder.Class;

namespace UniFinder.Controllers
{
    public class RegisterTblsController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();

        // GET: RegisterTbls
        public void gendertype()
        {
            ViewBag.genderlist = new SelectList(new[]
{
    new { ID = "Male", Gender = "Male" },
    new { ID = "Female", Gender = "Female" },
},
"ID", "Gender");
        }


        //        public void UniName()
        //        {
        //            ViewBag.unilist = new SelectList(new[]
        //    {
        //    new { ID = "KU", Uni = "Karachi University" },
        //    new { ID = "FAST", Uni= "FAST" },
        //    new { ID = "IBA", Uni = "IBA" },
        //    new { ID = "SSUET", Uni = "SSUET" },
        //    new { ID = "PAFKIET", Uni = "PAF KIET" },
        //},
        //    "ID", "Uni");
        //        }

        public ActionResult Index()
        {
            return View(db.RegisterTbls.ToList());
        }

        // GET: RegisterTbls/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RegisterTbl registerTbl = db.RegisterTbls.Find(id);
                if (registerTbl == null)
                {
                    return HttpNotFound();
                }
                return View(registerTbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: RegisterTbls/Create
        public ActionResult Create()
        {
            try
            {
                gendertype();
                TempData["signupMsg"] = null;
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult CreateInstitute()
        {
            try
            {
                //UniName();
                TempData["signupMsg"] = null;
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: RegisterTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StdID,StdCode,Name,Password,Gender,CNIC,Type,Qualification,Email,City,DOB,Address,Mobile,Status,CreateDate")] RegisterTbl registerTbl)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    registerTbl.CreateDate = DateTime.Now;
                    if (registerTbl.Type == "Institute")
                    {
                        registerTbl.Status = false;
                        db.RegisterTbls.Add(registerTbl);
                        db.SaveChanges();
                        TempData["signupMsg"] = "Your ID has been created successfully, kindly login to your account";
                        TempData.Keep();
                        if (registerTbl.Email != null)
                        {
                            Class.Email e = new Class.Email();
                            e.EmailSend(registerTbl.Email);
                        }
                        return RedirectToAction("CreateInstitute");
                    }
                    else
                    {
                        var duplicateEmailCNIC = db.RegisterTbls.Where(c => c.Email == registerTbl.Email || c.CNIC == registerTbl.CNIC).Select(cc => cc.Email).FirstOrDefault();
                        if (string.IsNullOrEmpty(duplicateEmailCNIC))
                        {
                            registerTbl.Status = true;
                            registerTbl.Type = "Student";
                            db.RegisterTbls.Add(registerTbl);
                            db.SaveChanges();
                            TempData["signupMsg"] = "Your ID has been created successfully, kindly login to your account";
                            TempData.Keep();
                            return RedirectToAction("Login","Login");
                        }
                        else
                        {
                            TempData["signupMsg"] = "Email or CNIC duplication error.";
                            TempData.Keep();
                            return RedirectToAction("Create", registerTbl);
                        }
                    }
                }
                return View(registerTbl);
            }
            catch (Exception)
            {
                throw;
            }
        }



        // GET: RegisterTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RegisterTbl registerTbl = db.RegisterTbls.Find(id);
                if (registerTbl == null)
                {
                    return HttpNotFound();
                }
                return View(registerTbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: RegisterTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StdID,StdCode,Name,Password,Gender,CNIC,Qualification,Status,CreateDate")] RegisterTbl registerTbl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(registerTbl).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(registerTbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: RegisterTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RegisterTbl registerTbl = db.RegisterTbls.Find(id);
                if (registerTbl == null)
                {
                    return HttpNotFound();
                }
                return View(registerTbl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: RegisterTbls/Delete/5
        public ActionResult Approve(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RegisterTbl registerTbl = db.RegisterTbls.Find(id);
                if (registerTbl == null)
                {
                    return HttpNotFound();
                }
                registerTbl.Status = true;
                db.Entry(registerTbl).State = EntityState.Modified;
                db.SaveChanges();
                if (registerTbl.Email != null)
                {
                    Class.Email e = new Class.Email();
                    e.EmailSend(registerTbl.Email);
                }

                TempData["lblMsg"] = "Registration email was sent successfully";
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
