using BusinessLogic.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Parameters;

namespace BusinessLogic
{
    public class ManagerFactory<C, M> : IManagerFactory<C, M>
    {
        public M Create(C context)
        {
            return ExpertOrderBusinessInit.kernel.Get<M>(new ConstructorArgument("context", context));
        }
    }

    public class HelperFactory<C, H> : IManagerFactory<C, H>
    {
        public H Create(C tName)
        {
            return ExpertOrderBusinessInit.kernel.Get<H>(new ConstructorArgument("tName", tName));
        }
    }
}
