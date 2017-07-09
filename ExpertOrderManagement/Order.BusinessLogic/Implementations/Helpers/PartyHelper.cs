using CommonLibraries;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class PartyHelper : Helper<Party>, IPartyHelper
    {
        public PartyHelper(string tName, int companyId)
            : base(tName, companyId)
        {

        }

        public List<Party> CheckDuplicateName(string Name, string Code)
        {
            if (string.IsNullOrEmpty(Code))
            {
                return DBHelper.ConvertToList<Party>("select * from " + base._tableName + " where ClientCompanyId = " + base._companyId + " and Name='" + Name + "'");
            }
            else
            {
                return DBHelper.ConvertToList<Party>("select * from " + base._tableName + " where ClientCompanyId = " + base._companyId + " and Name='" + Name + "' and Code!='" + Code + "'");
            }
        }


        public List<Party> GetSundryDebtors()
        {
            var query = "select * from " + base._tableName + " where [Group] = " + Constants.DEFAULT_SUNDRY_DEBTORS_CODE + " and ClientCompanyId=" + base._companyId;
            return DBHelper.ConvertToList<Party>(query);
        }
    }
}
