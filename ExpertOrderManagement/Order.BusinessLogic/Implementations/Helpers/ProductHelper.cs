using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ProductHelper : Helper<Product>, IProductHelper
    {
        public ProductHelper(string tName, int companyId)
            : base(tName, companyId)
        {

        }
        public IEnumerable<Product> CheckDuplicateName(string Name, string Code)
        {
            if (string.IsNullOrEmpty(Code))
            {
                return DBHelper.ConvertToEnumerable<Product>("select * from " + base._tableName + " where ClientCompanyId = " + base._companyId + " and Name='" + Name + "'");
            }
            else
            {
                return DBHelper.ConvertToEnumerable<Product>("select * from " + base._tableName + " where ClientCompanyId = " + base._companyId + " and Name='" + Name + "' and Code!='" + Code + "'");
            }

        }
    }
}
