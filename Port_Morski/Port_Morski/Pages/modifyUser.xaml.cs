using Microsoft.Data.SqlClient;
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
using static Port_Morski.Pages.admUzytkownicy;

namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy modifyUser.xaml
    /// </summary>
    public partial class modifyUser : UserControl
    {
        public modifyUser()
        {
            InitializeComponent();
            Btn_Exit.Click += Btn_Exit_Click;

            

        }

        private void modifyAdd_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new SeaPortContext())
            {
                try
                {
                    // Find the user by their login
                    var user = context.Users.SingleOrDefault(u => u.Id == int.Parse(Id.Text));

                    if (user == null)
                    {
                        MessageBox.Show("Użytkownik o podanym id nie istnieje.");
                        return;
                    }

                    user.Name = Imie.Text;
                    user.LastName = Nazwisko.Text;
                    user.Login = Login.Text;
                    if (!string.IsNullOrEmpty(haslo.Password))
                    {
                        user.Password = ConvertToUnsecureString(haslo.SecurePassword);
                    }
                    if (rola.SelectedItem != null)
                    {
                        user.UserRole = rola.Text;
                    }


                    context.SaveChanges();

                    MessageBox.Show("Pomyślnie zaktualizowano użytkownika w bazie danych.");
                    this.Visibility = Visibility.Collapsed;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas aktualizacji użytkownika w bazie danych: {ex.Message}");
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

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

        }
        
    }
}

