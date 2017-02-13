using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace BusinessLogic
{
    public class Client
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
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
        public string Email { get; set; }
        public int NoOfDays { get; set; }
        public int NoOfCompanyPerUser { get; set; }
        public int NoOfAccessUsers { get; set; }
        public int TotalCreatedUser { get; set; }
        public int TotalCreatedCompany { get; set; }
        public DateTime AccountExpiredOn { get; set; }
        public bool IsUploadingProcessStart { get; set; }
        public int CreatedAdminID { get; set; }
        public string ClientMastercol { get; set; }
        public bool QueryRights { get; set; }
        public DateTime DataUploadedOn { get; set; }
        public int CreatedDistributorId { get; set; }
        public bool IsWithout { get; set; }
        public Guid RefId { get; set; }

        private IClientManager _manager;

        public IClientManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = ExpertOrderBusinessInit.kernel.Get<IManagerFactory<Client, IClientManager>>().Create(this);
                }
                return _manager;
            }
        }
    }
}
