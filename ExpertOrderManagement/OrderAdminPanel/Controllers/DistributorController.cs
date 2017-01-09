using Admin.BusinessLogic;
using OrderAdminPanel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderAdminPanel.Controllers
{
    [AuthorizeWebForm]
    public class DistributorController : Controller
    {
        //
        // GET: /Distributor/

        public ActionResult BalanceReport()
        {
            var CurrentUser = (User)Session["User"];
            var filename = Admin.BusinessLogic.User.GenerateDistributorBalanceReport(CurrentUser);
            return Json(new { IsSuccess = true, ResponseValue = filename }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RechargeInformationReport(DateTime? FromDate, DateTime? ToDate)
        {
            if (!FromDate.HasValue && !ToDate.HasValue)
            {
                var users = Admin.BusinessLogic.User.DistributorRechargeInformationReport(FromDate, ToDate);
                return View(users);
            }
            else
            {
                var users = Admin.BusinessLogic.User.DistributorRechargeInformationReport(FromDate, ToDate);
                return PartialView("DistributorRechargeRow", users);
            }
        }

        public ActionResult DoRecharge()
        {
            var recharge = new RechargeTransaction();
            var allUsers = Admin.BusinessLogic.User.GetAllDistributors();
            if (allUsers != null)
            {
                ViewBag.DistributorList = allUsers.Select(x => new SelectListItem() { Text = x.FirstName + " " + x.LastName, Value = Convert.ToString(x.Id) }).ToList();
            }
            else
            {
                ViewBag.DistributorList = new List<SelectListItem>();
            }
            return View(recharge);
        }

        [ValidateOnlyIncomingValues]
        [HttpPost]
        public ActionResult DoRecharge(RechargeTransaction model)
        {
            if (ModelState.IsValid)
            {
                var currUser = (Admin.BusinessLogic.User)Session["User"];
                model.CreatedBy = currUser.Id;
                model.ClientId = 0;
                ViewBag.DistributorList = Admin.BusinessLogic.User.GetAllDistributors().Select(x => new SelectListItem() { Text = x.FirstName + " " + x.LastName, Value = Convert.ToString(x.Id) }).ToList();
                if (model.Save() > 0)
                {
                    ModelState.AddModelError("", "Recharge Transaction has been recorded successfully.");
                    return View(new RechargeTransaction());
                }
                else
                {
                    ModelState.AddModelError("", "Some problem occured while adding record to database, Please try after sometime.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult GetList()
        {
            var currUser = (Admin.BusinessLogic.User)Session["User"];
            var distributors = Admin.BusinessLogic.User.GetAllDistributors();
            return View(distributors);
        }

        public ActionResult Delete(int ID)
        {
            if (ID > 0)
            {
                if (Admin.BusinessLogic.User.Delete(ID, "DISTRIBUTOR") > 0)
                {
                    ModelState.AddModelError("", "Distributor Deleted Successfully.");
                }
                else
                {
                    ModelState.AddModelError("", "Error while deleting Admin, Please try after sometime.");
                }

            }

            return RedirectToAction("AddDistributor");
        }

        [ValidateOnlyIncomingValues]
        [HttpPost]
        public ActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                model.AdminID = ((Admin.BusinessLogic.User)Session["User"]).Id;
                if (model.Save() > 0)
                {
                    ModelState.AddModelError("", "Distributor is saved successfully.");
                    return View(new User());
                }
                else
                {
                    ModelState.AddModelError("", "Some problem occured while saving record, Please try after sometime.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Edit(string ID)
        {
            if (!string.IsNullOrEmpty(ID) && Convert.ToInt32(ID) > 0)
            {
                var user = Admin.BusinessLogic.User.GetDistributorByID(ID);
                return View(user);
            }
            else
            {
                return View(new User());
            }
        }
    }
}
