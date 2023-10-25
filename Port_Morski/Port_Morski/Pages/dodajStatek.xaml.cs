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
    /// Logika interakcji dla klasy dodajStatek.xaml
    /// </summary>
    public partial class dodajStatek : UserControl
    {
        public dodajStatek()
        {
            InitializeComponent();
            Btn_Exit.Click += Btn_Exit_Click;
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {

            this.Visibility = Visibility.Collapsed;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Nazwa.Text) || string.IsNullOrWhiteSpace(Pojemnosc.Text) || string.IsNullOrWhiteSpace(Typ.Text))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.");
                return;
            }


            using (var context = new SeaPortContext())
            {
                try
                {
                    var newShip = new Ship
                    {
                        Name = Nazwa.Text,
                        Capacity = int.Parse(Pojemnosc.Text),
                        Type = Typ.Text,
                        
                        
                    };

                    context.Ships.Add(newShip);
                    context.SaveChanges();

                    MessageBox.Show("Pomyślnie dodano nowego użytkownika do bazy danych.");


                    admUzytkownicy adm = new admUzytkownicy();
                    adm.Refresh();
                    this.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas dodawania użytkownika do bazy danych: Najprawdopodobniej w bazie w kolumnie id nie jest ustawione na auto uzupełnianie... {ex.Message}");
                }
            }
        }
    }
}
