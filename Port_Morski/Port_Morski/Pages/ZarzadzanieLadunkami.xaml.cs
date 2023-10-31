using Microsoft.EntityFrameworkCore;
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
    /// Logika interakcji dla klasy ZarzadzanieLadunkami.xaml
    /// </summary>
    public partial class ZarzadzanieLadunkami : UserControl
    {
        private SeaPortContext context = new SeaPortContext();
        public ZarzadzanieLadunkami()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            var cargo = context.Cargos.Include("Ship").ToList(); // Łączenie danych z tabeli Ships
            datagrid.ItemsSource = cargo;
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Cargo cargo)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Cargos.Remove(cargo);
                    context.SaveChanges();
                    LoadData();
                }
            }

        }
        private void DODAJ_Click(object sender, RoutedEventArgs e)
        {
            dodajLadunek.Visibility = Visibility.Visible;
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            Button? modifyButton = sender as Button;
            if (modifyButton != null)
            {
                // Retrieve the user object associated with the clicked button.
                Cargo? cargo = modifyButton.Tag as Cargo;

                if (cargo != null)
                {
                    // Show the user data in the modifyUser component.
                     modyfikujLadunek.Visibility = Visibility.Visible;

                    // Set the DataContext of modifyUser to the user object.
                    modyfikujLadunek.DataContext = cargo;
                }
            }
        }
    }
}
