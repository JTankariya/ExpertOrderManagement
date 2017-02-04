using DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ClientCompany
    {
        public int ClientCompanyId { get; set; }
        public int ClientId { get; set; }
        public string CompanyName { get; set; }

        public enum FIELDNAMES
        {
            CLIENTCOMPANYID = 1,
            CLIENTID = 2,
            COMPANYNAME = 3
        }

        public ClientCompany(int clientId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ClientId", clientId);
            DataTable dt = DBHelper.GetDataTable("Order.GetClientCompany", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                ClientCompanyId = Convert.ToInt32(dt.Rows[0][FIELDNAMES.CLIENTCOMPANYID.ToString()]);
                ClientId = Convert.ToInt32(dt.Rows[0][FIELDNAMES.CLIENTID.ToString()]);
                CompanyName = Convert.ToString(dt.Rows[0][FIELDNAMES.COMPANYNAME.ToString()]);
            }
        }
    }
}
