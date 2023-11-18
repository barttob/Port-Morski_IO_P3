using Port_Morski.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Port_Morski.Pages
{
    public partial class Statystyki : UserControl
    {
        private SeaPortContext context; 

        public Statystyki()
        {
            InitializeComponent();
            context = new SeaPortContext();
            wyswietl();
        }

      
        

        public void wyswietl()
        {
            // Fetch data from the database
            var operacjeLogDataFromDatabase = context.Operacje_Logs.ToList();

            // Bind the data to the DataGrid
            DataGridHistory.ItemsSource = operacjeLogDataFromDatabase;
        }

        private void ButtonWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new SeaPortContext())
            {
                var numer_z_textboxa = TextBoxNumer.Text;
                var operacja_z_textboxa = TextBoxOperacja.Text;
                var id_statku_z_textboxa = TextBoxIDStatku.Text;
                var id_doku_z_textboxa = TextBoxIDDoku.Text;
                var czy_zatwierdzony_z_textboxa = TextBoxCzyZatwierdzony.Text;
                var typ_z_textboxa = TextBoxTyp.Text;

                // Konwertuj wybrane daty na odpowiednią reprezentację
                DateTime? data_z_datepickera = DatePickerData.SelectedDate;
                DateTime? data_logu_z_datepickera = DatePickerDataLogu.SelectedDate;

                var query = from log in context.Operacje_Logs
                            where (string.IsNullOrEmpty(numer_z_textboxa) || log.LogID.ToString() == numer_z_textboxa) &&
                                  (string.IsNullOrEmpty(operacja_z_textboxa) || log.OldOperation == operacja_z_textboxa) &&
                                  (string.IsNullOrEmpty(id_statku_z_textboxa) || log.OldShipId.ToString() == id_statku_z_textboxa) &&
                                  (string.IsNullOrEmpty(id_doku_z_textboxa) || log.OldDockId.ToString() == id_doku_z_textboxa) &&
                                  (string.IsNullOrEmpty(czy_zatwierdzony_z_textboxa) || log.OldApproved.ToString() == czy_zatwierdzony_z_textboxa) &&
                                  (string.IsNullOrEmpty(typ_z_textboxa) || log.OperationTypeOnTable == typ_z_textboxa) &&
                                  (!data_z_datepickera.HasValue || log.OldDate == data_z_datepickera) &&
                                  (!data_logu_z_datepickera.HasValue || log.LogDate == data_logu_z_datepickera)
                            select log;

                var result = query.ToList();
                DataGridHistory.ItemsSource = result;
            }
        }

        
    }
}
