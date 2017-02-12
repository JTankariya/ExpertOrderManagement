using BusinessLogic;
using CommonLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpertOrderManagement.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            var isRemember = Request.Cookies["ExpertOrderManagement"];
            if (isRemember != null)
            {
                var client = new ClientUser(Convert.ToString(isRemember.Values["UName"]));
                if (client.Id > 0)
                {
                    Session["User"] = client;
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View(new ClientUser());
        }

        [HttpPost]
        public ActionResult Login(ClientUser employee)
        {
            var client = new ClientUser(employee.UserName, employee.Password);
            if (client.Id > 0)
            {
                if (Request.Form["IsRemember"] != null && Request.Form["IsRemember"].ToUpper() == "ON")
                {
                    HttpCookie cookie = new HttpCookie("ExpertOrderManagement");
                    cookie.Values.Add("UName", employee.UserName);
                    cookie.Values.Add("PWord", employee.Password);
                    cookie.Expires = DateTime.Now.AddDays(15);
                    Response.Cookies.Add(cookie);
                }
                Session["User"] = client;
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "User Name with given password is not found in database, Please enter proper username and Password");
            }
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(string EmailId)
        {
            ResponseMsg response = new ResponseMsg();
            response.IsSuccess = true;
            if (!string.IsNullOrEmpty(EmailId))
            {
                var existingUser = UserHelper.GetByEmail(EmailId);

                if (existingUser != null)
                {
                    var body = "DEAR, <b><i>" + (existingUser.FirstName + existingUser.LastName) + "</i></b><br><br>Your credentials for Mehul Industries system is as below :<br><br>User Name : <b>" + existingUser.UserName +
                       "</b><br>Password : <b>" + StringCipher.Decrypt(existingUser.Password) +
                       "</b><br><br>Please use above credentials to login into system.<br><br>Thanks & Regards,<br>Shah Infotech";
                    if (Mailer.SendMail(existingUser.EmailId, body, "Forget Password : Mehul Industries"))
                    {
                        return Json(response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ResponseValue = "Error while sending mail, Please try after sometime.";
                        return Json(response);
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseValue = "No record found with given email Id, Please enter proper emailid.";
                    return Json(response);
                }
            }
            else
            {
                return Json(response);
            }
        }

        [AuthorizeWebForm]
        public ActionResult UpdateProfile()
        {
            return View(currUser);
        }

        [AuthorizeWebForm]
        [HttpPost]
        public ActionResult UpdateProfile(ClientUser employee)
        {

            ResponseMsg response = new ResponseMsg();
            employee.Password = StringCipher.Decrypt(currUser.Password);
            employee.UserTypeId = currUser.UserTypeId;
            employee.Manager.Save();
            Session["User"] = new ClientUser(currUser.Id);
            response.IsSuccess = true;
            return Json(response);
        }

        [AuthorizeWebForm]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [AuthorizeWebForm]
        [HttpPost]
        public ActionResult ChangePassword(string Password)
        {
            ResponseMsg response = new ResponseMsg();
            currUser.Password = Password;
            currUser.Manager.Save();
            Session["User"] = UserHelper.GetById(currUser.Id);
            response.IsSuccess = true;
            return Json(response);
        }
        [AuthorizeWebForm]

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("MehulIndustries");
            cookie.Values.Add("UName", "");
            cookie.Values.Add("PWord", "");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            Session["User"] = null;
            return RedirectToAction("LogIn");
        }
    }
}
