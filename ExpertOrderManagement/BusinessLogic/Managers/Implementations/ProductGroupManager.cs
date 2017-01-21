using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ProductGroupManager : IProductGroupManager
    {
        private ProductGroup _context;
        public ProductGroupManager(ProductGroup context)
        {
            _context = context;
        }
        public ResponseMsg Save()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ClientCompanyId", _context.ClientCompanyId);
            param.Add("@Code", _context.Code);
            param.Add("@Name", _context.Name);
            param.Add("@Class", _context.Class);
            param.Add("@Flag", _context.Flag);
            param.Add("@Category1", _context.Category1);
            param.Add("@Category2", _context.Category2);
            param.Add("@Category3", _context.Category3);
            param.Add("@Parent", _context.Parent);
            param.Add("@VatName", _context.VatName);
            param.Add("@P_Type", _context.P_Type);
            param.Add("@Tag", _context.Tag);
            param.Add("@OperationFlag", _context.OperationFlag);
            DBHelper.ExecuteNonQuery("SaveProductGroup", param, true);
            return new ResponseMsg() { IsSuccess = true };
        }
    }
}
