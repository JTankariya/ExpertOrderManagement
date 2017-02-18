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
    public class ProductGroupController : BaseController
    {
        //
        // GET: /ProductGroup/

        public ActionResult List()
        {
            return View();
        }
        public ActionResult Add(string Id)
        {
            var groups = ProductGroupHelper.GetProductGroupsForParentDropDown();
            if (!string.IsNullOrEmpty(Id) && Id != "0")
            {
                groups = groups.Where(x => x.RefId.ToString() != Id);
                var group = ProductGroupHelper.GetByRefId(Id);
                ViewBag.ProductGroups = groups;
                return View(group);
            }
            else
            {
                var group = new ProductGroup();
                group.ClientCompanyId = currUser.DefaultCompany.ClientCompanyId;
                ViewBag.ProductGroups = groups;
                return View(group);
            }

        }
        [HttpPost]
        public JsonResult Save(ProductGroup group)
        {
            if (group.ClientCompanyId == 0)
            {
                group.ClientCompanyId = currUser.DefaultCompany.ClientCompanyId;
            }
            return Json(group.Manager.Save(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string CheckDuplicateName(string Name, string Code)
        {
            var groups = ProductGroupHelper.CheckDuplicateName(Name, Code);
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
            return View(ProductGroupHelper.GetAll());
        }

        public JsonResult Delete(string ID)
        {
            return Json(ProductGroupHelper.GetByRefId(ID).Manager.Delete(), JsonRequestBehavior.AllowGet);
        }
    }
}
