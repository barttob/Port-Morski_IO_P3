using Microsoft.EntityFrameworkCore;
using Port_Morski.Models;
using Port_Morski.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Port_Morski
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? UserRole { get; }
        int userId = Login.LoggedInUser.UserId;
        private MainViewModel _viewModel;
        Ustawienia ustawieniaControl;
        private UserPreferences userPreferences;
        private Button currentButton;

        public MainWindow(string userRole)
        {
            InitializeComponent();
            Loaded += TwojeOkno_Loaded;
            UserRole = userRole;
            
            userPreferences = UserPreferencesManager.LoadPreferences(userId);
            SetTheme(userPreferences.SelectedTheme);
            SetSize(userPreferences.SelectedSize);
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
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
            _viewModel.NazwaKontroli = "Statystyki dla Port Morski";
            using (var context = new SeaPortContext(new DbContextOptions<SeaPortContext>()))
            {
                var user = context.Users.FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                   


                   
                    // Aktualizacja TextBlock 'User'
                    User.Text = $"{user.Name} {user.LastName}";

                    // Przekształcenie roli na czytelny dla użytkownika format
                    string roleDisplay = user.UserRole == "Admin" ? "Administrator" : "Użytkownik";
                    
                    // Aktualizacja TextBlock 'Rola'
                    Rola.Text = $"({roleDisplay})";
                   

                }
                else
                {
                    // Obsługa sytuacji, gdy użytkownik nie istnieje w bazie danych
                    MessageBox.Show("Błąd: Użytkownik nie znaleziony w bazie danych.");
                }
            }

        }
        private void TwojeOkno_Loaded(object sender, RoutedEventArgs e)
        {
            StatystykiButton_Click(StatystykiButton, new RoutedEventArgs(Button.ClickEvent, StatystykiButton));
        }
        private void UstawieniaControl_WyborZmieniony(object sender, string wybranaOpcja)
        {
            // Zapisz wybrany motyw do preferencji użytkownika
            userPreferences.SelectedTheme = wybranaOpcja;
            UserPreferencesManager.SavePreferences(userPreferences);

            // Ustaw motyw interfejsu użytkownika
            SetTheme(wybranaOpcja);
        }

        private string DefaultColorButton = "";
        private string CurrentColorButtonClick = "";

        private void SetTheme(string selectedTheme)
        {
            switch (selectedTheme)
            {
                case "Domyślny":
                    Menu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2B4162"));
                    PanelGorny.Background = Brushes.White;
                    StatystykiButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F754F"));
                    MonitorowanieStatkow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F754F"));
                    ZarzadzanieLadunkami.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F754F"));
                    PlanowanieOperacjiPortowych.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F754F"));
                    GenerowanieRaportow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F754F"));
                    Administracja.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F754F"));
                    StatystykiButton.Foreground = Brushes.Black;
                    MonitorowanieStatkow.Foreground = Brushes.Black;
                    ZarzadzanieLadunkami.Foreground = Brushes.Black;
                    PlanowanieOperacjiPortowych.Foreground = Brushes.Black;
                    GenerowanieRaportow.Foreground = Brushes.Black;
                    Administracja.Foreground = Brushes.Black;

                    //Panel górny
                    PanelGorny.Background = Brushes.White;
                    Napis_glowny.Foreground = Brushes.Black;
                    User.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                    Rola.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#666666"));

                    DefaultColorButton = "#8F754F";
                    CurrentColorButtonClick = "#D7B377";

                    break;
                case "Neptun's Dream":
                    Menu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBBAF"));

                    StatystykiButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555A54"));
                    MonitorowanieStatkow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555A54"));
                    ZarzadzanieLadunkami.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555A54"));
                    PlanowanieOperacjiPortowych.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555A54"));
                    GenerowanieRaportow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555A54"));
                    Administracja.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555A54"));
                    StatystykiButton.Foreground = Brushes.White;
                    MonitorowanieStatkow.Foreground = Brushes.White;
                    ZarzadzanieLadunkami.Foreground = Brushes.White;
                    PlanowanieOperacjiPortowych.Foreground = Brushes.White;
                    GenerowanieRaportow.Foreground = Brushes.White;
                    Administracja.Foreground = Brushes.White;

                    //Panel górny
                    PanelGorny.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E1DDD2"));
                    Napis_glowny.Foreground = Brushes.Black;
                    User.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                    Rola.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#666666"));

                    DefaultColorButton = "#555A54";
                    CurrentColorButtonClick = "#95908c";
                    break;
                case "Galaktyczny Pirs":
                    Menu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F2F2F"));

                    StatystykiButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c5c5c5"));
                    MonitorowanieStatkow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c5c5c5"));
                    ZarzadzanieLadunkami.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c5c5c5"));
                    PlanowanieOperacjiPortowych.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c5c5c5"));
                    GenerowanieRaportow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c5c5c5"));
                    Administracja.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c5c5c5"));
                    StatystykiButton.Foreground = Brushes.Black;
                    MonitorowanieStatkow.Foreground = Brushes.Black;
                    ZarzadzanieLadunkami.Foreground = Brushes.Black;
                    PlanowanieOperacjiPortowych.Foreground = Brushes.Black;
                    GenerowanieRaportow.Foreground = Brushes.Black;
                    Administracja.Foreground = Brushes.Black;

                    //Panel górny
                    PanelGorny.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6F6F6"));
                    Napis_glowny.Foreground = Brushes.Black;
                    User.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                    Rola.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#666666"));

                    DefaultColorButton = "#c5c5c5";
                    CurrentColorButtonClick = "#939393";
                    break;
                case "Mistyczna Morska Mgła":

                    //Menu po prawej stronie
                    Menu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6ce636"));

                    StatystykiButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#393939"));
                    MonitorowanieStatkow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#393939"));
                    ZarzadzanieLadunkami.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#393939"));
                    PlanowanieOperacjiPortowych.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#393939"));
                    GenerowanieRaportow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#393939"));
                    Administracja.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#393939"));
                    StatystykiButton.Foreground = Brushes.White;
                    MonitorowanieStatkow.Foreground = Brushes.White;
                    ZarzadzanieLadunkami.Foreground = Brushes.White;
                    PlanowanieOperacjiPortowych.Foreground = Brushes.White;
                    GenerowanieRaportow.Foreground = Brushes.White;
                    Administracja.Foreground = Brushes.White;

                    //Panel górny
                    PanelGorny.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4638FF"));
                    Napis_glowny.Foreground = Brushes.White;
                    User.Foreground = Brushes.White;
                    Rola.Foreground = Brushes.LightGray;


                    DefaultColorButton = "#393939";
                    CurrentColorButtonClick = "#4638FF";
                    break;
                default:
                    Menu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2B4162"));
                    PanelGorny.Background = Brushes.White;
                    StatystykiButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F754F"));
                    MonitorowanieStatkow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F754F"));
                    ZarzadzanieLadunkami.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F754F"));
                    PlanowanieOperacjiPortowych.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F754F"));
                    GenerowanieRaportow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F754F"));
                    Administracja.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8F754F"));
                    StatystykiButton.Foreground = Brushes.Black;
                    MonitorowanieStatkow.Foreground = Brushes.Black;
                    ZarzadzanieLadunkami.Foreground = Brushes.Black;
                    PlanowanieOperacjiPortowych.Foreground = Brushes.Black;
                    GenerowanieRaportow.Foreground = Brushes.Black;
                    Administracja.Foreground = Brushes.Black;

                    //Panel górny
                    PanelGorny.Background = Brushes.White;
                    Napis_glowny.Foreground = Brushes.Black;
                    User.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                    Rola.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#666666"));

                    DefaultColorButton = "#555A54";
                    CurrentColorButtonClick = "#D7B377";
                    break;
            }
        }
        private void UstawieniaControl_WyborRozmiaruZmieniony(object sender, string wybranyRozmiar)
        {
            // Zapisz wybrany rozmiar do preferencji użytkownika
            userPreferences.SelectedSize = wybranyRozmiar;
            UserPreferencesManager.SavePreferences(userPreferences);

            // Ustaw rozmiar okna aplikacji
            SetSize(wybranyRozmiar);
        }
        private void SetSize(string selectedSize)
        {
            
            switch (selectedSize)
            {
                case "Zmaksymalizowany":
                    this.WindowState = WindowState.Maximized;
                    break;
                case "Zminimalizowany":
                    this.WindowState = WindowState.Minimized;
                    break;
                case "Pełny ekran":
                   this.WindowStyle = WindowStyle.None;
                   this.WindowState = WindowState.Maximized;
                    break;
                default:
                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Maximized;
                    break;
            }
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
            SwitchContentAndChangeButtonColor<Statystyki>((Button)sender);
            _viewModel.NazwaKontroli = "Statystyki dla Port Morski";
        }
        

        private void MonitorowanieStatkow_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MonitorowanieStatkow monitorowanie = new MonitorowanieStatkow();
            MainGrid.Children.Add(monitorowanie);
            SwitchContentAndChangeButtonColor<MonitorowanieStatkow>((Button)sender);
            //StatystykiButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8F754F"));
            _viewModel.NazwaKontroli = "Monitorowanie Statków";
        }

        private void ZarzadzanieLadunkami_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            ZarzadzanieLadunkami zarzadzanieLadunkami = new ZarzadzanieLadunkami();
            MainGrid.Children.Add(zarzadzanieLadunkami);
            SwitchContentAndChangeButtonColor<ZarzadzanieLadunkami>((Button)sender);
            //StatystykiButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8F754F"));
            _viewModel.NazwaKontroli = "Zarządzanie Ładunkami";
        }

        private void PlanowanieOperacjiPortowych_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            PlanowanieOperacjiPortowych planowanie = new PlanowanieOperacjiPortowych();
            MainGrid.Children.Add(planowanie);
            SwitchContentAndChangeButtonColor<PlanowanieOperacjiPortowych>((Button)sender);
           // StatystykiButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8F754F"));
            _viewModel.NazwaKontroli = "Planowanie Opreracji Portowych";
        }

        private void GenerowanieRaportow_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            GenerowanieRaportow generowanieRaportow = new GenerowanieRaportow();
            MainGrid.Children.Add(generowanieRaportow);
            SwitchContentAndChangeButtonColor<GenerowanieRaportow>((Button)sender);
           // StatystykiButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8F754F"));
            _viewModel.NazwaKontroli = "Generowanie Raportów";
        }

        private void Administracja_Click(object sender, RoutedEventArgs e)
        {
            
            MainGrid.Children.Clear();
            Administracja administracja = new Administracja();
            MainGrid.Children.Add(administracja);
            SwitchContentAndChangeButtonColor<Administracja>((Button)sender);
           // StatystykiButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8F754F"));
            _viewModel.NazwaKontroli = "Administracja";
        }
        private void SwitchContentAndChangeButtonColor<T>(Button clickedButton, bool setAsCurrentButton = true) where T : UserControl, new()
        {
            // Przywrócenie koloru pierwotnego poprzedniego przycisku
            if (currentButton != null && setAsCurrentButton)
            {
                Color defaultButtonColor = (Color)ColorConverter.ConvertFromString(DefaultColorButton);
                currentButton.Background = new SolidColorBrush(defaultButtonColor); // Ustawienie pierwotnego koloru
            }

            Color currentButtonColor = (Color)ColorConverter.ConvertFromString(CurrentColorButtonClick);
            // Ustawienie koloru tła aktualnie klikniętego przycisku
            clickedButton.Background = new SolidColorBrush(currentButtonColor);

            // Przechowanie aktualnie klikniętego przycisku
            if (setAsCurrentButton)
            {
                currentButton = clickedButton;
            }

            // Zmiana zawartości okna
            MainGrid.Children.Clear();
            T content = new T();
            MainGrid.Children.Add(content);
        }


        private DispatcherTimer timer;
        private void MenuButtonClick(object sender, RoutedEventArgs e)
        {
            if (MenuPopup.IsOpen)
            {
                MenuPopup.IsOpen = false;
                StopTimer();
            }
            else
            {
                MenuPopup.IsOpen = true;
                StartTimer();
            }
        }
        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            MenuPopup.IsOpen = false;
            StopTimer();
        }
        private void Info_Click(object sender, MouseButtonEventArgs e)
        {
            
            if (currentButton != null)
            {
                Color defaultButtonColor = (Color)ColorConverter.ConvertFromString(DefaultColorButton);
                currentButton.Background = new SolidColorBrush(defaultButtonColor);
                currentButton = null;
            }

            MainGrid.Children.Clear();
            O_Programie oProgramie = new O_Programie();
            MainGrid.Children.Add(oProgramie);
            _viewModel.NazwaKontroli = "Informacje o Programie";
           
        }

        private void Settings_Click(object sender, MouseButtonEventArgs e)
        {
           
            if (currentButton != null)
            {
                Color defaultButtonColor = (Color)ColorConverter.ConvertFromString(DefaultColorButton);
                currentButton.Background = new SolidColorBrush(defaultButtonColor);
                currentButton = null;
            }

            
            ustawieniaControl = new Ustawienia();
            ustawieniaControl.WyborZmieniony += UstawieniaControl_WyborZmieniony;
            ustawieniaControl.WyborRozmiaruZmieniony += UstawieniaControl_WyborRozmiaruZmieniony;
            _viewModel.NazwaKontroli = "Ustawienia";
            MainGrid.Children.Add(ustawieniaControl);
        }

        private void Logout_Click(object sender, MouseButtonEventArgs e)
        {
            MainGrid.Children.Clear();
            Login login = new Login();
            login.Show();
            this.Close();
        }

          
        
    }
}
