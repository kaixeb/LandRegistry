using LandRegistry.ViewModels;
using System.Windows;

namespace LandRegistry.Views
{
    public partial class AddOrChangeRegistryWindow : Window
    {
        public AddOrChangeRegistryWindowViewModel AOCRWVM = new AddOrChangeRegistryWindowViewModel();
        public AddOrChangeRegistryWindow()
        {
            InitializeComponent();
            DataContext = AOCRWVM;
            AOCRWVM.Owner = new Models.Owner();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AOCRWVM.Error) 
                && string.IsNullOrEmpty(AOCRWVM.Owner.Error)
                && AOCRWVM.SelectedCadEng != null
                && AOCRWVM.SelectedDistrict != null
                && AOCRWVM.SelectedServiceUnit != null
                && AOCRWVM.SelectedSettlement != null
                && AOCRWVM.SelectedUsePurpose != null)
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Имеются некорректные данные!");
            }
        }

    }
}
