using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace OrderAdminPanel.Helpers
{
    public class ValidateOnlyIncomingValuesAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        /// <summary>
        /// This Attribute is used when you want only passed parameter to be validated using ModelState function.
        /// We need to use this on top of Action with Bind attribute in parameter and in it's Include property, you need to specify names of properties using comma
        /// seperated values.
        /// EG. 
        /// [ValidateOnlyIncomingValuesAttribute]
        /// public ActionResult LogIn([Bind(Include="UserName,Password")] User user)
        /// 
        /// Created By : Jayesh Tankariya
        /// Created On : 03/08/2016
        /// </summary>
        /// <param name="filterContext"></param>

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;
            var valueProvider = filterContext.Controller.ValueProvider;

            var keysWithNoIncomingValue = modelState.Keys.Where(x => !valueProvider.ContainsPrefix(x));
            foreach (var key in keysWithNoIncomingValue)
                modelState[key].Errors.Clear();
        }
    }
}