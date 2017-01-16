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
    public class AdminMaster
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        public string SMSUrl { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Mobile No is required")]
        public string MobileNo { get; set; }

        public bool IsSuperAdmin { get; set; }

        public int SaveAdmin()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", this.ID);
            param.Add("@FName", this.FirstName);
            param.Add("@LName", this.LastName);
            param.Add("@Email", this.EmailId);
            param.Add("@Mobile", this.MobileNo);
            param.Add("@UName", this.UserName);
            if (!string.IsNullOrEmpty(Password))
            {
                param.Add("@PWord", StringCipher.Encrypt(this.Password));
            }
            else
            {
                param.Add("@PWord", DBNull.Value);
            }
            int result = DBHelper.ExecuteNonQuery("SaveOrderAdminMaster", param, true);

            return 1;
        }

        public static IEnumerable<AdminMaster> GetAllAdminMaster()
        {
            DataTable dt = DBHelper.GetDataTable("GetAllOrderAdminMaster", null, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<AdminMaster>(dt);
            else
                return null;
        }

        public static AdminMaster GetAdminByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetOrderAdminByID", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                var user = DBHelper.ConvertToEnumerable<AdminMaster>(dt).FirstOrDefault();
                user.Password = StringCipher.Decrypt(user.Password);
                return user;
            }
            else
                return null;
        }

        public static int Delete(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            return DBHelper.ExecuteNonQuery("DeleteOrderAdminByID", param, true);
        }
    }
}
