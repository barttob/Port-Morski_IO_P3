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
    /// Logika interakcji dla klasy admUzytkownicy.xaml
    /// </summary>
    public partial class admUzytkownicy : UserControl
    {
        private SeaPortContext context = new SeaPortContext(); 
        public admUzytkownicy()
        {
            InitializeComponent();
            LoadData();
        }

       

        private void LoadData()
        {
            var users = context.Users.ToList(); 
            datagridUsers.ItemsSource = users; 
        }
        
        private void SaveChanges()
        {
            context.SaveChanges(); 
            
        }


       

        private void datagridUsers_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
        }

        private void datagridUsers_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is User user)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Users.Remove(user); 
                    context.SaveChanges(); 
                    LoadData();                       
                }
            }
            
        }

        private void DODAJ_Click(object sender, RoutedEventArgs e)
        {
            AddUser.Visibility = Visibility.Visible;
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            modifyUser.Visibility = Visibility.Visible;

        }
    }
}
