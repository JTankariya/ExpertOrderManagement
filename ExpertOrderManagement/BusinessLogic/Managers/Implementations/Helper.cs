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
        protected int _companyId;
        public Helper(string tName, int companyId)
        {
            _tableName = tName;
            _companyId = companyId;
        }
        public IEnumerable<T> GetAll()
        {
            var list = DBHelper.ConvertToEnumerable<T>("select * from " + _tableName + " where ClientCompanyId = " + _companyId);
            if (list.IsNullOrEmpty())
            {
                return null;
            }
            return list;
        }

        public T GetById(int Id)
        {
            return DBHelper.ConvertToEnumerable<T>("select * from " + _tableName + " where Code='" + Id + "' and ClientCompanyId = " + _companyId).FirstOrDefault();
        }

        public T GetByRefId(string refId)
        {
            return DBHelper.ConvertToEnumerable<T>("select * from " + _tableName + " where RefId='" + refId + "' and ClientCompanyId = " + _companyId).FirstOrDefault();
        }
    }
}
