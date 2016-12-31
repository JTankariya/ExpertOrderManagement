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
        private Client _context;
        public UserManager(Client client)
        {
            _context = client;
        }
        public ResponseMsg Update()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", _context.ID);
            DBHelper.ExecuteNonQuery("SaveClient", param, true);
            return new ResponseMsg() { IsSuccess = true };
        }
    }
}
