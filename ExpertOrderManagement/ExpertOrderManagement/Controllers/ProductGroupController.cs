using BusinessLogic;
using ExpertOrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpertOrderManagement.Controllers
{
    [AuthorizeWebForm]
    public class ProductGroupController : BaseController
    {
        //
        // GET: /ProductGroup/

        public ActionResult List()
        {
            return View();
        }
        public ActionResult Add(int Id)
        {
            ViewBag.ProductGroups = ProductGroupHelper.GetProductGroupsForParentDropDown();
            if (Id > 0)
            {                
                var group = ProductGroupHelper.GetById(Id);
                return View(group);
            }
            else
            {
                var group = new ProductGroup();
                return View(group);
            }
            
        }
    }
}
