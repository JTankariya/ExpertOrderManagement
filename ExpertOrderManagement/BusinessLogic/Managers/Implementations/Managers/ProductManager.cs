using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ProductManager : IProductManager
    {
        private Product _context;
        public ProductManager(Product context)
        {
            _context = context;
        }
        public ResponseMsg Save()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ClientCompanyId", _context.ClientCompanyId);
            param.Add("@Code", _context.Code);
            param.Add("@Group", _context.Group);
            param.Add("@Name", _context.Name);
            param.Add("@Desc", _context.Desc);
            param.Add("@Batch", _context.Batch);
            param.Add("@Decimals", _context.Decimals);
            param.Add("@DUnit", _context.DUnit);
            param.Add("@Unit1", _context.Unit1);
            param.Add("@Unit2", _context.Unit2);
            param.Add("@Unit3", _context.Unit3);
            param.Add("@Unit4", _context.Unit4);
            param.Add("@Unit5", _context.Unit5);
            param.Add("@Unit6", _context.Unit6);
            param.Add("@Ratio2", _context.Ratio2);
            param.Add("@Ratio3", _context.Ratio3);
            param.Add("@Ratio4", _context.Ratio4);
            param.Add("@Ratio5", _context.Ratio5);
            param.Add("@Ratio6", _context.Ratio6);
            param.Add("@Op_Qty", _context.Op_Qty);
            param.Add("@MaxLevel", _context.MaxLevel);
            param.Add("@MinLevel", _context.MinLevel);
            param.Add("@ReOrder", _context.ReOrder);
            param.Add("@TotIn", _context.TotIn);
            param.Add("@TotOut", _context.TotOut);
            param.Add("@Cl_Qty", _context.Cl_Qty);
            param.Add("@Op_Value", _context.Op_Value);
            param.Add("@Op_Rate", _context.Op_Rate);
            param.Add("@Sl_Rate", _context.Sl_Rate);
            param.Add("@Pu_Rate", _context.Pu_Rate);
            param.Add("@ClRate", _context.ClRate);
            param.Add("@Category1", _context.Category1);
            param.Add("@Category2", _context.Category2);
            param.Add("@PackingQty", _context.PackingQty);
            param.Add("@Flag", _context.Flag);
            param.Add("@AdvanceOpt", _context.AdvanceOpt);
            param.Add("@Service", _context.Service);
            param.Add("@Op_Package", _context.Op_Package);
            param.Add("@TotPQty", _context.TotPQty);
            param.Add("@TotPVal", _context.TotPVal);
            param.Add("@Valuation", _context.Valuation);
            param.Add("@Min_Rate", _context.Min_Rate);
            param.Add("@VMethod", _context.VMethod);
            param.Add("@I_VatCode", _context.I_Vatcode);
            param.Add("@O_VatCode", _context.O_Vatcode);
            param.Add("@Blocked", _context.Blocked);
            param.Add("@Tag", _context.OperationFlag);
            param.Add("@OperationFlag", _context.OperationFlag);
            param.Add("@RefId", _context.RefId);
            DBHelper.ExecuteNonQuery("Order.SaveProduct", param, true);
            return new ResponseMsg() { IsSuccess = true };
        }


        public ResponseMsg Delete()
        {
            throw new NotImplementedException();
        }
    }
}
