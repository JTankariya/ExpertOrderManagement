
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

    public class HelperFactory<C1, C2, H> : IHelperFactory<C1, C2, H>
    {
        public H Create(C1 tName, C2 companyId)
        {
            return ExpertOrderBusinessInit.kernel.Get<H>(new[] { 
                            new ConstructorArgument("tName", tName), 
                            new ConstructorArgument("companyId", companyId) 
                        });
        }
    }
}
