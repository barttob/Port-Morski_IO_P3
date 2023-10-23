using Port_Morski.Pages;
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


namespace Port_Morski
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? UserRole { get; }
        public MainWindow(string userRole)
        {
            InitializeComponent();
            UserRole = userRole;
            this.WindowState = WindowState.Maximized;



            // Sprawdź, czy Tag nie jest pusty
            if (!string.IsNullOrEmpty(UserRole))
            {
                

                // Ustaw widoczność przycisków w zależności od roli
                if (UserRole == "User")
                {
                    Administracja.Visibility = Visibility.Collapsed;
                    
                }
                else if (UserRole == null)
                {
                    MessageBox.Show("Niepoprawnie przesłano Tag roli dla użytkownika");
                }
                else if (UserRole == "Admin")
                {
                    Administracja.Visibility = Visibility.Visible;
                    
                }
                
            }
            else
            {
                MessageBox.Show("Niepoprawnie przesłano Tag roli dla użytkownika"); 
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void StatystykiButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            Statystyki statystyki = new Statystyki();
            MainGrid.Children.Add(statystyki);
        }

        private void MonitorowanieStatkow_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MonitorowanieStatkow monitorowanie = new MonitorowanieStatkow();
            MainGrid.Children.Add(monitorowanie);
        }

        private void ZarzadzanieLadunkami_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            ZarzadzanieLadunkami zarzadzanieLadunkami = new ZarzadzanieLadunkami();
            MainGrid.Children.Add(zarzadzanieLadunkami);
        }

        private void PlanowanieOperacjiPortowych_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            PlanowanieOperacjiPortowych planowanie = new PlanowanieOperacjiPortowych();
            MainGrid.Children.Add(planowanie);
        }


        private void GenerowanieRaportow_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            GenerowanieRaportow generowanieRaportow = new GenerowanieRaportow();
            MainGrid.Children.Add(generowanieRaportow);
        }

        private void Administracja_Click(object sender, RoutedEventArgs e)
        {
            
            MainGrid.Children.Clear();
            Administracja administracja = new Administracja();
            MainGrid.Children.Add(administracja);
        }
    }
}
