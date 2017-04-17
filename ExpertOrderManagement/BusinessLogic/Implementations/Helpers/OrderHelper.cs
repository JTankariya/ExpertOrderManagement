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
            var orderNo = 1;
            var dt = DBHelper.GetDataTable("select ISNULL(MAX(CONVERT(int,ISNULL( Ord_No,0))),0) as OrderNo from " + base._tableName + " where ClientCompanyId = " + clientCompanyId);
            if (dt != null || dt.Rows.Count > 0)
            {
                orderNo = Convert.ToInt32(dt.Rows[0]["OrderNo"].ToString()) + 1;
            }
            return orderNo.ToString();
        }
    }
}
