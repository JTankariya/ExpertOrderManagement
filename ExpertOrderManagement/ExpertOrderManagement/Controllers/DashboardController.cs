using BusinessLogic;
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
            ResponseMsg response = new ResponseMsg();
            if (currUser.UserTypeId == 3)
            {
                if (CompanyId == 0)
                {
                    response = currUser.Manager.SaveSetting(1, Convert.ToString(currUser.Client.WithoutCompany.ClientCompanyId));
                }
                else
                {
                    response = currUser.Manager.SaveSetting(1, Convert.ToString(currUser.Client.BillableCompanies.FirstOrDefault().ClientCompanyId));
                }
            }
            else
            {
                response = currUser.Manager.SaveSetting(1, Convert.ToString(CompanyId));
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}
