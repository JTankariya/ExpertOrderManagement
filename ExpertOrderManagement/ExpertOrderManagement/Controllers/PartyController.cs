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
    public class PartyController : BaseController
    {
        public ActionResult Add(string Id)
        {
            ViewBag.GroupList = PartyGroupHelper.GetAll()
                .Where(x => (!string.IsNullOrEmpty(x.Code) && x.Code.Equals(Constants.DEFAULT_SUNDRY_DEBTORS_CODE)) || 
                    (!string.IsNullOrEmpty(x.Under) && x.Under.Equals(Constants.DEFAULT_SUNDRY_DEBTORS_CODE)));
            if (!string.IsNullOrEmpty(Id) && Id != "0")
            {
                var party = PartyHelper.GetByRefId(Id);
                return View(party);
            }
            else
            {
                var party = new Party();
                return View(party);
            }

        }
        [HttpPost]
        public JsonResult Save(Party party)
        {
            if (party.ClientCompanyId == 0)
            {
                party.ClientCompanyId = currUser.DefaultCompany.ClientCompanyId;
            }
            return Json(party.Manager.Save(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string CheckDuplicateName(string Name, string Code)
        {
            var parties = PartyHelper.CheckDuplicateName(Name, Code);
            if (parties != null && parties.Count > 0)
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }

        public ActionResult GetAll()
        {
            return View(PartyHelper.GetAll());
        }

    }
}
