using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IProductGroupHelper : IHelper<ProductGroup>
    {
        IEnumerable<ProductGroup> GetProductGroupsForParentDropDown();
    }
}
