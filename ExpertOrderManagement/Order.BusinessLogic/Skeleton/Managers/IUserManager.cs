using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IUserManager : IGenericCRUD
    {
        IEnumerable<UserSettings> GetSettings();

        ResponseMsg SaveSetting(int settingId, string value);
    }
}
