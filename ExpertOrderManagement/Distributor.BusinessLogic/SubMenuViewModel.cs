using Distributor.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distributor.BusinessLogic
{
    public class SubMenuViewModel
    {
        public int ClientMenuID { get; set; }
        public int ChildMenuID { get; set; }
        public int ClientChildMenuID { get; set; }
        public int MenuID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ChildMenuName { get; set; }
        public string MenuName { get; set; }
        public string Query { get; set; }
        public string ZoomQuery { get; set; }
        public static IEnumerable<SubMenuViewModel> GetClientMenuMaster(int AdminID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@AID", AdminID);
            DataTable dt = DBHelper.GetDataTable("GetClientMenuMaster", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<SubMenuViewModel>(dt);
            else
                return null;
        }

        public static IEnumerable<SubMenuViewModel> GetSubMenuDetail(int AdminID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@AID", AdminID);
            DataTable dt = DBHelper.GetDataTable("GetSubMenuDetail", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<SubMenuViewModel>(dt);
            else
                return null;
        }

        public static void SaveSubMenuForClient(int DetailChildMenuId, int DetailClientMenuId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@DCliMID", DetailClientMenuId);
            param.Add("@DChiMID", DetailChildMenuId);
            DBHelper.ExecuteNonQuery("SaveSubMenuForClient", param, true);
        }

        public static void DeleteSubMenuForClient(int DetailChildMenuId, int DetailClientMenuId, int AdminID)
        {
            var menu = GetSubMenuDetail(AdminID).FirstOrDefault(x => x.ChildMenuID == DetailChildMenuId && x.ClientMenuID == DetailClientMenuId);
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@CMID", menu.ClientChildMenuID);
            DBHelper.ExecuteNonQuery("DeleteSubMenuForClient", param, true);
        }
    }
}
