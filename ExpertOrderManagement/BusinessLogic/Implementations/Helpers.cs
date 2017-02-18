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
    }
}
