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
            SetButtonColorOnLoad();


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
        private void SetButtonColorOnLoad()
        {
            StatystykiButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D7B377"));
            // Dodaj kolejne przyciski, jeśli są inne, i ustaw im kolor tła
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

        private Button currentButton;
        private void StatystykiButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            Statystyki statystyki = new Statystyki();
            MainGrid.Children.Add(statystyki);
            SwitchContentAndChangeButtonColor<Statystyki>((Button)sender);
            
        }

        private void MonitorowanieStatkow_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MonitorowanieStatkow monitorowanie = new MonitorowanieStatkow();
            MainGrid.Children.Add(monitorowanie);
            SwitchContentAndChangeButtonColor<MonitorowanieStatkow>((Button)sender);
            StatystykiButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8F754F"));
        }

        private void ZarzadzanieLadunkami_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            ZarzadzanieLadunkami zarzadzanieLadunkami = new ZarzadzanieLadunkami();
            MainGrid.Children.Add(zarzadzanieLadunkami);
            SwitchContentAndChangeButtonColor<ZarzadzanieLadunkami>((Button)sender);
            StatystykiButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8F754F"));
        }

        private void PlanowanieOperacjiPortowych_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            PlanowanieOperacjiPortowych planowanie = new PlanowanieOperacjiPortowych();
            MainGrid.Children.Add(planowanie);
            SwitchContentAndChangeButtonColor<PlanowanieOperacjiPortowych>((Button)sender);
            StatystykiButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8F754F"));
        }


        private void GenerowanieRaportow_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            GenerowanieRaportow generowanieRaportow = new GenerowanieRaportow();
            MainGrid.Children.Add(generowanieRaportow);
            SwitchContentAndChangeButtonColor<GenerowanieRaportow>((Button)sender);
            StatystykiButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8F754F"));
        }

        private void Administracja_Click(object sender, RoutedEventArgs e)
        {
            
            MainGrid.Children.Clear();
            Administracja administracja = new Administracja();
            MainGrid.Children.Add(administracja);
            SwitchContentAndChangeButtonColor<Administracja>((Button)sender);
            StatystykiButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8F754F"));
        }
        private void SwitchContentAndChangeButtonColor<T>(Button clickedButton) where T : UserControl, new()
        {
            // Przywrócenie koloru pierwotnego poprzedniego przycisku
            if (currentButton != null)
            {
                currentButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8F754F")); // Ustawienie pierwotnego koloru
            }

            // Ustawienie koloru tła aktualnie klikniętego przycisku
            clickedButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D7B377"));

            // Przechowanie aktualnie klikniętego przycisku
            currentButton = clickedButton;

            // Zmiana zawartości okna
            MainGrid.Children.Clear();
            T content = new T();
            MainGrid.Children.Add(content);
        }
    }
}
