using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public abstract class Helper<T> : IHelper<T>
    {
        protected string _tableName;
        public Helper(string tName)
        {
            _tableName = tName;
        }
        public IEnumerable<T> GetAll()
        {
            var list = DBHelper.ConvertToEnumerable<T>("select * from " + _tableName);
            if (list.IsNullOrEmpty())
            {
                return null;
            }
            return list;
        }

        public T GetById(int Id)
        {
            return DBHelper.ConvertToEnumerable<T>("select * from " + _tableName + " where Id=" + Id).FirstOrDefault();
        }

        public T GetByRefId(string refId)
        {
            return DBHelper.ConvertToEnumerable<T>("select * from " + _tableName + " where RefId=" + refId).FirstOrDefault();
        }
    }
}
