using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class UserSettings
    {
        public int Id { get; set; }
        public int SettingId { get; set; }
        public int UserId { get; set; }
        public string Value { get; set; }

        private Setting _setting;
        public Setting Setting
        {
            get
            {
                if (_setting == null)
                {
                    _setting = Helpers.SettingHelper.GetById(SettingId);
                }
                return _setting;
            }
        }
    }
}
