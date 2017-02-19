using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IPartyHelper : IHelper<Party>
    {

        List<Party> CheckDuplicateName(string Name, string Code);

        List<Party> GetSundryDebtors();
    }
}
