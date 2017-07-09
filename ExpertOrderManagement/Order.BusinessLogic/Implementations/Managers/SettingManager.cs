using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class SettingManager : ISettingManager
    {
        private Setting _context;
        public SettingManager(Setting context)
        {
            _context = context;
        }
    }
}
