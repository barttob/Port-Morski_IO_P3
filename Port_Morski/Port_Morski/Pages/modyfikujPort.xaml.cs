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
    /// Logika interakcji dla klasy modyfikujPort.xaml
    /// </summary>
    public partial class modyfikujPort : UserControl
    {
        public modyfikujPort()
        {
            InitializeComponent();
            Btn_Exit.Click += Btn_Exit_Click;
           // LoadMagazinesData();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {

            this.Visibility = Visibility.Collapsed;
        }
        private void LoadMagazinesData()
        {
            using (SeaPortContext context = new SeaPortContext())
            {
                // Pobierz Id z TextBoxa
                var dockId = Id.Text;
                MessageBox.Show($"Wartość dockId: {dockId}");
                // Wykonaj zapytanie do bazy danych
                //List<Magazine> magazines = context.Magazines
                   // .Where(m => m.DockId == dockId) // Zakładam, że istnieje pole DockId w tabeli Magazines
                  //  .ToList();

                // Przypisz dane do źródła danych dla DataGrid
               // datagridMagazyny.ItemsSource = magazines;
            }
        }
        private void modify_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new SeaPortContext())
            {
                try
                {
                    // Find the user by their login
                    var dock = context.Docks.SingleOrDefault(s => s.Id == int.Parse(Id.Text));

                    if (dock == null)
                    {
                        MessageBox.Show("Port o podanym id nie istnieje.");
                        return;
                    }

                    dock.Name = Nazwa.Text;
                    


                    context.SaveChanges();


                    MessageBox.Show("Pomyślnie zaktualizowano port w bazie danych.");
                    this.Visibility = Visibility.Collapsed;
                    admPorty adm = new admPorty();
                    adm.LoadData();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas aktualizacji użytkownika w bazie danych: {ex.Message}");
                }
            }
        }

        private void add_Row_Magazyny(object sender, RoutedEventArgs e)
        {

        }

        private void add_Row_Terminal(object sender, RoutedEventArgs e)
        {

        }

        private void Del_Mag_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Del_Term_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
