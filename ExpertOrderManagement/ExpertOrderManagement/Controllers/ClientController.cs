using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpertOrderManagement.Controllers
{
    public class ClientController : BaseController
    {
        public ActionResult Add(string Id)
        {
            if (!string.IsNullOrEmpty(Id) && Id != "0")
            {
                var client = ClientHelper.GetByRefId(Id);
                return View(client);
            }
            else
            {
                var client = new Client();
                return View(client);
            }

        }
        [HttpPost]
        public JsonResult Save(Client client)
        {
            return Json(client.Manager.Save(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string CheckDuplicateName(string CompanyName, string RefId)
        {
            var companies = ClientHelper.CheckDuplicateName(CompanyName, RefId);
            if (companies != null && companies.Count() > 0)
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }
        [HttpPost]
        public string CheckDuplicateUserName(string UserName, string RefId)
        {
            var companies = ClientHelper.CheckDuplicateUserName(UserName, RefId);
            if (companies != null && companies.Count() > 0)
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
            return View(ClientHelper.GetAll());
        }

        public JsonResult Delete(string ID)
        {
            return Json(ClientHelper.GetByRefId(ID).Manager.Delete(), JsonRequestBehavior.AllowGet);
        }

    }
}
