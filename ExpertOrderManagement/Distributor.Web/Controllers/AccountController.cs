using Distributor.BusinessLogic;
using Distributor.DAL;
using NewExpert.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NewExpert.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult LogIn()
        {
            var cookievalue = Request.Cookies["ExpertMobileConfig"];
            if (cookievalue != null && Session["User"] != null)
            {
                var cookieuser = cookievalue.Values["UName"];
                if (cookieuser != null)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View(new User());
        }

        [ValidateOnlyIncomingValuesAttribute]
        [HttpPost]
        public ActionResult LogIn([Bind(Include = "UserName,Password")] User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var userDB = user.CheckUserByUserNameAndPassword();

            if (userDB != null && userDB.UserName == user.UserName)
            {
                HttpCookie cookie = new HttpCookie("ExpertMobileConfig");
                cookie.Values.Add("UName", user.UserName);
                if (Request.Form["IsRemember"] != null && Request.Form["IsRemember"].ToUpper() == "ON")
                {
                    cookie.Expires = DateTime.Now.AddDays(15);
                }
                else
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                }
                Response.Cookies.Add(cookie);
                Session["User"] = userDB;
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "User Name with given password is not found in database, Please enter proper username and Password");
            }

            return View(new User());
        }

        [HttpPost]
        public ActionResult CheckUserName(string UserName, int ExcludeId)
        {
            bool IsFound = false;
            var user = new User();
            user.UserName = UserName;
            var users = user.CheckUserByUserName();
            if (users != null)
            {
                if (users.Count() > 1)
                {
                    IsFound = true;
                }
                else
                {
                    if (ExcludeId > 0)
                    {
                        if (users.Count() == 1 && users.FirstOrDefault().Id == ExcludeId)
                        {
                            IsFound = false;
                        }
                        else
                        {
                            IsFound = true;
                        }
                    }
                    else
                    {
                        if (users.Count() > 0)
                        {
                            IsFound = true;
                        }
                        else
                        {
                            IsFound = false;
                        }
                    }
                }
            }
            else
            {
                IsFound = false;
            }

            return Json(new { valid = !IsFound });
        }

        [HttpPost]
        public ActionResult CheckEmailId(string EmailId, int ExcludeId)
        {
            bool IsFound = false;
            var user = new User();
            user.EmailId = EmailId;
            var users = user.CheckUserByEmailId();
            if (users != null)
            {
                if (users.Count() > 1)
                {
                    IsFound = true;
                }
                else
                {
                    if (ExcludeId > 0)
                    {
                        if (users.Count() == 1 && users.FirstOrDefault().Id == ExcludeId)
                        {
                            IsFound = false;
                        }
                        else
                        {
                            IsFound = true;
                        }
                    }
                    else
                    {
                        if (users.Count() > 0)
                        {
                            IsFound = true;
                        }
                        else
                        {
                            IsFound = false;
                        }
                    }
                }
            }
            else
            {
                IsFound = false;
            }
            return Json(new { valid = !IsFound });
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(string Email)
        {
            if (!string.IsNullOrEmpty(Email))
            {
                User user = new User();
                user.EmailId = Email;
                var users = user.CheckUserByEmailId();
                if (users != null && users.Count() > 0)
                {
                    user = users.FirstOrDefault();
                    var body = "DEAR, <b><i>" + user.FirstName + " " + user.LastName +
                        "</i></b><br><br>Your credentials for Expert Mobile Config are as below :<br><br>User Name : <b>" + user.UserName +
                        "</b><br>Password : <b>" + StringCipher.Decrypt(user.Password) +
                        "</b><br><br>Please use above credentials to login into system.<br><br>Thanks & Regards,<br>Shah Infotech";
                    if (SendMail(Email, body, "Forget Password : Expert Mobile Config"))
                    {
                        return View();
                    }
                }
            }
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateOnlyIncomingValuesAttribute]
        public ActionResult SignUp([Bind(Include = "FirstName,LastName,EmailId")]User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            user.UserName = user.EmailId;
            user.Password = StringCipher.Encrypt("jayeshTest");
            user.CreateAccount();
            return RedirectToAction("LogIn", "Account");
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("ExpertMobileConfig");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            Session["User"] = null;
            return RedirectToAction("LogIn", "Account");
        }

        public bool SendMail(string to, string body, string subject)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["MailFrom"]));
            message.Subject = "Your email subject";
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = Convert.ToString(ConfigurationManager.AppSettings["MailFrom"]),
                    Password = Convert.ToString(ConfigurationManager.AppSettings["MailPassword"])
                };
                smtp.Credentials = credential;
                smtp.Host = Convert.ToString(ConfigurationManager.AppSettings["MailHost"]);
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"]);
                smtp.Timeout = 200000;
                //smtp.EnableSsl = true;

                try
                {
                    smtp.Send(message);
                    return true;
                }
                catch
                {
                    return false;
                }

            }

            //return View(model);
        }
    }
}
