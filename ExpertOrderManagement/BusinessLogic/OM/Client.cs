using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class Client : User
    {
        public int ClientId { get; set; }
        public DateTime ClientCreatedDate { get; set; }
        public string MobileNo { get; set; }
    }
}
