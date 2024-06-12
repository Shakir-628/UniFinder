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
    public class ReportingsController : Controller
    {
        private Uni_Find_DB_310324Entities db = new Uni_Find_DB_310324Entities();

        // GET: Reportings
        public ActionResult Index()
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
