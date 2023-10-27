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
    /// Logika interakcji dla klasy MonitorowanieStatkow.xaml
    /// </summary>
    public partial class MonitorowanieStatkow : UserControl
    {
        public MonitorowanieStatkow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Tworzenie nowej instancji strony Wyszukiwanie_statków
            Wyszukiwanie_statków wyszukiwanieStron = new Wyszukiwanie_statków();

            // Wyświetlanie nowej strony
            wyszukiwanieStron.Show();


            

        }
    }
}
