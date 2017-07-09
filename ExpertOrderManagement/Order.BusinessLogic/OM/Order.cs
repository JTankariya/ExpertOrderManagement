using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace BusinessLogic
{
    public class Order
    {
        public int ClientCompanyId { get; set; }
        public string Division { get; set; }
        public string Ord_no { get; set; }
        public DateTime Ord_Dt { get; set; }
        public string Code { get; set; }
        public string Remarks { get; set; }
        public string Reference { get; set; }
        public string Agent { get; set; }
        public string Zone { get; set; }
        public decimal Ord_value { get; set; }
        public string Bill_Ins { get; set; }
        public string Pay_Terms { get; set; }
        public string Del_Ins { get; set; }
        public bool Adjusted { get; set; }
        public string Type { get; set; }
        public decimal Advance { get; set; }
        public Guid RefId { get; set; }
        public string OperationFlag { get; set; }
        public List<OrderDetail> Details { get; set; }

        private Party _party;

        public Party OrderParty
        {
            get
            {
                if (_party != null)
                {
                    _party = Helpers.PartyHelper.GetByCode(Code);
                }
                return _party;
            }
        }

        private IOrderManager _manager;

        public IOrderManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = ExpertOrderBusinessInit.kernel.Get<IManagerFactory<Order, IOrderManager>>().Create(this);
                }
                return _manager;
            }
        }

    }
}
