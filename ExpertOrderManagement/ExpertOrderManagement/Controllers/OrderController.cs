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
            if (currUser.UserTypeId != 3)
            {
                ViewBag.Parties = PartyHelper.GetSundryDebtors();
            }

            ViewBag.Products = ProductHelper.GetAll();
            if (!string.IsNullOrEmpty(Id) && Id != "0")
            {
                var order = OrderHelper.GetByRefId(Id);
                return View(order);
            }
            else
            {
                var order = new Order();
                order.Ord_no = Helpers.OrderHelper.GetMaxOrderNo(currUser.DefaultCompany.ClientCompanyId);
                return View(order);
            }

        }
        [HttpPost]
        public JsonResult Save(Order order)
        {
            if (currUser.UserTypeId != 3)
            {
                if (order.ClientCompanyId == 0)
                {
                    order.ClientCompanyId = currUser.DefaultCompany.ClientCompanyId;
                    order.Type = "S";
                }
            }
            else
            {
                order.ClientCompanyId = (order.Adjusted ? currUser.WithoutCompanies.FirstOrDefault().ClientCompanyId : currUser.BillableCompanies.FirstOrDefault().ClientCompanyId);
                order.Code = currUser.PartyCode;
                order.Ord_no = Helpers.OrderHelper.GetMaxOrderNo(order.ClientCompanyId);
                order.Ord_Dt = DateTime.Now;
                order.Type = "S";
            }
            return Json(order.Manager.Save(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string CheckDuplicateOrderNo(string Ord_No)
        {
            var orders = OrderHelper.CheckDuplicateOrderNo(Ord_No);
            if (orders != null && orders.Count() > 0)
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
            return View(OrderHelper.GetAll());
        }

        public JsonResult GetRateSchemeRate(int partyId, int productId)
        {
            var rate = OrderHelper.GetRateByPartyProduct(partyId, productId);
            return Json(rate, JsonRequestBehavior.AllowGet);
        }
    }
}
