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
    }
}
