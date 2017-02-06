using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distributor.BusinessLogic
{
    [Serializable]
    public class JsonResponse
    {
        public bool IsSuccess { get; set; }
        public object ResponseMsg { get; set; }
    }
}
