﻿using BusinessLogic;
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
        private int _currCompany = 15;
        private IProductGroupHelper _productGroupHelper;
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

        protected int CompanyId
        {
            get
            {
                return _currCompany;
            }
        }

        protected IUserHelper UserHelper
        {
            get
            {
                if (_userHelper == null)
                {
                    _userHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IUserHelper>>()
                        .Create(TableNames.USER.ToString(), CompanyId);
                }
                return _userHelper;
            }
        }

        protected IProductGroupHelper ProductGroupHelper
        {
            get
            {
                if (_productGroupHelper == null)
                {
                    _productGroupHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IProductGroupHelper>>()
                        .Create(TableNames.PRODUCTGROUP.ToString(), CompanyId);
                }
                return _productGroupHelper;
            }
        }
    }
}
