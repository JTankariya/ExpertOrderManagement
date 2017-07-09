using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IProductHelper : IHelper<Product>
    {
        IEnumerable<Product> CheckDuplicateName(string GroupName, string Code);
    }
}
