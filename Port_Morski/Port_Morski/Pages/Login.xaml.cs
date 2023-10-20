using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Port_Morski.Models;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Port_Morski.Pages;

namespace Port_Morski.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredLogin = LoginTextBox.Text;
            string enteredPassword = PasswordBox.Password;

            // Tutaj możesz przeprowadzić logikę uwierzytelniania
            // Sprawdź, czy login i hasło są poprawne
            if (IsLoginValid(enteredLogin) && IsPasswordValid(enteredPassword))
            {
                // Logowanie udane
                LoginStatus.Text = "Logowanie udane!";
                // Tutaj można nawigować do głównego interfejsu użytkownika lub innej strony
                // Przykład nawigacji do innej strony:
                // this.NavigationService.Navigate(new MainPage());
            }
            else
            {
                // Logowanie nieudane
                LoginStatus.Text = "Logowanie nieudane. Spróbuj ponownie.";
            }
        }

        private bool IsLoginValid(string login)
        {
            // Tutaj możesz dodać logikę walidacji loginu
            // Sprawdź, czy login istnieje w bazie danych lub innych źródłach
            // Zwróć true, jeśli login jest poprawny, w przeciwnym razie false
            // Przykład: Sprawdzanie w bazie danych za pomocą Entity Framework
            using (var context = new SeaPortContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Login == login);
                return user != null;
            }
        }

        private bool IsPasswordValid(string password)
        {
            // Tutaj możesz dodać logikę walidacji hasła
            // Sprawdź, czy hasło pasuje do danego loginu w bazie danych lub innych źródłach
            // Zwróć true, jeśli hasło jest poprawne, w przeciwnym razie false
            // Przykład: Sprawdzanie w bazie danych za pomocą Entity Framework
            using (var context = new SeaPortContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Login == LoginTextBox.Text);
                return user != null && user.Password == password;
            }
        }
    }
}
