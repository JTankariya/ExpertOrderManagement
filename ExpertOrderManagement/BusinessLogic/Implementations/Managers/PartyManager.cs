using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class PartyManager : IPartyManager
    {
        private Party _context;
        public PartyManager(Party context)
        {
            _context = context;
        }
        public ResponseMsg Save()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ClientCompanyId", _context.ClientCompanyId);
            param.Add("@Code", _context.Code);
            param.Add("@Name", _context.Name);
            param.Add("@Group", _context.Group);
            param.Add("@Bal_Op", _context.Bal_Op);
            param.Add("@Bal_DC", _context.Bal_DC);
            param.Add("@OpBal", _context.OpBal);
            param.Add("@TotDR", _context.TotDR);
            param.Add("@TotCR", _context.TotCR);
            param.Add("@ClBAL", _context.ClBAL);
            param.Add("@Budget", _context.Budget);
            param.Add("@Tax_Calc", _context.Tax_Calc);
            param.Add("@Oth_Calc", _context.Oth_Calc);
            param.Add("@Post", _context.Post);
            param.Add("@Oth_AL", _context.Oth_AL);
            param.Add("@CalcOn", _context.CalcOn);
            param.Add("@Dep_PCN", _context.Dep_PCN);
            param.Add("@Add1", _context.Add1);
            param.Add("@Add2", _context.Add2);
            param.Add("@Add3", _context.Add3);
            param.Add("@Phone", _context.Phone);
            param.Add("@Zone", _context.Zone);
            param.Add("@State", _context.State);
            param.Add("@CSTNo", _context.CSTNo);
            param.Add("@STNO", _context.STNO);
            param.Add("@ITNO", _context.ITNO);
            param.Add("@LICNO", _context.LICNO);
            param.Add("@VATNO", _context.VATNO);
            param.Add("@FaxNo", _context.FaxNo);
            param.Add("@Email", _context.Email);
            param.Add("@Mobile", _context.Mobile);
            param.Add("@WardNo", _context.WardNo);
            param.Add("@Cr_Days", _context.Cr_Days);
            param.Add("@Cr_Limit", _context.Cr_Limit);
            param.Add("@Int_Rate", _context.Int_Rate);
            param.Add("@AdvanceOpt", _context.AdvanceOpt);
            param.Add("@Flag", _context.Flag);
            param.Add("@Act_Type", _context.Act_Type);
            param.Add("@Class", _context.Class);
            param.Add("@CC", _context.CC);
            param.Add("@Category", _context.Category);
            param.Add("@Remarks", _context.Remarks);
            param.Add("@UseRef", _context.UseRef);
            param.Add("@RateType", _context.RateType);
            param.Add("@FBTRate", _context.FBTRate);
            param.Add("@ISTds", _context.ISTds);
            param.Add("@Noval", _context.Noval);
            param.Add("@Blocked", _context.Blocked);
            param.Add("@BSGroup", _context.BSGroup);
            param.Add("@Tag", _context.Tag);
            param.Add("@OperationFlag", _context.OperationFlag);
            param.Add("@RefId", _context.RefId);
            DBHelper.ExecuteNonQuery("Order.SaveParty", param, true);
            return new ResponseMsg() { IsSuccess = true };
        }

        public ResponseMsg Delete()
        {
            throw new NotImplementedException();
        }
    }
}
