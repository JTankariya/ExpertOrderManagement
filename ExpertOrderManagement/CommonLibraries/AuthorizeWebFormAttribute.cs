using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonLibraries
{
    public class AuthorizeWebFormAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var loginUrl = "~/Account/Login";
            if (System.Web.HttpContext.Current.Session["User"] == null)
            {
                filterContext.Result = new RedirectResult(loginUrl);
            }
        }
    }
}