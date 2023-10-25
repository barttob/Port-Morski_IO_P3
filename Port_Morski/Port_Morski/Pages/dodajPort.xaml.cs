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
using Dock = Port_Morski.Models.Dock;

namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy dodajPort.xaml
    /// </summary>
    public partial class dodajPort : UserControl
    {
        public dodajPort()
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
            if (string.IsNullOrWhiteSpace(Nazwa.Text))
            {
                MessageBox.Show("Wypełnij dane !!!");
                return;
            }


            using (var context = new SeaPortContext())
            {
               
                    var newdock = new Dock
                    {
                        Name = Nazwa.Text,
                        
                    };

                    context.Docks.Add(newdock);
                    context.SaveChanges();

                    MessageBox.Show("Pomyślnie dodano nowego użytkownika do bazy danych.");


                    admPorty adm = new admPorty();
                    adm.LoadData();
                    this.Visibility = Visibility.Collapsed;
                
                
            }
        }
    }
}
