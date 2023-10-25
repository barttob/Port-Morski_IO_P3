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
    /// Logika interakcji dla klasy modyfikujStatek.xaml
    /// </summary>
    public partial class modyfikujStatek : UserControl
    {
        public modyfikujStatek()
        {
            InitializeComponent();
            Btn_Exit.Click += Btn_Exit_Click;
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
                    // Find the user by their login
                    var ship = context.Ships.SingleOrDefault(s => s.Id == int.Parse(Id.Text));

                    if (ship == null)
                    {
                        MessageBox.Show("Statek o podanym id nie istnieje.");
                        return;
                    }

                    ship.Name = Nazwa.Text;
                    ship.Capacity = int.Parse(Pojemnosc.Text);


                    if (Typ.SelectedItem != null)
                    {
                        ship.Type = Typ.Text;
                    }


                    context.SaveChanges();


                    MessageBox.Show("Pomyślnie zaktualizowano statek w bazie danych.");
                    this.Visibility = Visibility.Collapsed;
                    admStatki adm = new admStatki();
                    adm.Refresh();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas aktualizacji użytkownika w bazie danych: {ex.Message}");
                }
            }
        } }
}
