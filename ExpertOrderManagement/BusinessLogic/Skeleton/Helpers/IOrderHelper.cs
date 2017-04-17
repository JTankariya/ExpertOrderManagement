using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IOrderHelper : IHelper<Order>
    {
        List<Order> CheckDuplicateOrderNo(string Ord_No);
        List<Rate2> GetRateByPartyProduct(int partyId, int productId);

        string GetMaxOrderNo(int clientCompanyId);
    }
}
