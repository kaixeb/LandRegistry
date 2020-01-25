using LandRegistry.Models;
using LandRegistry.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static LandRegistry.CurrentUser;

namespace LandRegistry
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void GuestEntrance_Click(object sender, MouseButtonEventArgs e)
        {
            CurUser = User.Guest;
            RegistryWindow registryWindow = new RegistryWindow();
            registryWindow.Show();
            Close();
        }

        private void AdminEntrance_Click(object sender, RoutedEventArgs e)
        {
            CurUser = User.Admin;

            if (AdminAuthentication(AdminPass.Password, 1))
            {
                RegistryWindow registryWindow = new RegistryWindow();
                registryWindow.Show();
                Close();
            }
            else
            {
                AdminPass.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private bool AdminAuthentication(string adminPassword, int adminId)
        {
            Users admin = GetUserByPassAndId(adminPassword, adminId);
            return admin != null;
        }

        private Users GetUserByPassAndId(string password, int id)
        {
            Users reqUser = null;
            using (landregistrydbContext lrdb = new landregistrydbContext())
            {
                var users = lrdb.Users.ToList();
                foreach (var user in users)
                {
                    if (user.Password == password && user.UserId == id)
                    {
                        reqUser = user;
                    }
                }
            }
            return reqUser;
        }
    }
}