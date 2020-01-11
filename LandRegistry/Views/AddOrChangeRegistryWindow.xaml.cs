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
            DialogResult = true;
        }

    }
}
