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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy PlanowanieOperacjiPortowych.xaml
    /// </summary>
    public partial class PlanowanieOperacjiPortowych : UserControl
    {
        public PlanowanieOperacjiPortowych()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            using (var context = new SeaPortContext())
            {
                var shipSchedule = context.ShipSchedules
        .Include(schedule => schedule.Ship)
        .Include(schedule => schedule.Dock)
        .ToList();
                datagridPlanDokowFront.ItemsSource = shipSchedule;
            }
        }

        private void wiecejPlanDokow_Click(object sender, RoutedEventArgs e)
        {
            planDoki.Visibility = Visibility.Visible;
        }
    }
}
