using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ProductGroupHelper : Helper<ProductGroup>, IProductGroupHelper
    {
        public ProductGroupHelper(string tName, int companyId)
            : base(tName, companyId)
        {

        }
        public IEnumerable<ProductGroup> GetProductGroupsForParentDropDown()
        {
            return DBHelper.ConvertToEnumerable<ProductGroup>("select Code,Name from " + base._tableName + " where ClientCompanyId = " + base._companyId);
        }
        public IEnumerable<ProductGroup> GetAll()
        {
            return DBHelper.ConvertToEnumerable<ProductGroup>("select * from " + base._tableName + " where ClientCompanyId = " + base._companyId);
        }
    }
}
