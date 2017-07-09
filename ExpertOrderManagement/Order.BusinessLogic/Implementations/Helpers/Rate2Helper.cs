using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    class Rate2Helper : Helper<Rate2>, IRate2Helper
    {
        public Rate2Helper(string tName, int companyId)
            : base(tName, companyId)
        {

        }


        public List<Rate2> GetByRate(string Link)
        {
            if (!string.IsNullOrEmpty(Link))
            {
                return DBHelper.ConvertToList<Rate2>("select * from " + base._tableName + " where ClientCompanyId = " + base._companyId + " and Link='" + Link + "'");
            }
            else
            {
                return null;
            }
        }
    }
}
