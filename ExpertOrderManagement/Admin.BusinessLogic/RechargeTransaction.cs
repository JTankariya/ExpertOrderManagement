using Admin.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.BusinessLogic
{
    public class RechargeTransaction
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select Distributor")]
        public int DistributorId { get; set; }
        [Required(ErrorMessage = "Please enter Amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a amount bigger than 0")]
        public string Amount { get; set; }
        [Required(ErrorMessage = "Please select Transaction Type")]
        public string Type { get; set; }
        public string Remarks { get; set; }

        public string ClientName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Int64 TotalCompany { get; set; }
        public Int64 TotalUser { get; set; }
        public int ClientId { get; set; }
        public int Balance { get; set; }

        public int Save()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@Distributor", this.DistributorId);
            param.Add("@Amnt", this.Amount);
            param.Add("@TType", this.Type);
            param.Add("@Remark", this.Remarks);
            param.Add("@CBy", this.CreatedBy);
            param.Add("@CId", this.ClientId);
            int result = DBHelper.ExecuteNonQuery("AddRecharge", param, true);

            return 1;
        }

        public static IEnumerable<RechargeTransaction> GetAllRechargeTransaction(int DistributorId=0) {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@Distributor", DistributorId);
            DataTable dt = DBHelper.GetDataTable("GetAllRechargeTransaction", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<RechargeTransaction>(dt);
            else
                return null;
        }

        public static IEnumerable<RechargeTransaction> RechargeHistory(int DistributorId = 0)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@DID", DistributorId);
            DataTable dt = DBHelper.GetDataTable("RechargeHistory", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<RechargeTransaction>(dt);
            else
                return null;
        }

        public static int SaveDictributorTransaction(int ClientID, int DistributorID, int Users, int Company)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            var totalAmount = (Convert.ToInt32(ConfigurationManager.AppSettings["UserPrice"]) * Users) +(Convert.ToInt32(ConfigurationManager.AppSettings["CompanyPrice"])*Company);

            param.Add("@Distributor", DistributorID);
            param.Add("@Amnt", totalAmount);
            param.Add("@TType", "D");
            param.Add("@CId", ClientID);
            param.Add("@CBy", 0);
            param.Add("@Remark", "");
            int result = DBHelper.ExecuteNonQuery("AddRecharge", param, true);

            if (result > 0)
                return result;
            else
                return 0;

        }
    }
}
