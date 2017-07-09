using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Order.API.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public ResponseMsg Test()
        {
            ResponseMsg response = new ResponseMsg();
            return response;
        }
    }
}
