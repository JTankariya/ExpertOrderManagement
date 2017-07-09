using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IHelper<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string SPName);
        T GetById(int Id);
        T GetByRefId(string refId);
        T GetByCode(string Code);
    }
}
