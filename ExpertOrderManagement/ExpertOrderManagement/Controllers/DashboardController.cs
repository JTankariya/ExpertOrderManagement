using CommonLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpertOrderManagement.Controllers
{
    [AuthorizeWebForm]
    public class DashboardController : BaseController
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetDefaultCompany(int CompanyId)
        {
            var result = currUser.Manager.SaveSetting(1, Convert.ToString(CompanyId));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
