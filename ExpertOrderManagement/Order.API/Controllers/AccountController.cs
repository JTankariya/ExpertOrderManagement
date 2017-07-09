using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Order.API.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        public ResponseMsg Login(ClientUser employee)
        {
            ResponseMsg response = new ResponseMsg();
            if (employee == null || string.IsNullOrEmpty(employee.UserName) || string.IsNullOrEmpty(employee.UserName))
            {
                response.IsSuccess = false;
                response.ResponseValue = "Please enter UserName and Password.";
                return response;
            }
            var user = new ClientUser(employee.UserName, employee.Password);
            if (user.Id > 0)
            {
                LoggedInUser.User = user;
                var defaultCompanySetting = user.Settings.Where(x => x.SettingId == 1);
                if ((defaultCompanySetting == null || defaultCompanySetting.Count() == 0) && (user.UserTypeId == 2 || user.UserTypeId == 3))
                {
                    user.Manager.SaveSetting(1, Convert.ToString(user.Client.BillableCompanies.FirstOrDefault().ClientCompanyId));
                }
                response.IsSuccess = true;
            }
            else
            {
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
