using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniFinder.Controllers
{
    public class WebsiteController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();
        // GET: Website
        public ActionResult Index()
        {
            try
            {
                var cities = db.Cities.ToList();
                return View(cities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult City_Universities(int id)
        {
            try
            {
                City cityList = db.Cities.Where(x => x.CityID == id).FirstOrDefault();
                Session["City"] = cityList.Name;
                Session["Country"] = cityList.Country;
                var uni = db.UniversityTbls.Where(x => x.Status == true).ToList();
                return View(uni);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult About()
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
        public ActionResult Programs(int id)
        {
            try
            {
                if (id > 0)
                {
                    UniversityTbl university = db.UniversityTbls.Where(x => x.UniID == id).FirstOrDefault();
                    Session["UniName"] = university.UniName;
                    Session["UniID"] = university.UniID;
                    var programs = db.ProgramTbls.Where(x => x.Status == true && x.UniID == university.UniID).ToList();
                    return View(programs);
                }
                else
                {
                    var programs = db.ProgramTbls.Where(x => x.Status == true).ToList();                   
                    return View(programs);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult StudenApplicationForm()
        {
            return View();
        }
        public ActionResult Slider()
        {
            return View();
        }

        public ActionResult StudenApplicationFormTable()
        {
            try
            {
                if (Session["Usertype"].ToString() == "Student")
                {
                    int ids = Convert.ToInt32(Session["UserID"]);
                    return View(db.StudentAdmissionForms.Where(x => x.FormID != null && x.CreatedBy == ids).ToList());
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}