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
using System.Windows.Shapes;

namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Wyszukiwanie_statków.xaml
    /// </summary>
    public partial class Wyszukiwanie_statków : Window
    {
        public Wyszukiwanie_statków()
        {
            InitializeComponent();
            LoadData();
        }

        




        //walidacja żeby nie wpisywać zera
        private void textbox_pojemnoscStatku_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textbox_pojemnoscStatku.Text) && !int.TryParse(textbox_pojemnoscStatku.Text, out _))
            {
                MessageBox.Show("Pojemność statku musi być liczbą.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_pojemnoscStatku.Text = ""; // Wyczyść pole
            }
        }


        // walidacja żeby nie wpisywać zera
        private void textbox_wagaLadunku_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textbox_wagaLadunku.Text) && !int.TryParse(textbox_wagaLadunku.Text, out _))
            {
                MessageBox.Show("Waga ładunku musi być liczbą.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                textbox_wagaLadunku.Text = ""; // Wyczyść pole
            }
        }


        public void LoadData()
        {


            var nazwa_statku_z_textboxa = textbox_nazwaStatku.Text;  //ma porównać z ShipName
            var typ_statku_z_textboxa = combobox_typStatku.Text;     //ma porównać z ShipType
            var pojemnosc_statku_z_textboxa = textbox_pojemnoscStatku.Text;     //ma porównać z ShipCapacity
            var nazwa_ladunku_z_textboxa = textbox_nazwaLadunku.Text;       //ma porównać z CargoName
            var waga_ladunku_z_textboxa = textbox_wagaLadunku.Text;     //ma porównać z CargoWeight
            var status_ladunku_z_textboxa = combobox_statusLadunku.Text;     //ma porównać z CargoStatus
           // var data_przybycia_z_textboxa = textbox_dataPrzybycia.Text;     //ma porównać z ArrivalDate 
            string dataPrzybyciaStr = datepicker_dataPrzybycia.SelectedDate?.ToString("yyyy-MM-dd");
            string dataOdplywuStr = datepicker_dataOdplywu.SelectedDate?.ToString("yyyy-MM-dd");


            var nazwa_doku_z_textboxa = textbox_nazwaDoku.Text;     //ma porównać z DockName


            using (var context = new SeaPortContext())
            {
                var query = from s in context.Ships
                            join c in context.Cargos on s.Id equals c.ShipId
                            join ss in context.ShipSchedules on s.Id equals ss.ShipId
                            join d in context.Docks on ss.DockId equals d.Id
                            where (string.IsNullOrEmpty(nazwa_statku_z_textboxa) || s.Name == nazwa_statku_z_textboxa) &&
                                  (string.IsNullOrEmpty(typ_statku_z_textboxa) || s.Type == typ_statku_z_textboxa) &&
                                  (string.IsNullOrEmpty(pojemnosc_statku_z_textboxa) || s.Capacity == int.Parse(pojemnosc_statku_z_textboxa)) &&
                                  (string.IsNullOrEmpty(nazwa_ladunku_z_textboxa) || c.Name == nazwa_ladunku_z_textboxa) &&
                                  (string.IsNullOrEmpty(waga_ladunku_z_textboxa) || c.Weight == int.Parse(waga_ladunku_z_textboxa)) &&
                                  (string.IsNullOrEmpty(status_ladunku_z_textboxa) || c.Status == status_ladunku_z_textboxa) &&
                                  (string.IsNullOrEmpty(dataPrzybyciaStr) || ss.ArriveDate == DateTime.Parse(dataPrzybyciaStr)) &&
                                  (string.IsNullOrEmpty(dataOdplywuStr) || ss.FlowOutDate == DateTime.Parse(dataOdplywuStr)) &&
                                  (string.IsNullOrEmpty(nazwa_doku_z_textboxa) || d.Name == nazwa_doku_z_textboxa)
                            select new ShipData
                            {
                                ShipName = s.Name,
                                ShipType = s.Type,
                                ShipCapacity = (int)s.Capacity,
                                CargoName = c.Name,
                                CargoWeight = (int)c.Weight,
                                CargoStatus = c.Status,
                                ArrivalDate = (DateTime)ss.ArriveDate,
                                FlowOutDate = (DateTime)ss.FlowOutDate,
                                DockName = d.Name
                            };


                var result = query.ToList();
                datagridWyszukaneStatki.ItemsSource = result;
            }
        }


        public class ShipData
        {
            public string? ShipName { get; set; }
            public string? ShipType { get; set; }
            public int ShipCapacity { get; set; }
            public string? CargoName { get; set; }
            public int CargoWeight { get; set; }
            public string? CargoStatus { get; set; }
            public DateTime ArrivalDate { get; set; }
            public DateTime FlowOutDate { get; set; }
            public string? DockName { get; set; }
        }


        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Wyszukaj_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        
    }
}
