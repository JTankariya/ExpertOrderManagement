using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace BusinessLogic
{
    public class Setting
    {
        public int Id { get; set; }
        public string KeyName { get; set; }

        private ISettingManager _manager;

        public ISettingManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = ExpertOrderBusinessInit.kernel.Get<IManagerFactory<Setting, ISettingManager>>().Create(this);
                }
                return _manager;
            }
        }
    }
}
