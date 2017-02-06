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
    public class ClientMenuMaster
    {
        public int MenuId { get; set; }
        public int SrNo { get; set; }
        public bool IsSelected { get; set; }
        public string MenuName { get; set; }
        public string ZoomQuery { get; set; }
        public string TextMessage { get; set; }
        public string EmailMessage { get; set; }
        public string Query { get; set; }
        public bool HasChildData { get; set; }
        public bool IsDashboard { get; set; }
        public bool HasGraph { get; set; }
        public bool otherOptions { get; set; }
        public bool HasChildMenu { get; set; }
        public Int64 ClientMenuID { get; set; }

        public static IEnumerable<ClientMenuMaster> GetClientMenuByClientId(int Id)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", Id);
            DataTable dt = DBHelper.GetDataTable("GetClientMenu", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<ClientMenuMaster>(dt);
            else
                return null;
        }

        public static IEnumerable<ClientMenuMaster> GetAllMenus()
        {
            DataTable dt = DBHelper.GetDataTable("GetAllMenus", null, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToEnumerable<ClientMenuMaster>(dt);
            else
                return null;
        }

        public static int SaveClientMenus(ClientMenuMaster[] clientMenus, int ClientID)
        {
            if (clientMenus != null && clientMenus.Length > 0)
            {
                foreach (var menu in clientMenus)
                {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@ISelected", menu.IsSelected);
                    param.Add("@CID", ClientID);
                    param.Add("@MID", menu.MenuId);
                    param.Add("@CMenuID", menu.ClientMenuID);
                    param.Add("@HDashboard", menu.IsDashboard);
                    param.Add("@HSubMenu", menu.HasChildMenu);
                    DBHelper.ExecuteNonQuery("SaveClientMenus", param, true);
                }
                return 1;
            }
            else
                return 0;
        }

        public static ClientMenuMaster GetMenuByMenuID(int ID)
        {
            return GetAllMenus().FirstOrDefault(x => x.MenuId == ID);
        }

        public static int DeleteMenuByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@MID", ID);
            DBHelper.ExecuteNonQuery("DeleteMenu", param, true);
            return 1;
        }

        public static bool CheckMenuReference(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@MID", ID);
            DataTable dt = DBHelper.GetDataTable("CheckMenuReference", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return (Convert.ToInt32(dt.Rows[0][0]) > 0 ? true : false);
            else
                return false;
        }

        public int Save()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@MID", this.MenuId);
            param.Add("@MName", this.MenuName);
            param.Add("@MQuery", this.Query);
            param.Add("@MSrNo", this.SrNo);
            param.Add("@MIsDashboard", this.IsDashboard);
            param.Add("@MHasChildMenu", this.HasChildMenu);
            param.Add("@MHasChildData", this.HasChildData);
            param.Add("@MHasGraph", this.HasGraph);
            param.Add("@MZoomQuery", (string.IsNullOrEmpty(this.ZoomQuery) ? "" : this.ZoomQuery));
            DataTable dt = DBHelper.GetDataTable("AddMenu", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0][0]);
            else
                return 0;
        }

        public static bool CheckDashboardExist(int MenuID)
        {
            if (GetAllMenus().Where(x => x.IsDashboard && x.MenuId != MenuID) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckMenuSrNo(int SrNo, int MenuId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@MSrNo", SrNo);
            param.Add("@MID", MenuId);
            DataTable dt = DBHelper.GetDataTable("CheckMenuSrNo", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}
