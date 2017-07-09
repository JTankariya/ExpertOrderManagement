using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IRate2Helper : IHelper<Rate2>
    {
        List<Rate2> GetByRate(string Link);
    }
}
