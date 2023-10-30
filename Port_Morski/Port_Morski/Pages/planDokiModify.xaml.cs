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
    /// Interaction logic for planDokiModify.xaml
    /// </summary>
    public partial class planDokiModify : UserControl
    {
        public planDokiModify()
        {
            InitializeComponent();
            Btn_Exit.Click += Btn_Exit_Click;
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {

            this.Visibility = Visibility.Collapsed;
        }

        public void addSS_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
