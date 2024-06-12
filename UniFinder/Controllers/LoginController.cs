using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniFinder;

namespace UniFinder.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public void usertype()
        {
            ViewBag.list = new SelectList(new[]
{
    new { ID = "1", Name = "Admin" },
    new { ID = "2", Name = "Student" },
    new { ID = "3", Name = "Institute" },
},
"ID", "Name");
        }
        public ActionResult Login()
        {
            try
            {
                if (Session["Username"] == null)
                {
                    usertype();
                    return View();
                }
                else
                {
                    if (Session["Usertype"].ToString() == "Admin")
                    {
                        return RedirectToAction("Dashboard", "Dashboard");
                    }
                    else if (Session["Usertype"].ToString() == "Student")
                    {
                        return RedirectToAction("Index", "Website");
                    }
                    else if (Session["Usertype"].ToString() == "Institute")
                    {
                        return RedirectToAction("Dashboard", "Dashboard");
                    }
                    return View();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Logout()
        {
            try
            {
                System.Web.HttpContext.Current.Session.Abandon(); // it will clear the session at the end of request
                System.Web.HttpContext.Current.Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();
                Session["Username"] = null;
                Session["UserID"] = null;
                Session["Usertype"] = null;
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                string msg = "<script>alert('Something went wrong')</script>";
                return RedirectToAction("Login", Content(msg, "text/html"));
            }

        }
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]

        public ActionResult Login(RegisterTbl user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities())
                    {
                        var obj = db.RegisterTbls.Where(a => a.Email.Equals(user.Email) && a.Password.Equals(user.Password) && a.Status == true).FirstOrDefault();
                        if (obj != null && user.Qualification != "")
                        {
                            Session["Username"] = obj.Name;
                            Session["Email"] = obj.Email;
                            Session["UserID"] = obj.StdID.ToString();
                            Session["Usertype"] = obj.Type;

                            if (user.Qualification == "1" && obj.Type == "Admin")
                            {
                                return RedirectToAction("Dashboard", "Dashboard");
                            }
                            else if (user.Qualification == "2" && obj.Type == "Student")
                            {
                                return RedirectToAction("Index", "Website");
                            }
                            else if (user.Qualification == "3" && obj.Type == "Institute")
                            {
                                return RedirectToAction("Dashboard", "Dashboard");
                            }
                            else
                            {
                                TempData["loginError"] = "Wrong username or password, please try again";
                                TempData.Keep();
                            }

                        }
                        else
                        {
                            usertype();
                            TempData["loginError"] = "Wrong username or password, please try again";
                        }
                    }
                }
                usertype();
                return View(user);
            }
            catch (Exception)
            {
                usertype();
                string msg = "<script>alert('Something went wrong')</script>";
                return Content(msg, "text/html");
            }
        }

        public ActionResult DashBoard()
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    if (Session["roles"].ToString() == "Admin")
                    {

                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception)
            {
                string msg = "<script>alert('Something went wrong')</script>";
                return Content(msg, "text/html");
            }
        }

    }
}