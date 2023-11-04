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
    /// Interaction logic for planOperacje.xaml
    /// </summary>
    public partial class planOperacje : UserControl
    {
        private SeaPortContext context = new SeaPortContext();
        public planOperacje()
        {
            InitializeComponent();
        }

        internal void LoadData()
        {
            var Operations = context.Operationss
        .Include(operation => operation.Ship)
        .Include(operation => operation.Dock)
        .ToList();
            datagridPorty.ItemsSource = Operations;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Models.Operations Operations)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Operationss.Remove(Operations);
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
                Operations? Operations = modifyButton.Tag as Operations;

                if (Operations != null)
                {
                    // Show the user data in the modifyUser component.
                    planOperacjeModify.Visibility = Visibility.Visible;

                    // Set the DataContext of modifyUser to the user object.
                    planOperacjeModify.DataContext = Operations;
                }
            }
        }
        private void DODAJ_Click(object sender, RoutedEventArgs e)
        {
            planOperacjeAdd.Visibility = Visibility.Visible;
        }
    }
}
