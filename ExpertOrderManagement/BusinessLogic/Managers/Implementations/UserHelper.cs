using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class UserHelper : Helper<User>, IUserHelper
    {
        public UserHelper(string tName)
            : base(tName)
        { 
        
        }
        public User GetByEmail(string Email)
        {
            return DBHelper.ConvertToEnumerable<User>("select * from " + base._tableName + " where Email='" + Email + "'").FirstOrDefault();
        }
    }
}
