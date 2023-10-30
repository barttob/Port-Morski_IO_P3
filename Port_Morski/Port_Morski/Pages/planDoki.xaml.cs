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
    /// Interaction logic for planDoki.xaml
    /// </summary>
    public partial class planDoki : UserControl
    {

        private SeaPortContext context = new SeaPortContext();
        public planDoki()
        {
            InitializeComponent();
            LoadData();
        }

        internal void LoadData()
        {
            var shipSchedule = context.ShipSchedules
        .Include(schedule => schedule.Ship) 
        .Include(schedule => schedule.Dock)  
        .ToList();
            datagridPorty.ItemsSource = shipSchedule;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Models.ShipSchedule shipSchedule)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.ShipSchedules.Remove(shipSchedule);
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
                ShipSchedule? shipSchedule = modifyButton.Tag as ShipSchedule;

                if (shipSchedule != null)
                {
                    // Show the user data in the modifyUser component.
                    planDokiModify.Visibility = Visibility.Visible;

                    // Set the DataContext of modifyUser to the user object.
                    planDokiModify.DataContext = shipSchedule;
                }
            }
        }
        private void DODAJ_Click(object sender, RoutedEventArgs e)
        {
            planDokiAdd.Visibility = Visibility.Visible;
        }
    }

}
