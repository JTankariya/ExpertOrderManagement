using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class OrderHelper : Helper<Order>, IOrderHelper
    {
        public OrderHelper(string tName, int companyId)
            : base(tName, companyId)
        {

        }

        public List<Order> CheckDuplicateOrderNo(string Ord_No)
        {
            if (!string.IsNullOrEmpty(Ord_No))
            {
                return DBHelper.ConvertToList<Order>("select * from " + base._tableName + " where ClientCompanyId = " + base._companyId + " and Ord_No='" + Ord_No + "'");
            }
            else
            {
                return null;
            }
        }


        public List<Rate2> GetRateByPartyProduct(int partyId, int productId)
        {
            return null;
        }

        public string GetMaxOrderNo(int clientCompanyId)
        {
            return clientCompanyId.ToString("000") + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm");
        }
    }
}
