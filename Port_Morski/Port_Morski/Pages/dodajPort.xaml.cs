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
                // Sprawdź czy textBox 'Nazwa' jest wpisany
                if (string.IsNullOrWhiteSpace(Nazwa.Text))
                {
                    MessageBox.Show("Wprowadź nazwę portu.");
                    return;
                }

                // Sprawdź czy wszystkie komórki są wypełnione w datagridMagazyny
                if (datagridMagazyny.Items.Cast<Magazine>().Any(m => string.IsNullOrWhiteSpace(m.Name) || m.Area == 0 || m.AvailableCapacity == 0 || string.IsNullOrWhiteSpace(m.Specification)))
                {
                    MessageBox.Show("Wypełnij wszystkie komórki w datagridMagazyny.");
                    return;
                }

                // Sprawdź czy wszystkie komórki są wypełnione w datagridTerminale
                if (datagridTerminale.Items.Cast<Terminal>().Any(t => string.IsNullOrWhiteSpace(t.Name) || string.IsNullOrWhiteSpace(t.Type) || t.MaxCapacity == 0 || t.AvailableFromDate == null))
                {
                    MessageBox.Show("Wypełnij wszystkie komórki w datagridTerminale.");
                    return;
                }

                // Twórz nowy port
                var newDock = new Dock
                {
                    Name = Nazwa.Text,
                };

                // Dodaj nowy port do bazy danych
                context.Docks.Add(newDock);
                context.SaveChanges();

                // Pobierz id nowo zapisanego doku
                int dockId = newDock.Id;

                // Zapisz dane z kolekcji 'magazynyCollection' do tabeli 'Magazines'
                foreach (var magazine in magazynyCollection)
                {
                    magazine.DockId = dockId;
                    context.Magazines.Add(magazine);
                }

                // Zapisz dane z kolekcji 'terminaleCollection' do tabeli 'Terminals'
                foreach (var terminal in terminaleCollection)
                {
                    terminal.DockId = dockId;
                    context.Terminals.Add(terminal);
                }

                // Zapisz zmiany w bazie danych
                context.SaveChanges();

                MessageBox.Show("Pomyślnie dodano nowego użytkownika do bazy danych.");

                // Załaduj dane
                admPorty adm = new admPorty();
                adm.LoadData();

                // Schowaj kontrolkę
                this.Visibility = Visibility.Collapsed;
            }
        }
        
        private void add_Row_Magazyny(object sender, RoutedEventArgs e)
        {
            
            magazynyCollection.Add(new Magazine
            {
                Name = "Nowy magazyn...",
                Area = 0,
                AvailableCapacity = 0,
                Specification = "Magazyny ogólne",
                
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (datagridMagazyny.SelectedItem is Magazine selectedMagazine)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Usuń zaznaczony magazyn z kolekcji
                    magazynyCollection.Remove(selectedMagazine);
                }
            }

        }

        private void add_Terminal(object sender, RoutedEventArgs e)
        {
            // Dodaj nowy obiekt do kolekcji
            terminaleCollection.Add(new Terminal
            {
                Name = "Nowy terminal...",
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
                    // Usuń zaznaczony magazyn z kolekcji
                    terminaleCollection.Remove(selectedTerminal);
                }
            }
        }
    }
}
