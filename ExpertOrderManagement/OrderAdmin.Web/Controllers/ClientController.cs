using Admin.BusinessLogic;
using CommonLibraries;
using OrderAdminPanel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderAdminPanel.Controllers
{
    [AuthorizeWebForm]
    public class ClientController : Controller
    {
        public ActionResult Edit(int ID)
        {
            if (ID > 0)
            {
                var client = ClientMaster.GetAllClients().FirstOrDefault(x => x.ClientID == ID);
                client.Password = StringCipher.Decrypt(client.Password);
                return View(client);
            }
            else
            {
                var client = new ClientMaster();
                client.AccountExpiredOn = DateTime.Now.Add(new TimeSpan(365, 0, 0, 0, 0));
                return View(client);
            }
        }

        public ActionResult GetList()
        {
            var currUser = (Admin.BusinessLogic.User)Session["User"];
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
        public JsonResult Edit(ClientMaster clientData)
        {
            JsonResponse response = new JsonResponse();
            if (ModelState.IsValid)
            {
                var currUser = (Admin.BusinessLogic.User)Session["User"];
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

        public ActionResult Delete(int ID)
        {
            if (ClientMaster.DeleteClient(ID) == 1)
            {
                return RedirectToAction("Edit", "Client", new { ID = 0 });
            }
            else
            {
                return RedirectToAction("Edit", "Client", new { ID = ID });
            }
        }

        public ActionResult Remove()
        {
            return View(ClientMaster.GetAllClients());
        }

        public ActionResult DeleteEntireClient(int ClientId)
        {
            ClientMaster.DeleteEntireClient(ClientId);
            return RedirectToAction("Edit");
        }

    }
}
