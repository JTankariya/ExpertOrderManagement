using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IUserHelper : IHelper<ClientUser>
    {
        ClientUser GetByEmail(string Email);
    }
}
