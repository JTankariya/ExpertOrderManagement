using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Order.API
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            ExpertOrderBusinessInit.Initialize();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!Request.RawUrl.Contains("account/login"))
            {
                var test = Request.Headers;
                if (test == null || test.Get("UserName") == null || test.Get("Password") == null || string.IsNullOrEmpty(test.Get("UserName")) || string.IsNullOrEmpty(test.Get("Password")))
                {
                    throw new Exception("Please enter your credentials");
                }
            }            
        }
    }
}