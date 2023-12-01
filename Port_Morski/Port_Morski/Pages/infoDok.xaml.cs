using Port_Morski.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy infoDok.xaml
    /// </summary>
    public partial class infoDok : UserControl
    {
        public infoDok()
        {
            InitializeComponent();
        }
        private ObservableCollection<Magazine> magazines = new ObservableCollection<Magazine>();
        private ObservableCollection<Terminal> terminal = new ObservableCollection<Terminal>();
        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {

            this.Visibility = Visibility.Collapsed;
        }
        public void LoadMagazinesData()
        {
            using (SeaPortContext context = new SeaPortContext())
            {
                int dockId = int.Parse(ID.Text);


                magazines = new ObservableCollection<Magazine>(
                    context.Magazines
                           .Where(m => m.DockId == dockId)
                           .ToList()
                );

                dataGridMagazyny.ItemsSource = magazines;

                terminal = new ObservableCollection<Terminal>(
                    context.Terminals
                            .Where(terminal => terminal.DockId == dockId)
                            .ToList()
                            );

                dataGridTerminale.ItemsSource = terminal;
            }
        }
    }
}
