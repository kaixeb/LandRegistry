namespace LandRegistry.ViewModels
{
    using LandRegistry.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class AddOrChangeRegistryWindowViewModel : INotifyPropertyChanged
    {
        public AddOrChangeRegistryWindowViewModel()
        {
            using (landregistrydbContext lrdb = new landregistrydbContext())
            {
                NumberedDistrictList = lrdb.District.ToList();
                var districts = NumberedDistrictList.Select(d => d.Name);
                DistrictList = new ObservableCollection<string>(districts);

                NumberedSettlementList = lrdb.Settlement.ToList();
                var settlements = NumberedSettlementList.Select(s => s.Name);
                SettlementList = new ObservableCollection<string>(settlements);

                NumberedUsePurposeList = lrdb.UsePurpose.ToList();
                var usePurposes = NumberedUsePurposeList.Select(u => u.Purpose);
                UsePurposeList = new ObservableCollection<string>(usePurposes);

                NumberedServiceUnitList = lrdb.ServiceUnit.ToList();
                var serviceUnits = NumberedServiceUnitList.Select(s => s.Name);
                ServiceUnitList = new ObservableCollection<string>(serviceUnits);

                NumberedCadEngList = lrdb.CadEng.ToList();
                var cadEngs = NumberedCadEngList.Select(c => c.Surname);
                CadEngList = new ObservableCollection<string>(cadEngs);
            }
        }

        public int? CadNum { get; set; }

        public string Address { get; set; }

        public int? Area { get; set; }

        public int? Price { get; set; }

        public Owner Owner { get; set; }
        
        private string selectedDistrict;

        private string selectedSettlement;

        private string selectedUsePurpose;

        private string selectedServiceUnit;

        private string selectedCadEng;

        public string SelectedCadEng
        {
            get => selectedCadEng; set { selectedCadEng = value; OnPropertyChanged("SelectedCadEng"); }
        }

        public string SelectedServiceUnit
        {
            get => selectedServiceUnit; set { selectedServiceUnit = value; OnPropertyChanged("SelectedServiceUnit"); }
        }

        public string SelectedUsePurpose
        {
            get => selectedUsePurpose; set { selectedUsePurpose = value; OnPropertyChanged("SelectedCUsePurpose"); }
        }

        public string SelectedSettlement
        {
            get => selectedSettlement; set { selectedSettlement = value; OnPropertyChanged("SelectedSettlement"); }
        }

        public string SelectedDistrict
        {
            get => selectedDistrict; set { selectedDistrict = value; OnPropertyChanged("SelectedDistrict"); }
        }

        public ObservableCollection<string> DistrictList { get; set; }

        public ObservableCollection<string> SettlementList { get; set; }

        public ObservableCollection<string> UsePurposeList { get; set; }

        public ObservableCollection<string> ServiceUnitList { get; set; }

        public ObservableCollection<string> CadEngList { get; set; }

        public List<District> NumberedDistrictList;

        public List<Settlement> NumberedSettlementList;

        public List<UsePurpose> NumberedUsePurposeList;

        public List<ServiceUnit> NumberedServiceUnitList;

        public List<CadEng> NumberedCadEngList;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //обновляет свойства объекта, который его вызвал
        }
    }
}
