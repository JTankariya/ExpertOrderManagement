using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class OrderHelper : Helper<Order>, IOrderHelper
    {
        public OrderHelper(string tName, int companyId)
            : base(tName, companyId)
        {

        }
    }
}
