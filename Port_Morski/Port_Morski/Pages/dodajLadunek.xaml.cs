using Port_Morski.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Logika interakcji dla klasy dodajLadunek.xaml
    /// </summary>
    public partial class dodajLadunek : UserControl
    {
        public dodajLadunek()
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

        public void add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Nazwa_Ladunku.Text) || string.IsNullOrWhiteSpace(Waga_Ladunku.Text) || string.IsNullOrWhiteSpace(Nazwa_Statku.Text) || string.IsNullOrWhiteSpace(Status.Text))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.");
                return;
            }

            using (var context = new SeaPortContext())
            {
                try
                {
                    var selectedShip = (Ship)Nazwa_Statku.SelectedItem; // Pobranie wybranego statku z ComboBoxu
                    if (selectedShip == null)
                    {
                        MessageBox.Show("Nie wybrano odpowiedniego statku.");
                        return;
                    }

                    var newCargo = new Cargo
                    {
                        Name = Nazwa_Ladunku.Text,
                        Weight = Convert.ToInt32(Waga_Ladunku.Text),
                        ShipId = selectedShip.Id, // Przypisanie ID statku do kolumny ship_id
                        Status = Status.Text
                    };

                    context.Cargos.Add(newCargo);
                    context.SaveChanges();

                    MessageBox.Show("Pomyślnie dodano nowy ładunek do bazy danych.");

                    // Poniższy kod może wymagać modyfikacji w zależności od struktury Twojej aplikacji
                    ZarzadzanieLadunkami adm = new ZarzadzanieLadunkami();
                    adm.LoadData();
                    this.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas dodawania ładunku do bazy danych: {ex.Message}");
                }
            }
        }
    }
}
