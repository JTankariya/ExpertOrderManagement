using CommonLibraries;
using Distributor.BusinessLogic;
using Distributor.DAL;
using NewExpert.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewExpert.Controllers
{
    [AuthorizeWebForm]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            return View();
        }

        #region CHANGE PASSWORD

        public ActionResult ChangePassword()
        {
            var CurrentUser = (Distributor.BusinessLogic.User)Session["User"];
            //var user = new User();
            //if (CurrentUser != null)
            //{
            //    user.UserName = CurrentUser.UserName;
            //    user.UserType = CurrentUser.UserType;
            //}
            return View(CurrentUser);
        }


        [HttpPost]
        public ActionResult ChangePassword(Distributor.BusinessLogic.User model)
        {
            var CurrentUser = (Distributor.BusinessLogic.User)Session["User"];
            ModelState.Clear();
            if (string.IsNullOrEmpty(model.Password))
            {
                return RedirectToAction("ChangePassword");
            }
            if (CurrentUser != null && StringCipher.Decrypt(CurrentUser.Password) == model.OldPassword)
            {
                model.Id = CurrentUser.Id;
                if (model.ChangePassword() > 0)
                {
                    ModelState.AddModelError("", "Password has been changed successfully.");
                    model.OldPassword = "";
                    ((Distributor.BusinessLogic.User)Session["User"]).Password = StringCipher.Encrypt(model.Password);
                }
            }
            else
            {
                ModelState.AddModelError("", "Your Oldpassword is not matched in our database, Please enter correct old password.");
            }
            model.UserName = CurrentUser.UserName;
            return View(model);
        }

        #endregion

        #region ADMIN MASTER CRUD OPERATIONS

        public ActionResult AdminMaster(string ID)
        {
            if (!string.IsNullOrEmpty(ID) && Convert.ToInt32(ID) > 0)
            {
                var admin = Distributor.BusinessLogic.AdminMaster.GetAdminByID(ID);
                return View(admin);
            }
            else
            {
                return View(new AdminMaster());
            }

        }

        [ValidateOnlyIncomingValues]
        [HttpPost]
        public ActionResult AdminMaster(Distributor.BusinessLogic.AdminMaster model)
        {
            if (ModelState.IsValid)
            {
                if (model.SaveAdmin() > 0)
                {
                    ModelState.AddModelError("", "Admin is saved successfully.");
                    return View(new AdminMaster());
                }
                else
                {
                    ModelState.AddModelError("", "Some problem occured while saving record, Please try after sometime.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult DeleteAdminMaster(int ID)
        {
            if (ID > 0)
            {
                if (Distributor.BusinessLogic.AdminMaster.Delete(ID) > 0)
                {
                    ModelState.AddModelError("", "Admin Deleted Successfully.");
                }
                else
                {
                    ModelState.AddModelError("", "Error while deleting Admin, Please try after sometime.");
                }

            }

            return RedirectToAction("AdminMaster");
        }

        public ActionResult GetAdminList()
        {
            var currUser = (Distributor.BusinessLogic.User)Session["User"];
            var admins = Distributor.BusinessLogic.AdminMaster.GetAllAdminMaster().Where(x => x.ID != currUser.Id);
            return View(admins);
        }

        #endregion

        #region UPDATE PROFILE

        public ActionResult UpdateProfile()
        {
            var currUser = (Distributor.BusinessLogic.User)Session["User"];
            return View(currUser);
        }

        [ValidateOnlyIncomingValuesAttribute]
        [HttpPost]
        public ActionResult UpdateProfile(User model)
        {
            var currUser = (Distributor.BusinessLogic.User)Session["User"];
            if (ModelState.IsValid)
            {
                model.UserType = currUser.UserType;
                if (model.Save() > 0)
                {
                    Session["User"] = model;
                    ModelState.AddModelError("", "Profile Updated successfully.");
                    return View(currUser);
                }
                else
                {
                    ModelState.AddModelError("", "Error while updating profile, Please try after sometime.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        #endregion

        #region DITRIBUTOR CRUD OPERATIONS

        public ActionResult GetDistributorList()
        {
            var currUser = (Distributor.BusinessLogic.User)Session["User"];
            var distributors = Distributor.BusinessLogic.User.GetAllDistributors();
            return View(distributors);
        }

        public ActionResult DeleteDistributorMaster(int ID)
        {
            if (ID > 0)
            {
                if (Distributor.BusinessLogic.User.Delete(ID, "DISTRIBUTOR") > 0)
                {
                    ModelState.AddModelError("", "Distributor Deleted Successfully.");
                }
                else
                {
                    ModelState.AddModelError("", "Error while deleting Admin, Please try after sometime.");
                }

            }

            return RedirectToAction("AddDistributor");
        }

        [ValidateOnlyIncomingValues]
        [HttpPost]
        public ActionResult AddDistributor(User model)
        {
            if (ModelState.IsValid)
            {
                model.AdminID = ((Distributor.BusinessLogic.User)Session["User"]).Id;
                if (model.Save() > 0)
                {
                    ModelState.AddModelError("", "Distributor is saved successfully.");
                    return View(new User());
                }
                else
                {
                    ModelState.AddModelError("", "Some problem occured while saving record, Please try after sometime.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult AddDistributor(string ID)
        {
            if (!string.IsNullOrEmpty(ID) && Convert.ToInt32(ID) > 0)
            {
                var user = Distributor.BusinessLogic.User.GetDistributorByID(ID);
                return View(user);
            }
            else
            {
                return View(new User());
            }
        }
        #endregion

        #region DITRIBUTOR RECHARGE
        public ActionResult DistributorRecharge()
        {
            var recharge = new RechargeTransaction();
            var allUsers = Distributor.BusinessLogic.User.GetAllDistributors();
            if (allUsers != null)
            {
                ViewBag.DistributorList = allUsers.Select(x => new SelectListItem() { Text = x.FirstName + " " + x.LastName, Value = Convert.ToString(x.Id) }).ToList();
            }
            else
            {
                ViewBag.DistributorList = new List<SelectListItem>();
            }
            return View(recharge);
        }

        [ValidateOnlyIncomingValues]
        [HttpPost]
        public ActionResult DistributorRecharge(RechargeTransaction model)
        {
            if (ModelState.IsValid)
            {
                var currUser = (Distributor.BusinessLogic.User)Session["User"];
                model.CreatedBy = currUser.Id;
                model.ClientId = 0;
                ViewBag.DistributorList = Distributor.BusinessLogic.User.GetAllDistributors().Select(x => new SelectListItem() { Text = x.FirstName + " " + x.LastName, Value = Convert.ToString(x.Id) }).ToList();
                if (model.Save() > 0)
                {
                    ModelState.AddModelError("", "Recharge Transaction has been recorded successfully.");
                    return View(new RechargeTransaction());
                }
                else
                {
                    ModelState.AddModelError("", "Some problem occured while adding record to database, Please try after sometime.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        #endregion

        #region CLIENT MASTER WITH MENU ALLOCATION

        public ActionResult AddClient(int ID)
        {
            if (ID > 0)
            {
                ViewBag.ClientMenuList = ClientMenuMaster.GetClientMenuByClientId(ID);
                var client = ClientMaster.GetAllClients().FirstOrDefault(x => x.ClientID == ID);
                client.Password = StringCipher.Decrypt(client.Password);
                return View(client);
            }
            else
            {
                ViewBag.ClientMenuList = ClientMenuMaster.GetClientMenuByClientId(0);
                var client = new ClientMaster();
                client.AccountExpiredOn = DateTime.Now.Add(new TimeSpan(365, 0, 0, 0, 0));
                return View(client);
            }
        }

        public ActionResult GetClientList()
        {
            var currUser = (Distributor.BusinessLogic.User)Session["User"];
            if (currUser.UserType == "ADMIN")
            {
                return View(ClientMaster.GetAllClients());
            }
            else
            {
                return View(ClientMaster.GetAllClients().Where(x => x.CreatedDistributorId == currUser.Id));
            }
        }

        [HttpPost]
        public JsonResult AddClient(ClientMaster clientData, ClientMenuMaster[] clientMenus)
        {
            JsonResponse response = new JsonResponse();
            if (ModelState.IsValid)
            {
                var currUser = (Distributor.BusinessLogic.User)Session["User"];
                if (currUser.UserType == "ADMIN")
                {
                    clientData.CreatedAdminID = currUser.Id;
                }
                else
                {
                    clientData.CreatedDistributorId = currUser.Id;
                }
                clientData.ClientCreatedDate = DateTime.Now;
                var clientId = clientData.SaveClient();
                if (clientId > 0)
                {
                    RechargeTransaction.SaveDictributorTransaction(clientId, clientData.CreatedDistributorId, clientData.NoOfAccessUsers, clientData.NoOfCompanyPerUser);
                    if (clientMenus != null && clientMenus.Length > 0)
                    {
                        ClientMenuMaster.SaveClientMenus(clientMenus, clientData.ClientID == 0 ? clientId : clientData.ClientID);
                    }
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                }
                return Json(response);
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseMsg = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { Error = x.Value.Errors[0].ErrorMessage }).ToArray();
                return Json(response);
            }

        }

        public ActionResult DeleteClient(int ID)
        {
            if (ClientMaster.DeleteClient(ID) == 1)
            {
                return RedirectToAction("AddClient", "Dashboard", new { ID = 0 });
            }
            else
            {
                return RedirectToAction("AddClient", "Dashboard", new { ID = ID });
            }
        }

        #endregion

        #region MENU MASTER
        public ActionResult GetMenuMaster(int ID)
        {
            var menus = ClientMenuMaster.GetAllMenus();
            if (ID > 0)
            {
                var menu = menus.FirstOrDefault(x => x.MenuId == ID);
                ViewBag.MenuList = menus.Where(x => x.MenuId != ID);
                if (menu.HasChildMenu)
                {
                    ViewBag.ChildMenuList = ChildMenuMaster.GetChildMenusByMenuId(menu.MenuId);
                }
                if (menu.IsDashboard)
                {
                    ViewBag.DashboardList = DashboardMaster.GetDashboardByMenuId(menu.MenuId);
                }
                return View(menu);
            }
            else
            {
                ViewBag.MenuList = menus;
                var menu = new ClientMenuMaster();
                if (menus != null && menus.Count() > 0)
                {
                    menu.SrNo = menus.OrderByDescending(x => x.SrNo).FirstOrDefault().SrNo + 1;
                }
                else
                {
                    menu.SrNo = 1;
                }
                return View(menu);
            }
        }

        [HttpPost]
        public ActionResult SaveMenu(ClientMenuMaster menuData, ChildMenuMaster[] subMenus, DashboardMaster[] dashboardMenus)
        {
            JsonResponse response = new JsonResponse();
            if (menuData.IsDashboard && CheckDashboardExist(menuData.MenuId))
            {
                response.IsSuccess = false;
                response.ResponseMsg = "Dashboard Menu Already Exist. Only One Dashboard Can be Entered.";
            }
            else
            {
                int result = menuData.Save();

                if (result > 0)
                {
                    if (subMenus != null && menuData.HasChildMenu)
                    {
                        var alreadyId = subMenus.ToList().Where(x => x.ChildMenuID > 0).Select(x => x.ChildMenuID);
                        var dbMenus = ChildMenuMaster.GetChildMenusByMenuId(result);
                        if (dbMenus != null)
                        {
                            var tobeDeleted = dbMenus.Select(x => x.ChildMenuID).Except(alreadyId);
                            foreach (var toDelete in tobeDeleted)
                            {
                                ChildMenuMaster.DeleteChildMenuById(toDelete);
                            }
                        }
                        subMenus.ToList().ForEach(x => x.MenuID = result);
                        ChildMenuMaster.SaveChildMenus(subMenus);
                    }
                    else
                    {
                        var dbMenus = ChildMenuMaster.GetChildMenusByMenuId(result);
                        if (dbMenus != null)
                        {
                            var tobeDeleted = dbMenus.Select(x => x.ChildMenuID);
                            foreach (var toDelete in tobeDeleted)
                            {
                                ChildMenuMaster.DeleteChildMenuById(toDelete);
                            }
                        }
                    }

                    if (dashboardMenus != null && menuData.IsDashboard)
                    {
                        var alreadyId = dashboardMenus.ToList().Where(x => x.DashboardID > 0).Select(x => x.DashboardID);
                        var dbMenus = DashboardMaster.GetDashboardByMenuId(result);
                        if (dbMenus != null)
                        {
                            var tobeDeleted = dbMenus.Select(x => x.DashboardID).Except(alreadyId);
                            foreach (var toDelete in tobeDeleted)
                            {
                                DashboardMaster.DeleteDashboardById(toDelete);
                            }
                        }

                        dashboardMenus.ToList().ForEach(x => x.MenuID = result);
                        DashboardMaster.SaveDashboard(dashboardMenus);
                    }
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseMsg = "Error while adding menu, Please try after sometime.";
                }
            }
            return Json(response);
        }

        public bool CheckDashboardExist(int MenuID)
        {
            return ClientMenuMaster.CheckDashboardExist(MenuID);
        }

        public ActionResult GetMenuList()
        {
            var menus = ClientMenuMaster.GetAllMenus();
            return View(menus);
        }

        public bool CheckMenuReference(int ID)
        {
            return ClientMenuMaster.CheckMenuReference(ID);
        }

        public ActionResult DeleteMenuMaster(int ID)
        {
            if (ClientMenuMaster.DeleteMenuByID(ID) > 0)
            {
                return RedirectToAction("GetMenuMaster", new { Id = 0 });
            }
            else
            {
                return RedirectToAction("GetMenuMaster", new { Id = ID });
            }
        }

        public JsonResult CheckMenuSrNo(int SrNo, int MenuId)
        {
            var IsFound = false;
            IsFound = ClientMenuMaster.CheckMenuSrNo(SrNo, MenuId);
            return Json(new { valid = !IsFound });
        }


        #endregion

        #region SUB MENU MASTER
        public ActionResult GetSubMenuMaster()
        {
            var CurrentUser = (Distributor.BusinessLogic.User)Session["User"];
            ViewBag.ChildMenus = ChildMenuMaster.GetChildMenusByMenuId(0);
            ViewBag.UserMenus = SubMenuViewModel.GetClientMenuMaster(CurrentUser.Id);
            ViewBag.SubMenus = SubMenuViewModel.GetSubMenuDetail(CurrentUser.Id);
            return View();
        }

        [HttpPost]
        public ActionResult SaveSubMenuForClient(int DetailClientMenuId, int DetailChildMenuId)
        {
            SubMenuViewModel.SaveSubMenuForClient(DetailChildMenuId, DetailClientMenuId);
            return Json("SUCCESS");
        }

        [HttpPost]
        public ActionResult DeleteSubMenuOfClient(int DetailClientMenuId, int DetailChildMenuId)
        {
            var CurrentUser = (Distributor.BusinessLogic.User)Session["User"];
            SubMenuViewModel.DeleteSubMenuForClient(DetailChildMenuId, DetailClientMenuId, CurrentUser.Id);
            return Json("SUCCESS");
        }
        #endregion

        #region DASHBOARD MASTER
        public ActionResult GetDashboard()
        {
            var CurrentUser = (Distributor.BusinessLogic.User)Session["User"];
            ViewBag.UserMenus = SubMenuViewModel.GetClientMenuMaster(CurrentUser.Id);
            ViewBag.DashboardMenus = DashboardMaster.GetDashboardMenuMaster();
            ViewBag.DashMenus = DashboardMaster.GetDashboardMenus(CurrentUser.Id);
            return View();
        }

        [HttpPost]
        public ActionResult SaveDashboardMenuForClient(int DetailClientMenuId, int DetailDashboardId)
        {
            DashboardMaster.SaveDashboardUserMenu(DetailClientMenuId, DetailDashboardId);
            return Json("SUCCESS");
        }

        [HttpPost]
        public ActionResult DeleteDashboardMenuOfClient(int DetailClientMenuId, int DetailDashboardId)
        {
            var CurrentUser = (Distributor.BusinessLogic.User)Session["User"];
            DashboardMaster.DeleteDashboardMenuOfClient(DetailClientMenuId, DetailDashboardId, CurrentUser.Id);
            return Json("SUCCESS");
        }
        #endregion

        #region REPORTS

        public ActionResult DistributorBalanceReport()
        {
            var CurrentUser = (Distributor.BusinessLogic.User)Session["User"];
            var filename = Distributor.BusinessLogic.User.GenerateDistributorBalanceReport(CurrentUser);
            return Json(new { IsSuccess = true, ResponseValue = filename }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DistributorRechargeInformationReport(DateTime? FromDate, DateTime? ToDate)
        {
            if (!FromDate.HasValue && !ToDate.HasValue)
            {
                var users = Distributor.BusinessLogic.User.DistributorRechargeInformationReport(FromDate, ToDate);
                return View(users);
            }
            else
            {
                var users = Distributor.BusinessLogic.User.DistributorRechargeInformationReport(FromDate, ToDate);
                return PartialView("DistributorRechargeRow", users);
            }
        }

        public ActionResult ClientDataUploadReport()
        {
            var users = Distributor.BusinessLogic.User.DistributorRechargeInformationReport(null, null);
            return View(users);
        }

        public ActionResult RechargeHistory()
        {
            var CurrentUser = (Distributor.BusinessLogic.User)Session["User"];
            var history = RechargeTransaction.RechargeHistory(CurrentUser.Id);
            if (history != null)
            {
                history = history.OrderBy(x => x.CreatedDate);
            }
            return View(history);
        }

        #endregion

        #region REMOVE CLIENT

        public ActionResult RemoveClient()
        {
            return View(ClientMaster.GetAllClients());
        }

        public ActionResult DeleteEntireClient(int ClientId)
        {
            ClientMaster.DeleteEntireClient(ClientId);
            return RedirectToAction("RemoveClient");
        }

        #endregion

    }
}
