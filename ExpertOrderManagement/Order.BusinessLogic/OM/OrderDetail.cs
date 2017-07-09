using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class OrderDetail
    {
        public int ClientCompanyId { get; set; }
        public string Division { get; set; }
        public string Ord_No { get; set; }
        public DateTime Ord_Dt { get; set; }
        public string Code { get; set; }
        public decimal Qty { get; set; }
        public decimal Bal { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public string Unit { get; set; }
        public decimal It_Disc { get; set; }
        public decimal It_Tax { get; set; }
        public decimal It_Oc { get; set; }
        public decimal Stk_Qty { get; set; }
        public DateTime Dly_Dt { get; set; }
        public string CV_Code { get; set; }
        public string Type { get; set; }
        public string BatchNo { get; set; }
        public string Narr1 { get; set; }
        public string Narr2 { get; set; }
        public bool Adjusted { get; set; }
        public string Trackno { get; set; }
        public string RTUnit { get; set; }
        public bool Tag { get; set; }
        public Guid RefId { get; set; }
        public string OperationFlag { get; set; }
        public bool IsDeleted { get; set; }
    }
}
