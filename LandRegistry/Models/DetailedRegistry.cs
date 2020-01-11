using System;

namespace LandRegistry.Models
{
    public class DetailedRegistry
    {
        public int CadNum { get; set; }
        public string Address { get; set; }
        public int Area { get; set; }
        public int Price { get; set; }                
        public string DistrictInfo { get; set; }
        public string SettlementInfo { get; set; }
        public string UsePurposeInfo { get; set; }
        public string OwnerInfo { get; set; }
        public string ServiceUnitInfo { get; set; }
        public string CadEngInfo { get; set; }
        public DateTime UpdTime { get; set; }
        public Registry Registry { get; set; }
    }
}
