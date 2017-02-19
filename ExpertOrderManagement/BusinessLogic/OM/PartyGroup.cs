using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class PartyGroup
    {
        public int ClientCompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Origname { get; set; }
        public string ALIE { get; set; }
        public string Order { get; set; }
        public bool Schedule { get; set; }
        public bool Showhead { get; set; }
        public bool Type { get; set; }
        public string Flag { get; set; }
        public string Under { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public string Parent { get; set; }
        public bool Reserved { get; set; }
        public bool Tag { get; set; }

    }
}
