using LandRegistry.ViewModels;
using System.Windows;

namespace LandRegistry.Views
{
    public partial class RegistryWindow : Window
    {
        public RegistryWindow()
        {
            InitializeComponent();
            DataContext = new RegistryViewModel();
        }
    }
}
