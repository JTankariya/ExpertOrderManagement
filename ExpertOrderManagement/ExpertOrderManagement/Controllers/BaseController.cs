
using BusinessLogic;
using BusinessLogic.App_Start;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;
using CommonLibraries;


namespace ExpertOrderManagement.Controllers
{
    public class BaseController : Controller
    {
        private IUserHelper _userHelper;
        //
        // GET: /BaseController/
        protected User currUser
        {
            get
            {
                if (Session["User"] != null)
                {
                    return (User)Session["User"];
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

        protected IUserHelper UserHelper
        {
            get
            {
                if (_userHelper == null)
                {
                    _userHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, IUserHelper>>(new ConstructorArgument("tName", TableNames.USER)).Create(TableNames.USER.ToString());
                }
                return _userHelper;
            }
        }
    }
}
