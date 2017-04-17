using BusinessLogic;
using CommonLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpertOrderManagement.Controllers
{
    [AuthorizeWebForm]
    public class ProductController : BaseController
    {
        public ActionResult Add(string Id)
        {
            ViewBag.GroupList = ProductGroupHelper.GetProductGroupsForParentDropDown();
            if (!string.IsNullOrEmpty(Id) && Id != "0")
            {
                var product = ProductHelper.GetByRefId(Id);
                return View(product);
            }
            else
            {
                var product = new Product();
                product.ClientCompanyId = currUser.DefaultCompany.ClientCompanyId;
                return View(product);
            }

        }
        [HttpPost]
        public JsonResult Save(Product product)
        {
            if (product.ClientCompanyId == 0)
            {
                product.ClientCompanyId = currUser.DefaultCompany.ClientCompanyId;
            }
            return Json(product.Manager.Save(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string CheckDuplicateName(string Name, string Code)
        {
            var groups = ProductHelper.CheckDuplicateName(Name, Code);
            if (groups != null && groups.Count() > 0)
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }

        public ActionResult GetAll()
        {
            return View(ProductHelper.GetAll());
        }

        public JsonResult GetDetail(string Code)
        {
            var product = ProductHelper.GetByCode(Code);
            return Json(new ResponseMsg() { IsSuccess = true, ResponseValue = product }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBatches(string Code)
        {
            var product = ProductHelper.GetByCode(Code);
            var batches = product.Manager.GetBatches();
            ResponseMsg response = new ResponseMsg();
            response.IsSuccess = true;
            response.ResponseValue = batches;
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
