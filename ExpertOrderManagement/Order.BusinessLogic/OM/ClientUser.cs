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
        public string PartyCode { get; set; }

        public bool IsToRefresh
        {
            get;
            set;
        }

        private IEnumerable<UserSettings> _settings;

        public IEnumerable<UserSettings> Settings
        {
            get
            {
                if (_settings == null || IsToRefresh)
                    _settings = Manager.GetSettings();
                return _settings;
            }
        }
        private List<ClientCompany> _companies;

        public List<ClientCompany> Companies
        {
            get
            {
                if (_companies == null)
                {
                    _companies = Helpers.ClientHelper.GetCompanies(Clientid).ToList();
                }
                return _companies;
            }
        }

        private List<ClientCompany> _billableCompanies;

        public List<ClientCompany> BillableCompanies
        {
            get
            {
                if (_billableCompanies == null)
                {
                    _billableCompanies = Helpers.ClientHelper.GetBillableCompanies(Clientid).ToList();
                }
                return _billableCompanies;
            }
        }
        private List<ClientCompany> _withoutCompanies;

        public List<ClientCompany> WithoutCompanies
        {
            get
            {
                if (_withoutCompanies == null)
                {
                    _withoutCompanies = Helpers.ClientHelper.GetWithoutCompanies(Clientid).ToList();
                }
                return _withoutCompanies;
            }
        }

        private ClientCompany _defaultCompany;

        public ClientCompany DefaultCompany
        {
            get
            {
                if (_defaultCompany == null || IsToRefresh)
                {
                    var companySetting = Settings.FirstOrDefault(x => x.SettingId == 1);
                    if (companySetting != null)
                    {
                        _defaultCompany = new ClientCompany(Convert.ToInt32(companySetting.Value));
                    }
                    IsToRefresh = false;
                }
                return _defaultCompany;
            }
        }

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
                if (_type == null && UserTypeId > 0)
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
            CLIENTID = 6,
            PARTYCODE = 7
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
                this.PartyCode = dt.Rows[0][FIELDNAMES.PARTYCODE.ToString()].ToString();
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
                this.PartyCode = dt.Rows[0][FIELDNAMES.PARTYCODE.ToString()].ToString();
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
                this.PartyCode = dt.Rows[0][FIELDNAMES.PARTYCODE.ToString()].ToString();
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
