using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class PartyGroupManager : IPartyGroupManager
    {
        private PartyGroup _context;
        public PartyGroupManager(PartyGroup context)
        {
            _context = context;
        }
        public ResponseMsg Save()
        {
            throw new NotImplementedException();
        }

        public ResponseMsg Delete()
        {
            throw new NotImplementedException();
        }
    }
}
