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

namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Administracja.xaml
    /// </summary>
    public partial class Administracja : UserControl
    {
        public Administracja()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            using (var context = new SeaPortContext())
            {
                var users = context.Users.Select(u => new { u.Name, u.LastName, u.UserRole }).ToList();
                datagridUzytkownicyFront.ItemsSource = users;

                var ships = context.Ships.ToList();
                datagridStatkiFront.ItemsSource = ships;

                var docks = context.Docks.ToList();
                datagridPortyFront.ItemsSource = docks;
            }
        }

        private void wiecejUzytkownicy_Click(object sender, RoutedEventArgs e)
        {
            admUzytkownicy.Visibility = Visibility.Visible;
        }

        private void wiecejStatki_Click(object sender, RoutedEventArgs e)
        {
            admStatki.Visibility = Visibility.Visible;
        }

        private void wiecejPorty_Click(object sender, RoutedEventArgs e)
        {
            admPorty.Visibility = Visibility.Visible;
           
        }
    }
}
