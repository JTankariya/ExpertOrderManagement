using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Parameters;
namespace BusinessLogic
{
    public class ProductGroup
    {
        public int ClientCompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Flag { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Parent { get; set; }
        public string VatName { get; set; }
        public int P_Type { get; set; }
        public int Tag { get; set; }
        public Guid RefId { get; set; }
        public string OperationFlag { get; set; }

        private IProductGroupManager _manager;

        public IProductGroupManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = ExpertOrderBusinessInit.kernel.Get<IManagerFactory<ProductGroup, IProductGroupManager>>(new ConstructorArgument("context", this)).Create(this);
                }
                return _manager;
            }
        }
    }
}
