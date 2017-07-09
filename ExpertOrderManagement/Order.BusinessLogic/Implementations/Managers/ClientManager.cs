using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ClientManager : IClientManager
    {
        private Client _context;
        public ClientManager(Client context)
        {
            _context = context;
        }
        public ResponseMsg Save()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", _context.Id);
            param.Add("@CreateDate", DateTime.Now);
            param.Add("@UName", _context.UserName);
            param.Add("@PWord", _context.Password);
            param.Add("@Mobile", _context.MobileNo);
            param.Add("@FName", _context.FirstName);
            param.Add("@LName", _context.LastName);
            param.Add("@CName", _context.CompanyName);
            param.Add("@CAddress", _context.CompanyAddress);
            param.Add("@Email", _context.Email);
            param.Add("@NoDays", _context.NoOfDays);
            param.Add("@NoAccessUsers", _context.NoOfAccessUsers);
            param.Add("@NoCompanyUser", _context.NoOfCompanyPerUser);
            param.Add("@ExpiryDate", _context.AccountExpiredOn);
            param.Add("@UploadingProcess", _context.IsUploadingProcessStart);
            param.Add("@AdminID", _context.CreatedAdminID);
            param.Add("@DistributorID", _context.CreatedDistributorId);
            param.Add("@QueryRight", _context.QueryRights);
            param.Add("@IsWithout", _context.IsWithout);
            DBHelper.ExecuteNonQuery("Order.SaveClient", param, true);
            return new ResponseMsg() { IsSuccess = true };
        }

        public ResponseMsg Delete()
        {
            throw new NotImplementedException();
        }
    }
}
