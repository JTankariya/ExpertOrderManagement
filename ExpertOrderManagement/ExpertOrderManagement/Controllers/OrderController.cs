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
    public class OrderController : BaseController
    {
        public ActionResult Add(string Id)
        {
            ViewBag.Parties = PartyHelper.GetSundryDebtors();
            ViewBag.Products = ProductHelper.GetAll();
            if (!string.IsNullOrEmpty(Id) && Id != "0")
            {
                var order = OrderHelper.GetByRefId(Id);
                return View(order);
            }
            else
            {
                var order = new Order();
                return View(order);
            }

        }
        [HttpPost]
        public JsonResult Save(Order order)
        {
            if (order.ClientCompanyId == 0)
            {
                order.ClientCompanyId = currUser.DefaultCompany.ClientCompanyId;
            }
            return Json(order.Manager.Save(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAll()
        {
            return View(OrderHelper.GetAll());
        }

    }
}
