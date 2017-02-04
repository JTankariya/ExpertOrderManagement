using DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Ninject;

namespace BusinessLogic
{
    [Serializable]
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string PhotoPath { get; set; }
        public string UserType { get; set; }

        public enum FIELDNAMES
        {
            ID = 1,
            USERNAME = 3,
            PASSWORD = 4,
            USERTYPE=5
        }

        public User()
        {

        }

        public User(string UserName)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UserName", UserName);
            DataTable dt = DBHelper.GetDataTable("Order.GetUser", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.ID = Convert.ToInt32(Convert.ToString(dt.Rows[0][FIELDNAMES.ID.ToString()]));
                this.UserName = Convert.ToString(dt.Rows[0][FIELDNAMES.USERNAME.ToString()]);
                this.Password = Convert.ToString(dt.Rows[0][FIELDNAMES.PASSWORD.ToString()]);
                UserType = Convert.ToString(dt.Rows[0][FIELDNAMES.USERTYPE.ToString()]);
            }
        }

        public User(string UserName, string Password)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UserName", UserName);
            param.Add("@Password", StringCipher.Encrypt(Password));
            DataTable dt = DBHelper.GetDataTable("Order.GetUser", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.ID = Convert.ToInt32(Convert.ToString(dt.Rows[0][FIELDNAMES.ID.ToString()]));
                this.UserName = Convert.ToString(dt.Rows[0][FIELDNAMES.USERNAME.ToString()]);
                this.Password = Convert.ToString(dt.Rows[0][FIELDNAMES.PASSWORD.ToString()]);
                UserType = Convert.ToString(dt.Rows[0][FIELDNAMES.USERTYPE.ToString()]);
            }
        }

        public User(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@Id", ID);
            DataTable dt = DBHelper.GetDataTable("Order.GetUser", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.ID = Convert.ToInt32(Convert.ToString(dt.Rows[0][FIELDNAMES.ID.ToString()]));
                this.UserName = Convert.ToString(dt.Rows[0][FIELDNAMES.USERNAME.ToString()]);
                this.Password = Convert.ToString(dt.Rows[0][FIELDNAMES.PASSWORD.ToString()]);
                UserType = Convert.ToString(dt.Rows[0][FIELDNAMES.USERTYPE.ToString()]);
            }
        }

        private IUserManager _manager;

        public IUserManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = ExpertOrderBusinessInit.kernel.Get<IManagerFactory<User, IUserManager>>().Create(this);
                }
                return _manager;
            }
        }
    }
}
