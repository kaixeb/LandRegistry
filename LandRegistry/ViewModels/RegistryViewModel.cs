namespace LandRegistry.ViewModels
{
    using LandRegistry.Models;
    using LandRegistry.Views;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media.Imaging;
    using static LandRegistry.CurrentUser;
    using Application = System.Windows.Application;

    public class RegistryViewModel : INotifyPropertyChanged
    {
        public RegistryViewModel()
        {
            SetCurUser();
            FillRegListWithRecords();
        }

        private RelayCommand addCommand;

        private RelayCommand changeCommand;

        private RelayCommand deleteCommand;

        private RelayCommand extractCommand;

        private DetailedRegistry selectedRegistry;

        public DetailedRegistry SelectedRegistry
        {
            get { return selectedRegistry; }
            set
            {
                selectedRegistry = value;
                OnPropertyChanged("SelectedRegistry");
            }
        }

        private ObservableCollection<DetailedRegistry> detailedRegistries;

        public ObservableCollection<DetailedRegistry> DetailedRegistryList
        {
            get { return detailedRegistries; }
            set
            {
                detailedRegistries = value;
                OnPropertyChanged("DetailedRegistryList");
            }
        }

        //Поиск
        private ObservableCollection<DetailedRegistry> nonFilteredDetailedRegistryList;

        private string cadNumSearchText;
        public string CadNumSearchText
        {
            get { return cadNumSearchText; }
            set
            {
                cadNumSearchText = value;
                OnPropertyChanged("CadNumSearchText");
                FilterItemsByCadNum();
            }
        }

        private string districtSearchText;

        public string DistrictSearchText
        {
            get => districtSearchText;
            set
            {
                districtSearchText = value;
                OnPropertyChanged("DistrictSearchText");
                FilterItemsByDistrict();
            }
        }

        private string settlementSearchText;
        public string SettlementSearchText
        {
            get => settlementSearchText;
            set
            {
                settlementSearchText = value;
                OnPropertyChanged("SettlementSearchText");
                FilterItemsBySettlement();
            }
        }

        private void FilterItemsByCadNum()
        {
            if (!string.IsNullOrEmpty(CadNumSearchText))
            {
                if (int.TryParse(CadNumSearchText, out _))
                {
                    DetailedRegistryList = new ObservableCollection<DetailedRegistry>
                        (
                        from item in nonFilteredDetailedRegistryList where item.CadNum == Convert.ToInt32(CadNumSearchText) select item
                        );
                }
            }
            else
            {
                DetailedRegistryList = nonFilteredDetailedRegistryList;
            }
        }

        private void FilterItemsByDistrict()
        {
            if (!string.IsNullOrEmpty(DistrictSearchText))
            {
                DetailedRegistryList = new ObservableCollection<DetailedRegistry>
                    (
                    from item in nonFilteredDetailedRegistryList where item.DistrictInfo.Contains(DistrictSearchText) select item
                    );
            }
            else
            {
                DetailedRegistryList = nonFilteredDetailedRegistryList;
            }
        }

        private void FilterItemsBySettlement()
        {
            if (!string.IsNullOrEmpty(SettlementSearchText))
            {
                DetailedRegistryList = new ObservableCollection<DetailedRegistry>
                    (
                    from item in nonFilteredDetailedRegistryList where item.SettlementInfo.Contains(SettlementSearchText) select item
                    );
            }
            else
            {
                DetailedRegistryList = nonFilteredDetailedRegistryList;
            }
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                      {
                          AddOrChangeRegistryWindow AOCRWindow = new AddOrChangeRegistryWindow();
                          if (AOCRWindow.ShowDialog() == true)
                          {
                              Registry registry = new Registry();
                              DetailedRegistry detailedRegistry = new DetailedRegistry();

                              //Меняем заголовок окна и иконку
                              AOCRWindow.Title = "Добавление";
                              Uri iconUri = new Uri("pack://application:,,,/Views/playlist-plus.png", UriKind.RelativeOrAbsolute);
                              AOCRWindow.Icon = BitmapFrame.Create(iconUri);

                              AddNewDetailedRegistry(detailedRegistry, registry, AOCRWindow);
                          }
                      }));
            }
        }

        public RelayCommand ChangeCommand
        {
            get
            {
                return changeCommand ??
                    (changeCommand = new RelayCommand(obj =>
                      {
                          DetailedRegistry detailedRegistry = obj as DetailedRegistry;
                          AddOrChangeRegistryWindow AOCRWindow = new AddOrChangeRegistryWindow();

                          //Занесение данных из соответствующего объекта Registry у detailedRegistry в форму  
                          if (detailedRegistry != null)
                          {
                              AOCRWindow.AOCRWVM.CadNum = detailedRegistry.CadNum;
                              AOCRWindow.AOCRWVM.Address = detailedRegistry.Address;
                              AOCRWindow.AOCRWVM.Area = detailedRegistry.Area;
                              AOCRWindow.AOCRWVM.Price = detailedRegistry.Price;
                              AOCRWindow.AOCRWVM.SelectedDistrict = AOCRWindow.AOCRWVM.NumberedDistrictList.Find(d => d.DisId == detailedRegistry.Registry.DisId).Name;
                              AOCRWindow.AOCRWVM.SelectedSettlement = AOCRWindow.AOCRWVM.NumberedSettlementList.Find(s => s.SettlId == detailedRegistry.Registry.SettlId).Name;
                              AOCRWindow.AOCRWVM.SelectedUsePurpose = AOCRWindow.AOCRWVM.NumberedUsePurposeList.Find(up => up.UpId == detailedRegistry.Registry.UpId).Purpose;

                              using (landregistrydbContext lrdb = new landregistrydbContext())
                              {
                                  AOCRWindow.AOCRWVM.Owner = lrdb.Owner.Find(detailedRegistry.Registry.OwnId);
                              }

                              if (AOCRWindow.AOCRWVM.Owner.Patronymic == "-")
                                  AOCRWindow.AOCRWVM.Owner.Patronymic = string.Empty;

                              if (AOCRWindow.AOCRWVM.Owner.Email == "-")
                                  AOCRWindow.AOCRWVM.Owner.Email = string.Empty;

                              AOCRWindow.AOCRWVM.SelectedServiceUnit = AOCRWindow.AOCRWVM.NumberedServiceUnitList.Find(su => su.SuId == detailedRegistry.Registry.SuId).Name;
                              AOCRWindow.AOCRWVM.SelectedCadEng = AOCRWindow.AOCRWVM.NumberedCadEngList.Find(ce => ce.CeId == detailedRegistry.Registry.CeId).Surname;

                              //Меняем заголовок окна и иконку
                              AOCRWindow.Title = "Изменение";
                              Uri iconUri = new Uri("pack://application:,,,/Views/playlist-edit.png", UriKind.RelativeOrAbsolute);
                              AOCRWindow.Icon = BitmapFrame.Create(iconUri);

                              if (AOCRWindow.ShowDialog() == true) // Если диалог успешен, то добавляем измененную запись
                              {
                                  using (landregistrydbContext lrdb = new landregistrydbContext()) //удаляем старую запись
                                  {
                                      lrdb.Registry.Remove(detailedRegistry.Registry);
                                      lrdb.SaveChanges();
                                  }
                                  DetailedRegistryList = nonFilteredDetailedRegistryList;
                                  DetailedRegistryList.Remove(detailedRegistry); // убираем из списка
                                  nonFilteredDetailedRegistryList = DetailedRegistryList;
                                  Registry registry = new Registry(); //новая запись для бд

                                  AddNewDetailedRegistry(detailedRegistry, registry, AOCRWindow); //добавляем новую запись в бд и в список
                              }
                          }
                      },
                      (obj) => DetailedRegistryList.Count > 0));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand(obj =>
                      {
                          DetailedRegistry detailedRegistry = obj as DetailedRegistry;
                          if (detailedRegistry != null)
                          {
                              CadNumSearchText = null;
                              DistrictSearchText = null;
                              SettlementSearchText = null;
                              DetailedRegistryList.Remove(detailedRegistry);
                              nonFilteredDetailedRegistryList = DetailedRegistryList;
                              using (landregistrydbContext lrdb = new landregistrydbContext())
                              {
                                  lrdb.Registry.Remove(detailedRegistry.Registry); //удаление из базы данных соответствующего объекта registry                                  
                                  lrdb.SaveChanges();
                              }
                          }
                      },
                      (obj) => DetailedRegistryList.Count > 0));
            }
        }

        public RelayCommand ExtractCommand
        {
            get
            {
                return extractCommand ??
                    (extractCommand = new RelayCommand(obj =>
                      {
                          DetailedRegistry detailedRegistry = obj as DetailedRegistry;
                          if (detailedRegistry != null)
                          {
                              ExtractDetailedRegistryWindow EDRW = new ExtractDetailedRegistryWindow(detailedRegistry);
                              EDRW.Show();
                          }
                      },
                    (obj) => DetailedRegistryList.Count > 0));
            }
        }

        private void AddNewDetailedRegistry(DetailedRegistry detailedRegistry, Registry registry, AddOrChangeRegistryWindow AOCRWindow)
        {
            //заполняем registry и detailedRegistry
            detailedRegistry.CadNum = registry.CadNum = AOCRWindow.AOCRWVM.CadNum ?? 0;
            detailedRegistry.Address = registry.Address = AOCRWindow.AOCRWVM.Address;
            detailedRegistry.Area = registry.Area = AOCRWindow.AOCRWVM.Area ?? 0;
            detailedRegistry.Price = registry.Price = AOCRWindow.AOCRWVM.Price ?? 0;

            Owner owner = AOCRWindow.AOCRWVM.Owner;

            if (owner.Patronymic == string.Empty)
            {
                owner.Patronymic = "-";
            }

            if (owner.Email == string.Empty)
            {
                owner.Email = "-";
            }

            foreach (District d in AOCRWindow.AOCRWVM.NumberedDistrictList)
            {
                if (AOCRWindow.AOCRWVM.SelectedDistrict == d.Name)
                {
                    registry.DisId = d.DisId;
                    detailedRegistry.DistrictInfo = d.Name;
                }
            }

            foreach (Settlement s in AOCRWindow.AOCRWVM.NumberedSettlementList)
            {
                if (AOCRWindow.AOCRWVM.SelectedSettlement == s.Name)
                {
                    registry.SettlId = s.SettlId;
                    detailedRegistry.SettlementInfo = s.Name;
                }
            }

            foreach (UsePurpose up in AOCRWindow.AOCRWVM.NumberedUsePurposeList)
            {
                if (AOCRWindow.AOCRWVM.SelectedUsePurpose == up.Purpose)
                {
                    registry.UpId = up.UpId;
                    detailedRegistry.UsePurposeInfo = up.Purpose;
                }
            }

            foreach (ServiceUnit su in AOCRWindow.AOCRWVM.NumberedServiceUnitList)
            {
                if (AOCRWindow.AOCRWVM.SelectedServiceUnit == su.Name)
                {
                    registry.SuId = su.SuId;
                    detailedRegistry.ServiceUnitInfo = su.Name
                    + "\n" + su.Address
                    + "\n" + su.StartTime.ToString()
                    + "-" + su.EndTime.ToString()
                    + "\n" + su.ConNum
                    + "\n" + su.ChiefFullName
                    + "\n" + su.Email;
                }
            }

            foreach (CadEng ce in AOCRWindow.AOCRWVM.NumberedCadEngList)
            {
                if (AOCRWindow.AOCRWVM.SelectedCadEng == ce.Surname)
                {
                    registry.CeId = ce.CeId;
                    detailedRegistry.CadEngInfo = ce.Surname
                    + " " + ce.Name
                    + " " + ce.Patronymic;
                }
            }

            using (landregistrydbContext lrdb = new landregistrydbContext())
            {
                var existOwners = (from existOwn in lrdb.Owner
                                   where existOwn.Inn == owner.Inn
                                   select existOwn).ToList();

                if (existOwners.Count == 0) // если новый пользователь
                {
                    owner.OwnId = lrdb.Owner.Count() + 1;
                    lrdb.Owner.Add(owner); //добавляем его
                    lrdb.SaveChanges();
                }
                else
                {
                    foreach (Owner existOwn in existOwners)
                    {
                        existOwn.Name = owner.Name;
                        existOwn.Surname = owner.Surname;
                        existOwn.Patronymic = owner.Patronymic;
                        existOwn.ConNum = owner.ConNum;
                        existOwn.Email = owner.Email;

                        lrdb.Owner.Update(existOwn);
                        lrdb.SaveChanges();
                    }
                }
            }

            registry.OwnId = owner.OwnId;
            registry.UpdTime = DateTime.Now;

            detailedRegistry.Registry = registry;
            detailedRegistry.UpdTime = registry.UpdTime;
            detailedRegistry.OwnerInfo = owner.Surname
                + " " + owner.Name
                + " " + owner.Patronymic
                + "\n" + owner.Inn
                + "\n" + owner.ConNum
                + "\n" + owner.Email;

            //добавляем в базу данных объект registry и в список объект detailedRegistry
            using (landregistrydbContext lrdb = new landregistrydbContext())
            {
                lrdb.Registry.Add(registry);
                lrdb.SaveChanges();
            }

            CadNumSearchText = null;
            DistrictSearchText = null;
            SettlementSearchText = null;
            DetailedRegistryList.Add(detailedRegistry);
            nonFilteredDetailedRegistryList = DetailedRegistryList;
            SelectedRegistry = detailedRegistry;
        }

        private void FillRegListWithRecords()
        {
            using (landregistrydbContext lrdb = new landregistrydbContext())
            {
                var records = from r in lrdb.Registry
                              join d in lrdb.District on r.DisId equals d.DisId
                              join s in lrdb.Settlement on r.SettlId equals s.SettlId
                              join u in lrdb.UsePurpose on r.UpId equals u.UpId
                              join o in lrdb.Owner on r.OwnId equals o.OwnId
                              join su in lrdb.ServiceUnit on r.SuId equals su.SuId
                              join ce in lrdb.CadEng on r.CeId equals ce.CeId
                              select new DetailedRegistry
                              {
                                  CadNum = r.CadNum,
                                  Address = r.Address,
                                  Area = r.Area,
                                  Price = r.Price,
                                  DistrictInfo = d.Name,
                                  SettlementInfo = s.Name,
                                  UsePurposeInfo = u.Purpose,
                                  OwnerInfo = o.Surname
                                  + " " + o.Name
                                  + " " + (o.Patronymic ?? "-")
                                  + "\n" + o.Inn
                                  + "\n" + o.ConNum
                                  + "\n" + (o.Email ?? "-"),
                                  ServiceUnitInfo = su.Name
                                  + "\n" + su.Address
                                  + "\n" + su.StartTime.ToString()
                                  + "-" + su.EndTime.ToString()
                                  + "\n" + su.ConNum
                                  + "\n" + su.ChiefFullName
                                  + "\n" + su.Email,
                                  CadEngInfo = ce.Surname
                                  + " " + ce.Name
                                  + " " + ce.Patronymic,
                                  UpdTime = r.UpdTime,
                                  Registry = r
                              };
                DetailedRegistryList = new ObservableCollection<DetailedRegistry>(records);
                nonFilteredDetailedRegistryList = DetailedRegistryList;
            }
        }

        private void SetCurUser()
        {
            Window RegistryForm = Application.Current.Windows[0] as Window;

            try
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(RegistryWindow))
                    {
                        if (CurUser == User.Admin)
                        {
                            RegistryWindow RegWin = window as RegistryWindow;
                            RegWin.curUserCaption.Text = "Администратор";
                            RegWin.AddBut.IsEnabled = true;
                            RegWin.ChangeBut.IsEnabled = true;
                            RegWin.DeleteBut.IsEnabled = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Current User Error");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
