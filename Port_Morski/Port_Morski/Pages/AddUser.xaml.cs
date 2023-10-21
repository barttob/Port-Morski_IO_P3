using Port_Morski.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
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

namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy AddUser.xaml
    /// </summary>
    public partial class AddUser : UserControl
    {
        
        public AddUser()
        {
            InitializeComponent();
            Btn_Exit.Click += Btn_Exit_Click;
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            
            this.Visibility = Visibility.Collapsed;
        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Imie.Text) || string.IsNullOrWhiteSpace(Nazwisko.Text) || string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(ConvertToUnsecureString(haslo.SecurePassword)))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.");
                return;
            }

            using (var context = new SeaPortContext())
            {
                try
                {
                    var newUser = new User
                    {
                        Name = Imie.Text,
                        LastName = Nazwisko.Text,
                        UserRole = rola.Text,
                        Login = Login.Text,
                        Password = ConvertToUnsecureString(haslo.SecurePassword)
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    MessageBox.Show("Pomyślnie dodano nowego użytkownika do bazy danych.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas dodawania użytkownika do bazy danych: {ex.Message}");
                }
            }
        }
        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

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
