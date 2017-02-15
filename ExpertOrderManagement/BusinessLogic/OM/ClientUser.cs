﻿using DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Ninject;

namespace BusinessLogic
{
    [Serializable]
    public class ClientUser
    {
        private UserType _type;
        private Client _client;
        public int Id { get; set; }
        public int Clientid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime AccountExpiredOn { get; set; }
        public string DeviceId { get; set; }
        public string AuthorisedCode { get; set; }
        public bool IsVerified { get; set; }
        public int UserTypeId { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }

        public Client Client
        {
            get
            {
                if (_client == null)
                {
                    _client = Helpers.ClientHelper.GetById(Clientid);
                }
                return _client;
            }
        }
        public UserType UserType
        {
            get
            {
                if (_type == null)
                {
                    _type = Helpers.UserTypeHelper.GetById(UserTypeId);
                }
                return _type;
            }
        }

        public enum FIELDNAMES
        {
            ID = 1,
            USERNAME = 3,
            PASSWORD = 4,
            USERTYPEID = 5,
            CLIENTID = 6
        }

        public ClientUser()
        {

        }

        public ClientUser(string UserName)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UserName", UserName);
            DataTable dt = DBHelper.GetDataTable("Order.GetUser", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0][FIELDNAMES.ID.ToString()]));
                this.UserName = Convert.ToString(dt.Rows[0][FIELDNAMES.USERNAME.ToString()]);
                this.Password = Convert.ToString(dt.Rows[0][FIELDNAMES.PASSWORD.ToString()]);
                this.UserTypeId = Convert.ToInt32(dt.Rows[0][FIELDNAMES.USERTYPEID.ToString()]);
                this.Clientid = Convert.ToInt32(dt.Rows[0][FIELDNAMES.CLIENTID.ToString()]);
            }
        }

        public ClientUser(string UserName, string Password)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UserName", UserName);
            param.Add("@Password", StringCipher.Encrypt(Password));
            DataTable dt = DBHelper.GetDataTable("Order.GetUser", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0][FIELDNAMES.ID.ToString()]));
                this.UserName = Convert.ToString(dt.Rows[0][FIELDNAMES.USERNAME.ToString()]);
                this.Password = Convert.ToString(dt.Rows[0][FIELDNAMES.PASSWORD.ToString()]);
                this.UserTypeId = Convert.ToInt32(dt.Rows[0][FIELDNAMES.USERTYPEID.ToString()]);
                this.Clientid = Convert.ToInt32(dt.Rows[0][FIELDNAMES.CLIENTID.ToString()]);
            }
        }

        public ClientUser(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@Id", ID);
            DataTable dt = DBHelper.GetDataTable("Order.GetUser", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.Id = Convert.ToInt32(Convert.ToString(dt.Rows[0][FIELDNAMES.ID.ToString()]));
                this.UserName = Convert.ToString(dt.Rows[0][FIELDNAMES.USERNAME.ToString()]);
                this.Password = Convert.ToString(dt.Rows[0][FIELDNAMES.PASSWORD.ToString()]);
                this.UserTypeId = Convert.ToInt32(dt.Rows[0][FIELDNAMES.USERTYPEID.ToString()]);
                this.Clientid = Convert.ToInt32(dt.Rows[0][FIELDNAMES.CLIENTID.ToString()]);
            }
        }

        private IUserManager _manager;

        public IUserManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = ExpertOrderBusinessInit.kernel.Get<IManagerFactory<ClientUser, IUserManager>>().Create(this);
                }
                return _manager;
            }
        }
    }
}
