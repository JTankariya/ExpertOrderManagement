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
    public class ClientController : BaseController
    {
        public ActionResult Add(string refId)
        {
            if (!string.IsNullOrEmpty(refId))
            {
                var client = ClientHelper.GetByRefId(refId);
                client.Password = StringCipher.Decrypt(client.Password);
                return View(client);
            }
            else
            {
                var client = new Client();
                client.AccountExpiredOn = DateTime.Now.AddYears(1);
                return View(client);
            }

        }
        [HttpPost]
        public JsonResult Save(Client client)
        {
            client.Password = StringCipher.Encrypt(client.Password);
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
            return View(ClientHelper.GetAll("Order.GetAllClientMaster"));
        }

        public JsonResult Delete(string ID)
        {
            return Json(ClientHelper.GetByRefId(ID).Manager.Delete(), JsonRequestBehavior.AllowGet);
        }

    }
}
