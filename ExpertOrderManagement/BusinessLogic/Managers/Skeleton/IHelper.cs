using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IHelper<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        T GetByRefId(string refId);
    }
}
