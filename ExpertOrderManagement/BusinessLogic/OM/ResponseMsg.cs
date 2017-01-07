using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class ResponseMsg
    {
        public bool IsSuccess { get; set; }
        public object ResponseValue { get; set; }
    }
}