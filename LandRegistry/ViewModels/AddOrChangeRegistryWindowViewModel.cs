namespace LandRegistry.ViewModels
{
    using LandRegistry.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public class AddOrChangeRegistryWindowViewModel : INotifyPropertyChanged, IDataErrorInfo
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

        public string this[string columnName]
        {
            get
            {
                Error = string.Empty;
                switch (columnName)
                {
                    case "CadNum":
                        bool successCadNum = int.TryParse(CadNum.ToString(), out _);
                        if (successCadNum)
                        {
                            if (CadNum.ToString().Length > 6 || CadNum.ToString().Length < 1)
                            {
                                Error = "Кадастровый номер должен быть больше 0 и меньше 1000000.";
                            }
                        }
                        else
                        {
                            Error = " ";
                        }
                        break;

                    case "Address":
                        string addressPattern = @"ул\.\s[\D]{2,30},\s(?<дом>дом\s?[\d]{1,3}$)";
                        if (Address != null)
                        {
                            if (Address.Length < 80)
                            {
                                if (!Regex.IsMatch(Address, addressPattern, RegexOptions.IgnoreCase))
                                {
                                    Error = "Неверный формат адреса.";
                                }
                            }
                            else
                            {
                                Error = "Недопустимая длина!";
                            }
                        }
                        else
                        {
                            Error = " ";
                        }

                        break;

                    case "Area":
                        bool successArea = int.TryParse(Area.ToString(), out _);
                        if (successArea)
                        {
                            if (Area < 1 || Area > 1000000)
                            {
                                Error = "Недопустимая площадь!";
                            }
                        }
                        else
                        {
                            Error = " ";
                        }
                        break;

                    case "Price":
                        bool successPrice = int.TryParse(Price.ToString(), out _);
                        if (successPrice)
                        {
                            if (Price < 1 || Price > 1000000000)
                            {
                                Error = "Недопустимая цена!";
                            }
                        }
                        else
                        {
                            Error = " ";
                        }
                        break;

                }
                return Error;
            }
        }

        public string Error
        {
            get; set;
        }

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
