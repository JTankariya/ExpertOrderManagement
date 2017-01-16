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
    public class AdminController : Controller
    {
        public ActionResult Edit(string ID)
        {
            if (!string.IsNullOrEmpty(ID) && Convert.ToInt32(ID) > 0)
            {
                var admin = AdminMaster.GetAdminByID(ID);
                return View(admin);
            }
            else
            {
                return View(new AdminMaster());
            }

        }

        [ValidateOnlyIncomingValues]
        [HttpPost]
        public ActionResult Save(AdminMaster model)
        {
            if (ModelState.IsValid)
            {
                if (model.SaveAdmin() > 0)
                {
                    ModelState.AddModelError("", "Admin is saved successfully.");
                    return View(new AdminMaster());
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

        public ActionResult Delete(int ID)
        {
            if (ID > 0)
            {
                if (AdminMaster.Delete(ID) > 0)
                {
                    ModelState.AddModelError("", "Admin Deleted Successfully.");
                }
                else
                {
                    ModelState.AddModelError("", "Error while deleting Admin, Please try after sometime.");
                }

            }

            return RedirectToAction("AdminMaster");
        }

        public ActionResult GetList()
        {
            var currUser = (User)Session["User"];
            var admins = AdminMaster.GetAllAdminMaster();
            if (admins != null)
            {
                admins = admins.Where(x => x.ID != currUser.Id);
            }
            return View(admins);
        }

    }
}
