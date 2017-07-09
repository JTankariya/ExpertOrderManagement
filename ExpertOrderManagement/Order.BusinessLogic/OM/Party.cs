using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace BusinessLogic
{
    public class Party
    {
        public int ClientCompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public decimal Bal_Op { get; set; }
        public string Bal_DC { get; set; }
        public decimal OpBal { get; set; }
        public decimal TotDR { get; set; }
        public decimal TotCR { get; set; }
        public decimal ClBAL { get; set; }
        public decimal Budget { get; set; }
        public bool Tax_Calc { get; set; }
        public bool Oth_Calc { get; set; }
        public bool Post { get; set; }
        public string Oth_AL { get; set; }
        public string CalcOn { get; set; }
        public decimal Dep_PCN { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string Add3 { get; set; }
        public string Phone { get; set; }
        public string Zone { get; set; }
        public string State { get; set; }
        public string CSTNo { get; set; }
        public string STNO { get; set; }
        public string ITNO { get; set; }
        public string LICNO { get; set; }
        public string VATNO { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string WardNo { get; set; }
        public int Cr_Days { get; set; }
        public decimal Cr_Limit { get; set; }
        public decimal Int_Rate { get; set; }
        public bool AdvanceOpt { get; set; }
        public string Flag { get; set; }
        public int Act_Type { get; set; }
        public string Class { get; set; }
        public string CC { get; set; }
        public string Category { get; set; }
        public string Remarks { get; set; }
        public bool UseRef { get; set; }
        public string RateType { get; set; }
        public decimal FBTRate { get; set; }
        public bool ISTds { get; set; }
        public bool Noval { get; set; }
        public bool Blocked { get; set; }
        public string BSGroup { get; set; }
        public bool Tag { get; set; }
        public string OperationFlag { get; set; }
        public Guid RefId { get; set; }
        private PartyGroup _group;

        public PartyGroup PartyGroup
        {
            get
            {
                if (_group == null)
                {
                    if (string.IsNullOrEmpty(Group))
                    {
                        return new PartyGroup();
                    }
                    else
                    {
                        _group = Helpers.PartyGroupHelper.GetByCode(Group);
                    }
                    
                }
                return _group;
            }
        }

        private IPartyManager _manager;

        public IPartyManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = ExpertOrderBusinessInit.kernel.Get<IManagerFactory<Party, IPartyManager>>(new ConstructorArgument("context", this)).Create(this);
                }
                return _manager;
            }
        }
    }
}
