using Microsoft.EntityFrameworkCore;
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
        }
        private ObservableCollection<Magazine> magazines = new ObservableCollection<Magazine>();
        private ObservableCollection<Terminal> terminal = new ObservableCollection<Terminal>();
       
        
        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {

            this.Visibility = Visibility.Collapsed;
        }
        public void LoadMagazinesData()
        {
            using (SeaPortContext context = new SeaPortContext())
            {
                int dockId = int.Parse(Id.Text);

                
                magazines = new ObservableCollection<Magazine>(
                    context.Magazines
                           .Where(m => m.DockId == dockId)
                           .ToList()
                );

                datagridMagazyny.ItemsSource = magazines;

                terminal = new ObservableCollection<Terminal>(
                    context.Terminals
                            .Where(terminal => terminal.DockId == dockId)
                            .ToList()
                            );

                datagridTerminale.ItemsSource = terminal;
            }
        }
        public void modify_Click(object sender, RoutedEventArgs e)
        {
            ModifyDock();
            ModifyMagazines();
            ModifyTerminals();

            MessageBox.Show("Pomyślnie zaktualizowano port w bazie danych.");
            this.Visibility = Visibility.Collapsed;


            admPorty adm = new admPorty();
            adm.LoadData();

        }


        private void ModifyDock()
        {
            using (var context = new SeaPortContext())
            {
                try
                {

                    var dock = context.Docks.SingleOrDefault(s => s.Id == int.Parse(Id.Text));

                    if (dock == null)
                    {
                        MessageBox.Show("Port o podanym id nie istnieje.");
                        return;
                    }

                    dock.Name = Nazwa.Text;

                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas aktualizacji użytkownika w bazie danych: {ex.Message}");
                }
            }

        }

        private void ModifyMagazines()
        {
            using (SeaPortContext context = new SeaPortContext())
            {
                int dockId = int.Parse(Id.Text);

                List<Magazine> magazinesToDelete = context.Magazines.Where(m => m.DockId == dockId).ToList();
                foreach (Magazine deletedMagazine in magazinesToDelete)
                {
                    context.Magazines.Remove(deletedMagazine);
                }

                foreach (Magazine newMagazine in magazines)
                {
                    newMagazine.DockId = dockId; 
                    context.Magazines.Add(newMagazine);
                }
                context.SaveChanges();
            }
        }

        private void ModifyTerminals()
        {
            using (SeaPortContext context = new SeaPortContext())
            {
                int dockId = int.Parse(Id.Text);

                List<Terminal> terminalToDelete = context.Terminals.Where(t => t.DockId == dockId).ToList();
                foreach (Terminal deletedTerminal in terminalToDelete)
                {
                    context.Terminals.Remove(deletedTerminal);
                }

                foreach (Terminal newTerminal in terminal)
                {
                    newTerminal.DockId = dockId; 
                    context.Terminals.Add(newTerminal);
                }

                context.SaveChanges();
            }
        }


        private void add_Row_Magazyny(object sender, RoutedEventArgs e)
        {
            magazines.Add(new Magazine
            {
                Name = "Nowy magazyn...",
                Area = 0,
                AvailableCapacity = 0,
                Specification = "Magazyn ogolny",

            });
        }

        private void add_Row_Terminal(object sender, RoutedEventArgs e)
        {
            terminal.Add(new Terminal
            {
                Name = "Nowy terminal",
                Type = "Terminal kontenerowy",
                MaxCapacity = 0,
                Available = true,
                AvailableFromDate = DateTime.Now,

            });
        }

        private void Del_Mag_Click(object sender, RoutedEventArgs e)
        {
            if (datagridMagazyny.SelectedItem is Magazine selectedMagazine)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    magazines.Remove(selectedMagazine);
                }
            }
        }

        private void Del_Term_Click(object sender, RoutedEventArgs e)
        {
            if (datagridTerminale.SelectedItem is Terminal selectedTerminal)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    terminal.Remove(selectedTerminal);
                }
            }
        }
    }
}
