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
    /// Logika interakcji dla klasy modyfikujLadunek.xaml
    /// </summary>
    public partial class modyfikujLadunek : UserControl
    {
        public modyfikujLadunek()
        {
            InitializeComponent();
            Btn_Exit.Click += Btn_Exit_Click;

            using (var context = new SeaPortContext())
            {
                var ships = context.Ships.ToList(); // Pobranie wszystkich statków z bazy danych
                Nazwa_Statku.ItemsSource = ships; // Przypisanie listy statków do ComboBox
                Nazwa_Statku.DisplayMemberPath = "Name"; // Ustawienie, które pole ma być wyświetlane w ComboBox

                foreach (var ship in ships)
                {
                    ship.Name = $"{ship.Id} - {ship.Name} - {ship.Type}"; // Połączenie nazwy statku i jego typu w jednym ciągu
                }
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {

            this.Visibility = Visibility.Collapsed;
        }

        private void modify_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new SeaPortContext())
            {
                try
                {
                    // Znajdź ładunek na podstawie jego Id
                    var cargo = context.Cargos.SingleOrDefault(s => s.Id == int.Parse(Id.Text));

                    if (cargo == null)
                    {
                        MessageBox.Show("Ładunek o podanym id nie istnieje.");
                        return;
                    }

                    // Przechowanie oryginalnych wartości ładunku
                    string originalName = cargo.Name;
                    int? originalWeight = cargo.Weight;
                    string originalStatus = cargo.Status;
                    int? originalShipId = cargo.ShipId;

                    // Aktualizacja jedynie zmienionych pól
                    if (!string.IsNullOrEmpty(Nazwa_Ladunku.Text) && Nazwa_Ladunku.Text != originalName)
                    {
                        cargo.Name = Nazwa_Ladunku.Text;
                    }

                    if (!string.IsNullOrEmpty(Waga_Ladunku.Text) && Waga_Ladunku.Text != originalWeight.ToString())
                    {
                        cargo.Weight = int.Parse(Waga_Ladunku.Text);
                    }

                    if (!string.IsNullOrEmpty(Status.Text) && Status.Text != originalStatus)
                    {
                        cargo.Status = Status.Text;
                    }

                    if (Nazwa_Statku.SelectedItem != null && Nazwa_Statku.SelectedItem is Ship selectedShip)
                    {
                        if (selectedShip.Id != originalShipId)
                        {
                            cargo.ShipId = selectedShip.Id; // Aktualizacja ID statku dla ładunku
                        }
                    }

                    context.SaveChanges();

                    MessageBox.Show("Pomyślnie zaktualizowano ładunek w bazie danych.");
                    this.Visibility = Visibility.Collapsed;
                    // Poniższy kod może wymagać modyfikacji w zależności od struktury Twojej aplikacji
                    ZarzadzanieLadunkami adm = new ZarzadzanieLadunkami();
                    adm.LoadData();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas aktualizacji ładunku w bazie danych: {ex.Message}");
                }
            }
        }



    }
}
