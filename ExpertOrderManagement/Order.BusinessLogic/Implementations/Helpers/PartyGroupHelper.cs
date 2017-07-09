using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class PartyGroupHelper : Helper<PartyGroup>, IPartyGroupHelper
    {
        public PartyGroupHelper(string tName, int companyId)
            : base(tName, companyId)
        {

        }
    }
}
