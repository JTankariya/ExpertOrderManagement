using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class SettingHelper : Helper<Setting>, ISettingHelper
    {
        public SettingHelper(string tName, int companyId)
            : base(tName, companyId)
        {

        }
    }
}
