using LandRegistry.Models;
using System.Windows;
using System.Windows.Documents;

namespace LandRegistry.Views
{    
    public partial class ExtractDetailedRegistryWindow : Window
    {
        public ExtractDetailedRegistryWindow(DetailedRegistry detailedRegistry)
        {
            InitializeComponent();
            ((Run)CadNum.Inlines.FirstInline).Text += detailedRegistry.CadNum.ToString();
            ((Run)Address.Inlines.FirstInline).Text += detailedRegistry.Address;
            ((Run)Area.Inlines.FirstInline).Text += detailedRegistry.Area.ToString();
            ((Run)Price.Inlines.FirstInline).Text += detailedRegistry.Price.ToString();
            ((Run)DistrictInfo.Inlines.FirstInline).Text += detailedRegistry.DistrictInfo;
            ((Run)SettlementInfo.Inlines.FirstInline).Text += detailedRegistry.SettlementInfo;
            ((Run)UsePurposeInfo.Inlines.FirstInline).Text += detailedRegistry.UsePurposeInfo;
            ((Run)OwnerInfo.Inlines.FirstInline).Text += "\n" + detailedRegistry.OwnerInfo;
            ((Run)ServiceUnitInfo.Inlines.FirstInline).Text += "\n" + detailedRegistry.ServiceUnitInfo;
            ((Run)CadEngInfo.Inlines.FirstInline).Text += "\n" + detailedRegistry.CadEngInfo;
            ((Run)UpdTime.Inlines.FirstInline).Text += "\n" + detailedRegistry.UpdTime.ToString();
        }
    }
}
