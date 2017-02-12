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
    }
}
