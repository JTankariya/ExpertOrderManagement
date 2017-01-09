
using Admin.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.BusinessLogic
{
    public class ClientMaster
    {
        public int ClientID { get; set; }
        public DateTime ClientCreatedDate { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string EmailId { get; set; }
        public int NoOfDays { get; set; }
        public int NoOfCompanyPerUser { get; set; }
        public int NoOfAccessUsers { get; set; }
        public int TotalCreatedUser { get; set; }
        public int TotalCreatedCompany { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AccountExpiredOn { get; set; }
        public int IsUploadingProcessStart { get; set; }
        public int CreatedAdminID { get; set; }
        public int CreatedDistributorId { get; set; }
        public string ClientMastercol { get; set; }
        public int QueryRights { get; set; }
        public DateTime? DataUploadedOn { get; set; }

        public int SaveClient()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", this.ClientID);
            param.Add("@CreateDate", this.ClientCreatedDate.ToString("yyyy-MM-dd"));
            param.Add("@UName", this.UserName);
            param.Add("@PWord", StringCipher.Encrypt(this.Password));
            param.Add("@Mobile", this.MobileNo);
            param.Add("@FName", this.FirstName);
            param.Add("@LName", this.LastName);
            param.Add("@CName", this.CompanyName);
            param.Add("@CAddress", this.CompanyAddress);
            param.Add("@Email", this.EmailId);
            param.Add("@NoDays", this.NoOfDays);
            param.Add("@NoAccessUsers", this.NoOfAccessUsers);
            param.Add("@NoCompanyUser", this.NoOfCompanyPerUser);
            param.Add("@ExpiryDate", this.AccountExpiredOn.Value.ToString("yyyy-MM-dd"));
            param.Add("@UploadingProcess", this.IsUploadingProcessStart);
            param.Add("@AdminID", this.CreatedAdminID);
            param.Add("@DistributorID", this.CreatedDistributorId);
            param.Add("@QueryRight", this.QueryRights);

            DataTable dt = DBHelper.GetDataTable("SaveClient", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0][0]);
            else
                return 0;
        }

        public static IEnumerable<ClientMaster> GetAllClients()
        {
            DataTable dt = DBHelper.GetDataTable("GetAllClientMaster", null, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<ClientMaster>(dt);
            else
                return null;
        }

        public static int DeleteClient(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@CID", ID);
            int result = DBHelper.ExecuteNonQuery("DeleteClient", param, true);
            return 1;
        }

        public static void DeleteEntireClient(int ClientId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@CID", ClientId);
            int result = DBHelper.ExecuteNonQuery("DeleteEntireClient", param, true);
        }
    }


}
