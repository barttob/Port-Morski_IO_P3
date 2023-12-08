using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;
using Port_Morski.Models;
using System.Globalization;
using LiveCharts.Defaults;

namespace Port_Morski
{
    class IloscOperacji
    {
        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string[] Months { get; set; }

        public SeriesCollection SeriesCollections { get; set; }
        public List<string> Statusy { get; set; }


        public IloscOperacji() 
        {

            // Pobierz dane z bazy danych
            List<Operacje> operacje;
            using (var context = new SeaPortContext())
            {
                operacje = context.Operacje.ToList();
            }

            // Utwórz listę zliczeń operacji dla każdego miesiąca
            var operacjeGrupowane = operacje
                .Where(o => o.Date.HasValue)
                .GroupBy(o => o.Date.Value.Month)
                .OrderBy(g => g.Key)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToList();

            // Utwórz listę zliczeń dla wszystkich miesięcy (łącznie z miesiącami, gdzie nie ma operacji)
            var wszystkieMiesiace = Enumerable.Range(1, 12);
            var zliczeniaWszystkichMiesiecy = wszystkieMiesiace
                .GroupJoin(operacjeGrupowane, m => m, g => g.Month, (m, g) => g.FirstOrDefault()?.Count ?? 0)
                .ToList();

            // Utwórz dane dla wykresu
            SeriesCollection = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "Ilość operacji",
                Values = new ChartValues<int>(zliczeniaWszystkichMiesiecy),
            }
        };

            // Utwórz etykiety dla osi X
            Months = wszystkieMiesiace.Select(m => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m)).ToArray();

            // Ustaw formater dla etykiet osi Y
            YFormatter = value => value.ToString("N0");









            Statusy = new List<string>
        {
            "Przyjęty", "Załadowany", "W Transporcie", "Oczekujący na odprawę celna",
            "Przygotowany do rozładunku", "Rozładowany", "Przechowywany", "Wysłany",
            "Dostarczony", "Anulowany"
        };

            SeriesCollections = new SeriesCollection();

            using (var context = new SeaPortContext())
            {
                foreach (var status in Statusy)
                {
                    var ilosc = context.Cargos.Count(c => c.Status == status);
                    SeriesCollections.Add(new PieSeries { Title = status, Values = new ChartValues<int> { ilosc } });
                }
            }


        }
       

    }
}
