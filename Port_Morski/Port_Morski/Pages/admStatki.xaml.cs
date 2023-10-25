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
    /// Logika interakcji dla klasy admStatki.xaml
    /// </summary>
    public partial class admStatki : UserControl
    {
        private SeaPortContext context = new SeaPortContext();

        public admStatki()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            var ships = context.Ships.ToList();
            datagridStatki.ItemsSource = ships;
        }
        internal void Refresh()
        {
            context.SaveChanges();
            var ship = context.Ships.ToList();
            datagridStatki.ItemsSource = ship; ;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Ship ship)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Ships.Remove(ship);
                    context.SaveChanges();
                    LoadData();
                }
            }

        }
        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            Button? modifyButton = sender as Button;
            if (modifyButton != null)
            {
                // Retrieve the user object associated with the clicked button.
                Ship? ship = modifyButton.Tag as Ship;

                if (ship != null)
                {
                    // Show the user data in the modifyUser component.
                    modyfikujStatek.Visibility = Visibility.Visible;

                    // Set the DataContext of modifyUser to the user object.
                    modyfikujStatek.DataContext = ship;
                }
            }
        }
        private void DODAJ_Click(object sender, RoutedEventArgs e)
        {
            dodajStatek.Visibility = Visibility.Visible;
        }

    }
}
