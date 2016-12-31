using BAL;
using BusinessLogic.Models;
using ExpertOrderManagement.Models;
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
                var client = new Client(Convert.ToString(isRemember.Values["UName"]));
                if (client.ID > 0)
                {
                    Session["User"] = client;
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View(new Client());
        }

        [HttpPost]
        public ActionResult Login(Client employee)
        {
            var client = new Client(employee.UserName, employee.Password);
            if (client.ID >0)
            {
                if (Request.Form["IsRemember"] != null && Request.Form["IsRemember"].ToUpper() == "ON")
                {
                    HttpCookie cookie = new HttpCookie("MehulIndustries");
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
                var existingUser = EmployeeLogic.GetEmployeeByID(0).FirstOrDefault(x => x.EmailId == EmailId);
                if (existingUser != null)
                {
                    var body = "DEAR, <b><i>" + existingUser.Name + "</i></b><br><br>Your credentials for Mehul Industries system is as below :<br><br>User Name : <b>" + existingUser.UserName +
                       "</b><br>Password : <b>" + StringCipher.Decrypt(existingUser.Password) +
                       "</b><br><br>Please use above credentials to login into system.<br><br>Thanks & Regards,<br>Shah Infotech";
                    if (CommonLogic.SendMail(existingUser.EmailId, body, "Forget Password : Mehul Industries"))
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
        public ActionResult UpdateProfile(Client employee)
        {
            ResponseMsg response = new ResponseMsg();
            employee.CreatedBy = employee.UpdatedBy = currUser.ID;
            employee.Password = StringCipher.Decrypt(currUser.Password);
            employee.Type = currUser.Type;
            EmployeeLogic.AddEmployee(employee);
            Session["User"] = EmployeeLogic.GetEmployeeByID(currUser.ID).FirstOrDefault();
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
            currUser.CreatedBy = currUser.UpdatedBy = currUser.ID;
            EmployeeLogic.AddEmployee(currUser);
            Session["User"] = EmployeeLogic.GetEmployeeByID(currUser.ID).FirstOrDefault();
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
