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
        private IProductGroupHelper _productGroupHelper;
        private IClientHelper _clientHelper;
        private IProductHelper _productHelper;
        private ClientCompany _company;
        //
        // GET: /BaseController/
        protected ClientUser currUser
        {
            get
            {
                if (Session["User"] != null)
                {
                    return (ClientUser)Session["User"];
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
                if (_company == null)
                {
                    _company = new ClientCompany(currUser.Id);
                }
                return _company.ClientCompanyId;
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
        protected IClientHelper ClientHelper
        {
            get
            {
                if (_clientHelper == null)
                {
                    _clientHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IClientHelper>>()
                        .Create(TableNames.CLIENT.ToString(), CompanyId);
                }
                return _clientHelper;
            }
        }

        protected IProductHelper ProductHelper
        {
            get
            {
                if (_productHelper == null)
                {
                    _productHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IProductHelper>>()
                        .Create(TableNames.PRODUCT.ToString(), CompanyId);
                }
                return _productHelper;
            }
        }
    }
}
