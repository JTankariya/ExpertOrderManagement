using DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class OrderManager : IOrderManager
    {
        private Order _context;
        public OrderManager(Order context)
        {
            _context = context;
        }

        public ResponseMsg Save()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ClientCompanyId", _context.ClientCompanyId);
            param.Add("@Division", _context.Division);
            param.Add("@Ord_no", _context.Ord_no);
            param.Add("@Ord_Dt", _context.Ord_Dt);
            param.Add("@Code", _context.Code);
            param.Add("@Remarks", _context.Remarks);
            param.Add("@Reference", _context.Reference);
            param.Add("@Agent", _context.Agent);
            param.Add("@Zone", _context.Zone);
            param.Add("@Ord_value", _context.Ord_value);
            param.Add("@Bill_Ins", _context.Bill_Ins);
            param.Add("@Pay_Terms", _context.Pay_Terms);
            param.Add("@Del_Ins", _context.Del_Ins);
            param.Add("@Adjusted", _context.Adjusted);
            param.Add("@Type", _context.Type);
            param.Add("@Advance", _context.Advance);
            param.Add("@RefId", _context.RefId);
            param.Add("@OperationFlag", _context.OperationFlag);
            if (_context.Details != null && _context.Details.Count > 0)
            {
                var rm = new DataTable();
                rm.TableName = "[dbo].[OrderDetail]";
                rm.Columns.Add("ClientCompanyId", typeof(int));
                rm.Columns.Add("Division", typeof(string));
                rm.Columns.Add("Ord_No", typeof(string));
                rm.Columns.Add("Ord_Dt", typeof(DateTime));
                rm.Columns.Add("Code", typeof(string));
                rm.Columns.Add("Qty", typeof(decimal));
                rm.Columns.Add("Bal", typeof(decimal));
                rm.Columns.Add("Rate", typeof(decimal));
                rm.Columns.Add("Value", typeof(decimal));
                rm.Columns.Add("Unit", typeof(string));
                rm.Columns.Add("It_Disc", typeof(decimal));
                rm.Columns.Add("It_Tax", typeof(decimal));
                rm.Columns.Add("It_Oc", typeof(decimal));
                rm.Columns.Add("Stk_Qty", typeof(decimal));
                rm.Columns.Add("Dly_Dt", typeof(DateTime));
                rm.Columns.Add("CV_Code", typeof(string));
                rm.Columns.Add("Type", typeof(string));
                rm.Columns.Add("BatchNo", typeof(string));
                rm.Columns.Add("Narr1", typeof(string));
                rm.Columns.Add("Narr2", typeof(string));
                rm.Columns.Add("Adjusted", typeof(bool));
                rm.Columns.Add("Trackno", typeof(string));
                rm.Columns.Add("RTUnit", typeof(string));
                rm.Columns.Add("Tag", typeof(bool));
                rm.Columns.Add("RefId", typeof(Guid));
                rm.Columns.Add("OperationFlag", typeof(string));

                foreach (var detail in _context.Details)
                {
                    rm.Rows.Add();
                    rm.Rows[rm.Rows.Count - 1]["ClientCompanyId"] = detail.ClientCompanyId;
                    rm.Rows[rm.Rows.Count - 1]["Division"] = detail.Division;
                    rm.Rows[rm.Rows.Count - 1]["Ord_No"] = detail.Ord_No;
                    rm.Rows[rm.Rows.Count - 1]["Ord_Dt"] = detail.Ord_Dt;
                    rm.Rows[rm.Rows.Count - 1]["Code"] = detail.Code;
                    rm.Rows[rm.Rows.Count - 1]["Qty"] = detail.Qty;
                    rm.Rows[rm.Rows.Count - 1]["Bal"] = detail.Bal;
                    rm.Rows[rm.Rows.Count - 1]["Rate"] = detail.Rate;
                    rm.Rows[rm.Rows.Count - 1]["Value"] = detail.Value;
                    rm.Rows[rm.Rows.Count - 1]["Unit"] = detail.Unit;
                    rm.Rows[rm.Rows.Count - 1]["It_Disc"] = detail.It_Disc;
                    rm.Rows[rm.Rows.Count - 1]["It_Tax"] = detail.It_Tax;
                    rm.Rows[rm.Rows.Count - 1]["It_Oc"] = detail.It_Oc;
                    rm.Rows[rm.Rows.Count - 1]["Stk_Qty"] = detail.Stk_Qty;
                    rm.Rows[rm.Rows.Count - 1]["Dly_Dt"] = detail.Dly_Dt;
                    rm.Rows[rm.Rows.Count - 1]["CV_Code"] = detail.CV_Code;
                    rm.Rows[rm.Rows.Count - 1]["Type"] = detail.Type;
                    rm.Rows[rm.Rows.Count - 1]["BatchNo"] = detail.BatchNo;
                    rm.Rows[rm.Rows.Count - 1]["Narr1"] = detail.Narr1;
                    rm.Rows[rm.Rows.Count - 1]["Narr2"] = detail.Narr2;
                    rm.Rows[rm.Rows.Count - 1]["Adjusted"] = detail.Adjusted;
                    rm.Rows[rm.Rows.Count - 1]["Trackno"] = detail.Trackno;
                    rm.Rows[rm.Rows.Count - 1]["RTUnit"] = detail.RTUnit;
                    rm.Rows[rm.Rows.Count - 1]["Tag"] = detail.Tag;
                    rm.Rows[rm.Rows.Count - 1]["RefId"] = detail.RefId;
                    rm.Rows[rm.Rows.Count - 1]["OperationFlag"] = detail.OperationFlag;
                }
                param.Add("@RawMaterials", rm);
            }
            DBHelper.ExecuteNonQuery("Order.SaveOrder", param, true);
            return new ResponseMsg() { IsSuccess = true };
        }

        public ResponseMsg Delete()
        {
            throw new NotImplementedException();
        }
    }
}
