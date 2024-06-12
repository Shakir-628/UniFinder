using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniFinder.Controllers
{
    public class DashboardController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            try
            {
                Reporting reporting = new Reporting();
                reporting.TotalStudent = Convert.ToInt32(db.RegisterTbls.Where(x => x.Type == "Student").Count());
                reporting.ActiveStudent = Convert.ToInt32(db.RegisterTbls.Where(x => x.Type == "Student" && x.Status == true).Count());
                reporting.OffStudent = Convert.ToInt32(db.RegisterTbls.Where(x => x.Type == "Student" && x.Status == false).Count());
                reporting.EnrolledStudent = Convert.ToInt32(db.FormStatus.Where(x => x.Status == true).Count());

                return View(reporting);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}