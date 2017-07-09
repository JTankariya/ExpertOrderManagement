using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IHelperFactory<C1, C2, H>
    {
        H Create(C1 tName, C2 companyId);
    }
}
