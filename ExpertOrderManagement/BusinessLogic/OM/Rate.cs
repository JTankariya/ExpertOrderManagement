using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class Rate
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public DateTime? FDate { get; set; }
        public DateTime? TDate { get; set; }
        public Guid RefId { get; set; }
        public int ClientCompanyId { get; set; }
        public string OperationFlag { get; set; }

        private List<Rate2> _rates;
        public List<Rate2> rates
        {
            get {
                if (_rates == null)
                {
                    _rates = Helpers.Rate2Helper.GetByRate(this.Link);
                }
                return _rates;
            }
        }
    }
}
