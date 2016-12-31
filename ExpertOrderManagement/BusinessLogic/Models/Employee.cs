using DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessLogic.Models
{
    [Serializable]
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string PhotoPath { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public enum FIELDNAMES
        {
            CLIENTID = 1,
            CLIENTCREATEDDATE = 2,
            USERNAME = 3,
            PASSWORD = 4
        }

        public Client()
        {

        }

        public Client(string UserName)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UserName", UserName);
            DataTable dt = DBHelper.GetDataTable("GetClient", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.ID = Convert.ToInt32(Convert.ToString(dt.Rows[0][FIELDNAMES.CLIENTID.ToString()]));
                this.CreatedDate = Convert.ToDateTime(dt.Rows[0][FIELDNAMES.CLIENTCREATEDDATE.ToString()]);
                this.UserName = Convert.ToString(dt.Rows[0][FIELDNAMES.USERNAME.ToString()]);
                this.Password = Convert.ToString(dt.Rows[0][FIELDNAMES.PASSWORD.ToString()]);
            }
        }

        public Client(string UserName, string Password)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UserName", UserName);
            param.Add("@Password", StringCipher.Encrypt(Password));
            DataTable dt = DBHelper.GetDataTable("CheckUserByUserNameAndPassword", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.ID = Convert.ToInt32(Convert.ToString(dt.Rows[0][FIELDNAMES.CLIENTID.ToString()]));
                this.CreatedDate = Convert.ToDateTime(dt.Rows[0][FIELDNAMES.CLIENTCREATEDDATE.ToString()]);
                this.UserName = Convert.ToString(dt.Rows[0][FIELDNAMES.USERNAME.ToString()]);
                this.Password = Convert.ToString(dt.Rows[0][FIELDNAMES.PASSWORD.ToString()]);
            }
        }

        
    }
}
