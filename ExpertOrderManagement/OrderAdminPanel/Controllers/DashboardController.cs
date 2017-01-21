using Admin.BusinessLogic;
using OrderAdminPanel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderAdminPanel.Controllers
{
     [AuthorizeWebFormAttribute]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            return View();
        }

        #region CHANGE PASSWORD

        public ActionResult ChangePassword()
        {
            var CurrentUser = (User)Session["User"];
            //var user = new User();
            //if (CurrentUser != null)
            //{
            //    user.UserName = CurrentUser.UserName;
            //    user.UserType = CurrentUser.UserType;
            //}
            return View(CurrentUser);
        }


        [HttpPost]
        public ActionResult ChangePassword(User model)
        {
            var CurrentUser = (User)Session["User"];
            ModelState.Clear();
            if (string.IsNullOrEmpty(model.Password))
            {
                return RedirectToAction("ChangePassword");
            }
            if (CurrentUser != null && StringCipher.Decrypt(CurrentUser.Password) == model.OldPassword)
            {
                model.Id = CurrentUser.Id;
                if (model.ChangePassword() > 0)
                {
                    ModelState.AddModelError("", "Password has been changed successfully.");
                    model.OldPassword = "";
                    ((User)Session["User"]).Password = StringCipher.Encrypt(model.Password);
                }
            }
            else
            {
                ModelState.AddModelError("", "Your Oldpassword is not matched in our database, Please enter correct old password.");
            }
            model.UserName = CurrentUser.UserName;
            return View(model);
        }

        #endregion        

        #region UPDATE PROFILE

        public ActionResult UpdateProfile()
        {
            var currUser = (Admin.BusinessLogic.User)Session["User"];
            return View(currUser);
        }

        [ValidateOnlyIncomingValuesAttribute]
        [HttpPost]
        public ActionResult UpdateProfile(User model)
        {
            var currUser = (Admin.BusinessLogic.User)Session["User"];
            if (ModelState.IsValid)
            {
                model.UserType = currUser.UserType;
                if (model.Save() > 0)
                {
                    Session["User"] = model;
                    ModelState.AddModelError("", "Profile Updated successfully.");
                    return View(currUser);
                }
                else
                {
                    ModelState.AddModelError("", "Error while updating profile, Please try after sometime.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        #endregion

        

        #region REPORTS

        public ActionResult ClientDataUploadReport()
        {
            var users = Admin.BusinessLogic.User.DistributorRechargeInformationReport(null, null);
            return View(users);
        }

        #endregion

        

    }
}
