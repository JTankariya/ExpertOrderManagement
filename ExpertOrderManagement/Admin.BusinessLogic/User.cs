using Admin.DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace Admin.BusinessLogic
{
    public class User
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Id is required")]
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string UserType { get; set; }
        public string CompanyName { get; set; }
        public int NoOfCompanyPerUser { get; set; }
        public int NoOfAccessUsers { get; set; }
        public string Amount { get; set; }
        public string ClientName { get; set; }

        public DateTime? DataUploadedOn { get; set; }
        public int AdminID { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Balance { get; set; }
        public decimal TotalCompany { get; set; }
        public decimal TotalUser { get; set; }

        public User CheckUserByUserNameAndPassword()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UName", this.UserName);
            param.Add("@Pword", StringCipher.Encrypt(this.Password));
            DataTable dt = DBHelper.GetDataTable("CheckUserByUserNameAndPassword", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<User>(dt).FirstOrDefault();
            else
                return null;
        }
        public IEnumerable<User> CheckUserByUserName()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UName", this.UserName);
            DataTable dt = DBHelper.GetDataTable("CheckUserName", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<User>(dt);
            else
                return null;
        }

        public IEnumerable<User> CheckUserByEmailId()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@Email", this.EmailId);
            DataTable dt = DBHelper.GetDataTable("CheckEmailId", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<User>(dt);
            else
                return null;
        }

        public int CreateAccount()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@FirstName", this.FirstName);
            param.Add("@LastName", this.LastName);
            param.Add("@EmailId", this.EmailId);
            param.Add("@UserName", this.UserName);
            param.Add("@Password", this.Password);
            DataTable dt = DBHelper.GetDataTable("AddUser", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0][0]);
            else
                return 0;
        }

        public static User GetDistributorByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetDistributorByID", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                var user = DBHelper.ConvertToEnumerable<User>(dt).FirstOrDefault();
                user.Password = StringCipher.Decrypt(user.Password);
                return user;
            }
            else
                return null;
        }

        public int ChangePassword()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", this.Id);
            param.Add("@Pword", StringCipher.Encrypt(this.Password));
            param.Add("@UType", this.UserType);
            int result = DBHelper.ExecuteNonQuery("ChangePassword", param, true);

            if (result > 0)
                return result;
            else
                return 0;

        }

        public int Save()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", this.Id);
            param.Add("@Pword", (!string.IsNullOrEmpty(Password) ? StringCipher.Encrypt(this.Password) : ""));
            param.Add("@UName", this.UserName);
            param.Add("@FName", this.FirstName);
            param.Add("@LName", this.LastName);
            param.Add("@Mobile", this.MobileNo);
            param.Add("@Email", this.EmailId);
            param.Add("@AId", this.AdminID);
            param.Add("@UType", this.UserType);
            int result = DBHelper.ExecuteNonQuery("SaveUser", param, true);

            if (result > 0)
                return result;
            else
                return 0;
        }

        public static string GetDistributorBalance(int DistributorId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@DId", DistributorId);
            DataTable dt = DBHelper.GetDataTable("GetDistributorBalance", param, true, 2);

            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToString(dt.Rows[0][0]);
            else
                return "";
        }

        public static int Delete(int ID, string UserType)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            param.Add("@UType", UserType);
            return DBHelper.ExecuteNonQuery("DeleteUserByID", param, true);
        }

        public static IEnumerable<User> GetAllDistributors()
        {
            DataTable dt = DBHelper.GetDataTable("GetAllDistributors", null, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<User>(dt);
            else
                return null;
        }

        

        public static string GenerateDistributorBalanceReport(User user)
        {
            DataTable dt = DBHelper.GetDataTable("GetDistributorBalanceForReport", null, true);

            if (dt != null && dt.Rows.Count > 0)
                return ReportManager.GetDistributorBalanceReport(user, DBHelper.ConvertToEnumerable<User>(dt));
            else
                return null;
            
        }

        public static IEnumerable<User> DistributorRechargeInformationReport(DateTime? fromDate,DateTime? toDate)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            if (fromDate.HasValue)
            {
                param.Add("@FromDate", fromDate.Value);
            }
            else
            {
                param.Add("@FromDate", DBNull.Value);
            }
            if (toDate.HasValue)
            {
                param.Add("@ToDate", toDate);
            }
            else
            {
                param.Add("@ToDate", DBNull.Value);
            }
            DataTable dt = DBHelper.GetDataTable("DistributorRechargeInformationReport", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<User>(dt);
            else
                return null;
        }
    }
}