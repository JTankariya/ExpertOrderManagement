using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IClientHelper : IHelper<Client>
    {
        IEnumerable<Client> CheckDuplicateName(string ClientName, string RefId);
        IEnumerable<Client> CheckDuplicateUserName(string UserName, string RefId);
        IEnumerable<ClientCompany> GetCompanies(int clientId);
        IEnumerable<ClientCompany> GetBillableCompanies(int clientId);
        IEnumerable<ClientCompany> GetWithoutCompanies(int clientId);
    }
}
