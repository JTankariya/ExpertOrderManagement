using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class Batch
    {
        public int ClientCompanyId { get; set; }
        public string Code { get; set; }
        public string BatchNo { get; set; }
        public string Name { get; set; }
        public decimal Op_Qty { get; set; }
        public decimal TotIn { get; set; }
        public decimal TotOut { get; set; }
        public decimal Cl_Qty { get; set; }
        public decimal Op_Value { get; set; }
        public decimal Op_Rate { get; set; }
        public decimal Sl_Rate { get; set; }
        public decimal Pu_Rate { get; set; }
        public decimal ClRate { get; set; }
        public string Category { get; set; }
        public string Class { get; set; }
        public string Flag { get; set; }
        public string U_Code { get; set; }
        public int Op_Package { get; set; }
        public decimal TotPQty { get; set; }
        public decimal TotPVal { get; set; }
        public DateTime? MFGDt { get; set; }
        public DateTime? ExpireDt { get; set; }
        public int AdvanceOpt { get; set; }
        public string Make { get; set; }
        public string Packing { get; set; }
        public decimal MRP { get; set; }
        public decimal ED { get; set; }
        public decimal Sl_Rate1 { get; set; }
        public decimal Sl_Rate2 { get; set; }
        public decimal Sl_Rate3 { get; set; }
        public int Tag { get; set; }
        public Guid RefId { get; set; }
        public string OperationFlag { get; set; }

    }
}
