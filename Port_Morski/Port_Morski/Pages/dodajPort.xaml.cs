using Port_Morski.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.ComponentModel;
using System.Globalization;


namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy dodajPort.xaml
    /// </summary>
    public partial class dodajPort : UserControl
    {
        private ObservableCollection<Magazine> magazynyCollection;
        private ObservableCollection<Terminal> terminaleCollection;

        public dodajPort()
        {
            InitializeComponent();
            Btn_Exit.Click += Btn_Exit_Click;
           
            magazynyCollection = new ObservableCollection<Magazine>();
            datagridMagazyny.ItemsSource = magazynyCollection;

            terminaleCollection = new ObservableCollection<Terminal>();
            datagridTerminale.ItemsSource = terminaleCollection;
            
        }


       




        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {

            this.Visibility = Visibility.Collapsed;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new SeaPortContext())
            {
                
                if (string.IsNullOrWhiteSpace(Nazwa.Text))
                {
                    MessageBox.Show("Wprowadź nazwę portu.");
                    return;
                }

                
                if (datagridMagazyny.Items.Cast<Magazine>().Any(m => string.IsNullOrWhiteSpace(m.Name) || m.Area == 0 || m.AvailableCapacity == 0 || string.IsNullOrWhiteSpace(m.Specification)))
                {
                    MessageBox.Show("Wypełnij wszystkie komórki w datagridMagazyny.");
                    return;
                }

                
                if (datagridTerminale.Items.Cast<Terminal>().Any(t => string.IsNullOrWhiteSpace(t.Name) || string.IsNullOrWhiteSpace(t.Type) || t.MaxCapacity == 0 || t.AvailableFromDate == null))
                {
                    MessageBox.Show("Wypełnij wszystkie komórki w datagridTerminale.");
                    return;
                }

                
                var newDock = new Dock
                {
                    Name = Nazwa.Text,
                };

                
                context.Docks.Add(newDock);
                context.SaveChanges();

                int dockId = newDock.Id;

                foreach (var magazine in magazynyCollection)
                {
                    magazine.DockId = dockId;
                    context.Magazines.Add(magazine);
                }

                foreach (var terminal in terminaleCollection)
                {
                    terminal.DockId = dockId;
                    context.Terminals.Add(terminal);
                }

                context.SaveChanges();

                MessageBox.Show("Pomyślnie dodano nowego użytkownika do bazy danych.");

                admPorty adm = new admPorty();
                adm.LoadData();

                this.Visibility = Visibility.Collapsed;
            }
        }
        
        private void add_Row_Magazyny(object sender, RoutedEventArgs e)
        {
            
            magazynyCollection.Add(new Magazine
            {
                Name = "Nowy magazyn",
                Area = 0,
                AvailableCapacity = 0,
                Specification = "Magazyn ogolny",
                
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (datagridMagazyny.SelectedItem is Magazine selectedMagazine)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    magazynyCollection.Remove(selectedMagazine);
                }
            }

        }

        private void add_Terminal(object sender, RoutedEventArgs e)
        {
            terminaleCollection.Add(new Terminal
            {
                Name = "Nowy terminal",
                Type = "Terminal kontenerowy",
                MaxCapacity = 0,
                Available = true,
                AvailableFromDate = DateTime.Now,
            });
        }

        private void Delete_Terminal_Click(object sender, RoutedEventArgs e)
        {
            if (datagridTerminale.SelectedItem is Terminal selectedTerminal)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    terminaleCollection.Remove(selectedTerminal);
                }
            }
        }
    }
}
