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
using static Port_Morski.Pages.Wyszukiwanie_statków;

namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy WyszukiwanieOperacjiPortowych.xaml
    /// </summary>
    public partial class WyszukiwanieOperacjiPortowych : Window
    {
        public WyszukiwanieOperacjiPortowych()
        {
            InitializeComponent();
            LoadData();
        }



        public void LoadData()
        {


            var nazwa_operacji_z_textboxa = textbox_nazwaOperacji.Text; 
            var id_statku_z_textboxa = textbox_identyfikatorStatku.Text;     
            var id_doku_z_textboxa = textbox_identyfikatorDoku.Text;     
            var czy_zatwierdzono_z_comboboxa = combobox_zatwierdzono.Text;       
            
            string dataOperacji = datepicker_dataOperacji.SelectedDate?.ToString("yyyy-MM-dd");




            using (var context = new SeaPortContext())
            {
                var query = from o in context.Operationss
                            where (string.IsNullOrEmpty(nazwa_operacji_z_textboxa) || o.Operation.StartsWith(nazwa_operacji_z_textboxa)) &&
                                  (string.IsNullOrEmpty(id_statku_z_textboxa) || o.ShipId == int.Parse(id_statku_z_textboxa)) &&
                                  (string.IsNullOrEmpty(id_doku_z_textboxa) || o.DockId == int.Parse(id_doku_z_textboxa)) &&
                                  (string.IsNullOrEmpty(czy_zatwierdzono_z_comboboxa) || o.Approved == bool.Parse(czy_zatwierdzono_z_comboboxa)) &&
                                  (string.IsNullOrEmpty(dataOperacji) || o.Date == DateTime.Parse(dataOperacji))
                            select new PortOperations
                            {
                                id = o.Id,
                                operation = o.Operation,
                                ship_id = (int)o.ShipId,
                                dock_id = (int)o.DockId,
                                approved = (bool)o.Approved,
                                date = (DateTime)o.Date
                                
                            };


                var result = query.ToList();
                datagridWyszukaneStatki.ItemsSource = result;
            }
        }


        public class PortOperations
        {
            public int id { get; set; }
            public string? operation { get; set; }
            public int ship_id { get; set; }
            public int dock_id { get; set; }
            public bool approved { get; set; }
            public DateTime date { get; set; }
        }



        private void Wyszukaj_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
