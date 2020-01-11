using System;

namespace LandRegistry.Models
{
    public partial class Registry
    {
        public int CadNum { get; set; }
        public string Address { get; set; }
        public int Area { get; set; }
        public int Price { get; set; }
        public DateTime UpdTime { get; set; }
        public int CeId { get; set; }
        public int DisId { get; set; }
        public int OwnId { get; set; }
        public int SuId { get; set; }
        public int SettlId { get; set; }
        public int UpId { get; set; }

        public virtual CadEng Ce { get; set; }
        public virtual District Dis { get; set; }
        public virtual Owner Own { get; set; }
        public virtual Settlement Settl { get; set; }
        public virtual ServiceUnit Su { get; set; }
        public virtual UsePurpose Up { get; set; }
    }
}
