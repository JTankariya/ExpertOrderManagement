using BusinessLogic;
using DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class UserManager : IUserManager
    {
        private ClientUser _context;
        public UserManager(ClientUser context)
        {
            _context = context;
        }
        public ResponseMsg Save()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", _context.Id);
            DBHelper.ExecuteNonQuery("Order.SaveClient", param, true);
            return new ResponseMsg() { IsSuccess = true };
        }


        public ResponseMsg Delete()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserSettings> GetSettings()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UserId", _context.Id);
            return DBHelper.ConvertToEnumerable<UserSettings>(DBHelper.GetDataTable("Order.GetUserSettings", param, true));
        }


        public ResponseMsg SaveSetting(int settingId, string value)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@SettingId", settingId);
            param.Add("@UserId", _context.Id);
            param.Add("@Value", value);
            DBHelper.ExecuteNonQuery("Order.SaveUserSettings", param, true);
            _context.IsToRefresh = true;
            return new ResponseMsg() { IsSuccess = true };
        }
    }
}
