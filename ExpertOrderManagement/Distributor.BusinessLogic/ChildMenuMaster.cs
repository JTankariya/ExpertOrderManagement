using Distributor.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distributor.BusinessLogic
{
    public class ChildMenuMaster
    {
        public int ChildMenuID { get; set; }
        public string ChildMenuName { get; set; }
        public int MenuID { get; set; }
        public string Query { get; set; }
        public string ZoomQuery { get; set; }
        public bool HasGraph { get; set; }
        public bool HasChildData { get; set; }

        public static void SaveChildMenus(ChildMenuMaster[] subMenus)
        {
            foreach (var submenu in subMenus)
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@CMID", submenu.ChildMenuID);
                param.Add("@MID", submenu.MenuID);
                param.Add("@CName", submenu.ChildMenuName);
                param.Add("@CQuery", submenu.Query);
                param.Add("@CHasChildData", submenu.HasChildData);
                param.Add("@CHasGraph", submenu.HasGraph);
                param.Add("@CZoomQuery", (string.IsNullOrEmpty(submenu.ZoomQuery) ? "" : submenu.ZoomQuery));

                DBHelper.ExecuteNonQuery("SaveChildMenu", param, true);
            }
        }

        public static IEnumerable<ChildMenuMaster> GetChildMenusByMenuId(int MenuID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@MID", MenuID);
            DataTable dt = DBHelper.GetDataTable("GetChildMenusByMenuId", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<ChildMenuMaster>(dt);
            else
                return null;
        }

        public static int DeleteChildMenuById(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("DeleteChildMenuById", param, true);
            return 1;
        }
    }
}
