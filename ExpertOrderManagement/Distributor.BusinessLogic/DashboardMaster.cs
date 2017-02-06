using Distributor.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distributor.BusinessLogic
{
    [Serializable]
    public class DashboardMaster
    {
        public int DashboardID { get; set; }
        public int MenuID { get; set; }
        public string Type { get; set; }
        public string DashboardName { get; set; }
        public string MenuName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Query { get; set; }
        public string ZoomMenu { get; set; }
        public int ZoomMenuId { get; set; }
        public int ClientMenuID { get; set; }
        public int ClientDashboardID { get; set; }



        public static void SaveDashboard(DashboardMaster[] dashboardMenus)
        {
            foreach (var dashboardmenu in dashboardMenus)
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@DMID", dashboardmenu.DashboardID);
                param.Add("@MID", dashboardmenu.MenuID);
                param.Add("@DType", dashboardmenu.Type);
                param.Add("@DName", dashboardmenu.DashboardName);
                param.Add("@DQuery", dashboardmenu.Query);
                param.Add("@ZMenuID", dashboardmenu.ZoomMenuId);
                DBHelper.ExecuteNonQuery("SaveDashboardMenu", param, true);
            }
        }

        public static IEnumerable<DashboardMaster> GetDashboardByMenuId(int MenuID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@MID", MenuID);
            DataTable dt = DBHelper.GetDataTable("GetDashboardByMenuId", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<DashboardMaster>(dt);
            else
                return null;
        }

        public static int DeleteDashboardById(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("DeleteDashboardById", param, true);
            return 1;
        }



        public static IEnumerable<DashboardMaster> GetDashboardMenuMaster()
        {
            DataTable dt = DBHelper.GetDataTable("GetDashboardMenuMaster", null, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<DashboardMaster>(dt);
            else
                return null;
        }

        public static IEnumerable<DashboardMaster> GetDashboardMenus(int AdminID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@AID", AdminID);
            DataTable dt = DBHelper.GetDataTable("GetDashboardMenus", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<DashboardMaster>(dt);
            else
                return null;
        }

        public static void SaveDashboardUserMenu(int DetailClientMenuId, int DetailDashboardId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@DCMID", DetailClientMenuId);
            param.Add("@DDID", DetailDashboardId);
            DBHelper.ExecuteNonQuery("SaveDashboardUserMenu", param, true);
        }

        public static void DeleteDashboardMenuOfClient(int DetailClientMenuId, int DetailDashboardId, int AdminID)
        {
            var menu = GetDashboardMenus(AdminID).FirstOrDefault(x => x.ClientMenuID == DetailClientMenuId && x.DashboardID == DetailDashboardId);
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@CDID", menu.ClientDashboardID);
            DBHelper.ExecuteNonQuery("DeleteDashboardMenuForClient", param, true);
        }
    }
}
