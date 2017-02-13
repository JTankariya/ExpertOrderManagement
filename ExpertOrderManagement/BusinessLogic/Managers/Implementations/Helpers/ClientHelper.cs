using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ClientHelper : Helper<Client>, IClientHelper
    {
        public ClientHelper(string tName, int companyId)
            : base(tName, companyId)
        { 
        
        }

        public IEnumerable<Client> CheckDuplicateName(string ClientName, string RefId)
        {
            if (string.IsNullOrEmpty(RefId) || RefId == "00000000-0000-0000-0000-000000000000")
            {
                return DBHelper.ConvertToEnumerable<Client>("select * from " + base._tableName + " where CompanyName='" + ClientName + "'");
            }
            else
            {
                return DBHelper.ConvertToEnumerable<Client>("select * from " + base._tableName + " where CompanyName='" + ClientName + "' and RefId!='" + RefId + "'");
            }

        }
        public IEnumerable<Client> CheckDuplicateUserName(string UserName, string RefId)
        {
            if (string.IsNullOrEmpty(RefId) || RefId == "00000000-0000-0000-0000-000000000000")
            {
                return DBHelper.ConvertToEnumerable<Client>("select * from " + base._tableName + " where UserName='" + UserName + "'");
            }
            else
            {
                return DBHelper.ConvertToEnumerable<Client>("select * from " + base._tableName + " where UserName='" + UserName + "' and RefId!='" + RefId + "'");
            }

        }
    }
}
