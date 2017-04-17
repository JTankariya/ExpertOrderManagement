using Ninject.Parameters;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibraries;
using System.Web;

namespace BusinessLogic
{
    public class Helpers
    {
        private static ClientUser CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session["User"] != null)
                {
                    return ((ClientUser)HttpContext.Current.Session["User"]);
                }
                else
                {
                    return null;
                }
            }
        }

        private static ClientCompany CurrentCompany
        {
            get
            {
                if (CurrentUser != null)
                {
                    return new ClientCompany(CurrentUser.Client.BillableCompanies.FirstOrDefault().ClientCompanyId);
                }
                else
                {
                    return null;
                }
            }
        }

        private static IUserTypeHelper _userTypeHelper;
        public static IUserTypeHelper UserTypeHelper
        {
            get
            {
                if (_userTypeHelper == null)
                {
                    _userTypeHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IUserTypeHelper>>()
                        .Create(TableNames.USERTYPES.ToString(), CurrentCompany.ClientCompanyId);
                }
                return _userTypeHelper;
            }
        }

        private static IRate2Helper _rate2Helper;
        public static IRate2Helper Rate2Helper
        {
            get
            {
                if (_rate2Helper == null)
                {
                    _rate2Helper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IRate2Helper>>()
                        .Create(TableNames.RATE2.ToString(), CurrentCompany.ClientCompanyId);
                }
                return _rate2Helper;
            }
        }

        private static IClientHelper _clientHelper;
        public static IClientHelper ClientHelper
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

        private static IPartyGroupHelper _partyGroupHelper;
        public static IPartyGroupHelper PartyGroupHelper
        {
            get
            {
                if (_partyGroupHelper == null)
                {
                    _partyGroupHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IPartyGroupHelper>>()
                        .Create(TableNames.PARTYGROUP.ToString(), CurrentUser.DefaultCompany.ClientCompanyId);
                }
                return _partyGroupHelper;
            }
        }

        private static IPartyHelper _partyHelper;
        public static IPartyHelper PartyHelper
        {
            get
            {
                if (_partyHelper == null)
                {
                    _partyHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IPartyHelper>>()
                        .Create(TableNames.PARTY.ToString(), CurrentUser.DefaultCompany.ClientCompanyId);
                }
                return _partyHelper;
            }
        }

        private static ISettingHelper _settingHelper;
        public static ISettingHelper SettingHelper
        {
            get
            {
                if (_settingHelper == null)
                {
                    _settingHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, ISettingHelper>>()
                        .Create(TableNames.SETTING.ToString(), CurrentCompany.ClientCompanyId);
                }
                return _settingHelper;
            }
        }
        private static IOrderHelper _orderHelper;
        public static IOrderHelper OrderHelper
        {
            get
            {
                if (_orderHelper == null)
                {
                    _orderHelper = ExpertOrderBusinessInit.kernel.Get<IHelperFactory<string, int, IOrderHelper>>()
                        .Create(TableNames.ORDER.ToString(), CurrentCompany.ClientCompanyId);
                }
                return _orderHelper;
            }
        }
    }
}
