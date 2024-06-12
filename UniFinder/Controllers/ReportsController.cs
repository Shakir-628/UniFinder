using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniFinder.Controllers
{
    public class ReportsController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();
        // GET: Reports
        public ActionResult StudentEnrolledReport()
        {

            try
            {
                var StudentEnrolled = db.StudentAdmissionReport();
                return View("", StudentEnrolled);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult UniversityReport()
        {
            try
            {
                var UniversityReport = db.UniversityReport();
                return View(UniversityReport);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}