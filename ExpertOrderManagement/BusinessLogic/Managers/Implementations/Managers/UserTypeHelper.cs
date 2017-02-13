using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class UserTypeHelper : Helper<UserType>, IUserTypeHelper
    {
        public UserTypeHelper(string tName, int copmanyId)
            : base(tName, copmanyId)
        { 
        
        }
    }
}
