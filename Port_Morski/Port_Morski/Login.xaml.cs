
using Port_Morski.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Port_Morski
{
    /// <summary>
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SeaPortContext db = new SeaPortContext();
        
        public Login()
        {
            InitializeComponent();
        }
        
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {

            string login = txtUsername.Text;
            SecureString securePassword = txtPassword.SecurePassword;
            string password = ConvertToUnsecureString(securePassword);

            using (var context = new SeaPortContext(new DbContextOptions<SeaPortContext>()))
            {
                var user = context.Users.FirstOrDefault(u => u.Login == login);

                if (user != null && user.Password == password)
                {
                    LoggedInUser.SetLoggedInUserId(user.Id);
                    MainWindow mainWindow = new MainWindow(user.UserRole);
                        mainWindow.Show();
                        this.Close();
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe dane logowania.");
                }
            }
        }
        public static class LoggedInUser
        {
            public static int UserId { get; private set; }

            public static void SetLoggedInUserId(int userId)
            {
                UserId = userId;
            }
        }

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
