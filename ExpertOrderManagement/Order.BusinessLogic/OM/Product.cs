using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace BusinessLogic
{
    public class Product
    {
        public int ClientCompanyId { get; set; }
        public string Code { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool Batch { get; set; }
        public bool Decimals { get; set; }
        public bool DUnit { get; set; }
        public string Unit1 { get; set; }
        public string Unit2 { get; set; }
        public string Unit3 { get; set; }
        public string Unit4 { get; set; }
        public string Unit5 { get; set; }
        public string Unit6 { get; set; }
        public decimal Ratio2 { get; set; }
        public decimal Ratio3 { get; set; }
        public decimal Ratio4 { get; set; }
        public decimal Ratio5 { get; set; }
        public decimal Ratio6 { get; set; }
        public decimal Op_Qty { get; set; }
        public decimal MaxLevel { get; set; }
        public decimal MinLevel { get; set; }
        public decimal ReOrder { get; set; }
        public decimal TotIn { get; set; }
        public decimal TotOut { get; set; }
        public decimal Cl_Qty { get; set; }
        public decimal Op_Value { get; set; }
        public decimal Op_Rate { get; set; }
        public decimal Sl_Rate { get; set; }
        public decimal Pu_Rate { get; set; }
        public decimal ClRate { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public decimal PackingQty { get; set; }
        public string Flag { get; set; }
        public bool AdvanceOpt { get; set; }
        public bool Service { get; set; }
        public decimal Op_Package { get; set; }
        public decimal TotPQty { get; set; }
        public decimal TotPVal { get; set; }
        public bool Valuation { get; set; }
        public decimal Min_Rate { get; set; }
        public string VMethod { get; set; }
        public string I_Vatcode { get; set; }
        public string O_Vatcode { get; set; }
        public bool Blocked { get; set; }
        public bool Tag { get; set; }
        public string OperationFlag { get; set; }
        public Guid RefId { get; set; }

        private IProductManager _manager;

        public IProductManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = ExpertOrderBusinessInit.kernel.Get<IManagerFactory<Product, IProductManager>>(new ConstructorArgument("context", this)).Create(this);
                }
                return _manager;
            }
        }
    }
}
