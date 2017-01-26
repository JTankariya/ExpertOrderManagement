using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewExpert.Helpers
{
    public class AuthorizeWebFormAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var loginUrl = "~/Account/Login";
            if (HttpContext.Current.Session["User"] == null)
            {
                filterContext.Result = new RedirectResult(loginUrl);
            }
            //if (Convert.ToString(filterContext.RouteData.Values["controller"]) == "POS")
            //{
            //    if (Convert.ToInt32(SessionManager.Instance.POSShiftLogged) < 1)
            //    {
            //        filterContext.Result = new RedirectResult("~/");
            //    }
            //}
            //else
            //{
            //    if (ActiveMachines.lstActiveMachines.Any(x => x.MachineName == SessionManager.Instance.MachineName && x.LastUpdate.AddMinutes(1) < DateTime.Now))
            //        filterContext.Result = new RedirectResult(loginUrl);
            //}
        }
    }
}