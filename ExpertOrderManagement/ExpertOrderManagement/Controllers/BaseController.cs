
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ExpertOrderManagement.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /BaseController/
        protected Client currUser
        {
            get
            {
                if (Session["User"] != null)
                {
                    return (Client)Session["User"];
                }
                else
                {
                    return null;
                }
            }
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            ExceptionParameters param = new ExceptionParameters(filterContext.Exception);
            ExceptionLogic.AddException(param);
            base.OnException(filterContext);
        }

    }
}
