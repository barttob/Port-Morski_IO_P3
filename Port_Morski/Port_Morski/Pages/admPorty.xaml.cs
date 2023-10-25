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
using Dock = Port_Morski.Models.Dock;

namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy admPorty.xaml
    /// </summary>
    public partial class admPorty : UserControl
    {
        private SeaPortContext context = new SeaPortContext();
        public admPorty()
        {

            InitializeComponent();
            LoadData();
        }
        internal void LoadData()
        {
            var docks = context.Docks.ToList();
            datagridPorty.ItemsSource = docks;
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Models.Dock docks)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Docks.Remove(docks);
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
                Dock? dock = modifyButton.Tag as Dock;

                if (dock != null)
                {
                    // Show the user data in the modifyUser component.
                    modyfikujPort.Visibility = Visibility.Visible;

                    // Set the DataContext of modifyUser to the user object.
                    modyfikujPort.DataContext = dock;
                }
            }
        }
        private void DODAJ_Click(object sender, RoutedEventArgs e)
        {
            dodajPort.Visibility = Visibility.Visible;
        }
    }
}
