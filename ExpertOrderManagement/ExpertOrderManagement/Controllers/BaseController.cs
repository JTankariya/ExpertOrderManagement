using BusinessLogic;
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
        private IPartyHelper _partyHelper;
        private IPartyGroupHelper _partyGroupHelper;
        private IOrderHelper _orderHelper;
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

        //protected int CompanyId
        //{
        //    get
        //    {
        //        if (_company == null)
        //        {
        //            _company = currUser.Client.BillableCompanies.FirstOrDefault();
        //        }                
        //        return _company.ClientCompanyId;
        //    }
        //    set
        //    {
        //        _company = new ClientCompany(value);
        //    }
        //}

        protected IUserHelper UserHelper
        {
            get
            {
                if (_userHelper == null)
                {
                    _userHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IUserHelper>>()
                        .Create(TableNames.USER.ToString(), currUser.DefaultCompany.ClientCompanyId);
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
                        .Create(TableNames.PRODUCTGROUP.ToString(), currUser.DefaultCompany.ClientCompanyId);
                }
                return _productGroupHelper;
            }
        }

        protected IPartyHelper PartyHelper
        {
            get
            {
                if (_partyHelper == null)
                {
                    _partyHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IPartyHelper>>()
                        .Create(TableNames.PARTY.ToString(), currUser.DefaultCompany.ClientCompanyId);
                }
                return _partyHelper;
            }
        }
        protected IPartyGroupHelper PartyGroupHelper
        {
            get
            {
                if (_partyGroupHelper == null)
                {
                    _partyGroupHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IPartyGroupHelper>>()
                        .Create(TableNames.PARTYGROUP.ToString(), currUser.DefaultCompany.ClientCompanyId);
                }
                return _partyGroupHelper;
            }
        }
        protected IOrderHelper OrderHelper
        {
            get
            {
                if (_orderHelper == null)
                {
                    _orderHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IOrderHelper>>()
                        .Create(TableNames.ORDER.ToString(), currUser.DefaultCompany.ClientCompanyId);
                }
                return _orderHelper;
            }
        }
        protected IClientHelper ClientHelper
        {
            get
            {
                if (_clientHelper == null)
                {
                    _clientHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IClientHelper>>()
                        .Create(TableNames.CLIENT.ToString(), 0);
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
                        .Create(TableNames.PRODUCT.ToString(), currUser.DefaultCompany.ClientCompanyId);
                }
                return _productHelper;
            }
        }
    }
}
