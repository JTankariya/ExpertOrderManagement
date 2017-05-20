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
        public DateTime CompanyFromDate { get; set; }
        public DateTime CompanyToDate { get; set; }
        public string CompanyAdd1 { get; set; }
        public string CompanyAdd2 { get; set; }
        public string CompanyAdd3 { get; set; }
        public string CompanyAdd4 { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyMobileNo { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyVAT { get; set; }
        public string CompanyCST { get; set; }
        public string CompanyITNO { get; set; }
        public string CompanyLICNO { get; set; }
        public string CompanyTANNO { get; set; }
        public string CompanyAuthorised { get; set; }
        public string CompanyRemarks { get; set; }
        public string ExpertPath { get; set; }
        public int CompanyCode { get; set; }
        public DateTime DataUploadDateTime { get; set; }
        public bool IsWithout { get; set; }
        public bool IsDefault { get; set; }


        public enum FIELDNAMES
        {
            CLIENTCOMPANYID = 1,
            CLIENTID = 2,
            COMPANYNAME = 3,
            ISWITHOUT = 4,
            ISDEFAULT = 5
        }

        public ClientCompany()
        {

        }

        public ClientCompany(int CompanyId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ClientId", DBNull.Value);
            param.Add("@CompanyId", CompanyId);
            DataTable dt = DBHelper.GetDataTable("Order.GetClientCompany", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                ClientCompanyId = Convert.ToInt32(dt.Rows[0][FIELDNAMES.CLIENTCOMPANYID.ToString()]);
                ClientId = Convert.ToInt32(dt.Rows[0][FIELDNAMES.CLIENTID.ToString()]);
                CompanyName = Convert.ToString(dt.Rows[0][FIELDNAMES.COMPANYNAME.ToString()]);
                IsDefault = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0][FIELDNAMES.ISDEFAULT.ToString()])) ? false : Convert.ToBoolean(dt.Rows[0][FIELDNAMES.ISDEFAULT.ToString()]);
                IsWithout = Convert.ToBoolean(string.IsNullOrEmpty(Convert.ToString(dt.Rows[0][FIELDNAMES.ISWITHOUT.ToString()])) ? false : dt.Rows[0][FIELDNAMES.ISWITHOUT.ToString()]);
            }
        }
    }
}
