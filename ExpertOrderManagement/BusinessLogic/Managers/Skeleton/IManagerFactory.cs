using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IManagerFactory<C, M>
    {
        M Create(C context);
    }

    public interface IHelperFactory<C, H>
    {
        H Create(C tName);
    }
}
