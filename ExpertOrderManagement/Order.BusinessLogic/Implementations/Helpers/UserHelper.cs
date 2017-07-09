using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class UserHelper : Helper<ClientUser>, IUserHelper
    {
        public UserHelper(string tName,int copmanyId)
            : base(tName, copmanyId)
        { 
        
        }
        public ClientUser GetByEmail(string Email)
        {
            return DBHelper.ConvertToEnumerable<ClientUser>("select * from " + base._tableName + " where Email='" + Email + "'").FirstOrDefault();
        }
    }
}
