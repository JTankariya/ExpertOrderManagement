using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public abstract class Helper<T> : IHelper<T> where T : new()
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
                return Enumerable.Empty<T>();
            }
            return list;
        }

        public IEnumerable<T> GetAll(string SPName)
        {
            var list = DBHelper.ConvertToEnumerable<T>(DBHelper.GetDataTable(SPName, null, true));
            if (list.IsNullOrEmpty())
            {
                return Enumerable.Empty<T>();
            }
            return list;
        }

        public T GetById(int Id)
        {
            return DBHelper.ConvertToEnumerable<T>("select * from " + _tableName + " where Id='" + Id + "'").FirstOrDefault();
        }

        public T GetByRefId(string refId)
        {
            return DBHelper.ConvertToEnumerable<T>("select * from " + _tableName + " where RefId='" + refId + "'").FirstOrDefault();
        }


        public T GetByCode(string Code)
        {
            var records = DBHelper.ConvertToEnumerable<T>("select * from " + _tableName + " where Code='" + Code + "' and ClientCompanyId=" + _companyId);
            if (records.IsNullOrEmpty())
            {
                return new T();
            }
            else
            {
                return records.FirstOrDefault();
            }

        }
    }
}
