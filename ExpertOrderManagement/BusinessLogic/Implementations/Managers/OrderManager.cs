using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class OrderManager : IOrderManager
    {
        private Order _context;
        public OrderManager(Order context)
        {
            _context = context;
        }

        public ResponseMsg Save()
        {
            
        }

        public ResponseMsg Delete()
        {
            throw new NotImplementedException();
        }
    }
}
