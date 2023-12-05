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
using static Port_Morski.Login;

namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Ustawienia.xaml
    /// </summary>
    public partial class Ustawienia : UserControl
    {
        int userId = Port_Morski.Login.LoggedInUser.UserId;

        public event EventHandler<string> WyborZmieniony;



        public Ustawienia()
        {
            InitializeComponent();
            Dane();
            
        }
        private bool AreFieldsNotEmpty()
{
    return !string.IsNullOrWhiteSpace(new TextRange(Imie.Document.ContentStart, Imie.Document.ContentEnd).Text.Trim()) &&
           !string.IsNullOrWhiteSpace(new TextRange(Nazwisko.Document.ContentStart, Nazwisko.Document.ContentEnd).Text.Trim()) &&
           !string.IsNullOrWhiteSpace(new TextRange(Loginn.Document.ContentStart, Loginn.Document.ContentEnd).Text.Trim()) &&
           !string.IsNullOrWhiteSpace(new TextRange(Haslo.Document.ContentStart, Haslo.Document.ContentEnd).Text.Trim());
}

        private void ZMODYFIKUJ_Click(object sender, RoutedEventArgs e)
        {
            int userId = LoggedInUser.UserId;

            if (!AreFieldsNotEmpty())
            {
                MessageBox.Show("Proszę wypełnić wszystkie pola przed zapisaniem zmian.");
                return;
            }

            using (var context = new SeaPortContext(new DbContextOptions<SeaPortContext>()))
            {
                var user = context.Users.FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    // Pobranie danych przed modyfikacją
                    string originalName = user.Name;
                    string originalLastName = user.LastName;
                    string originalLogin = user.Login;
                    string originalPassword = user.Password;

                    // Aktualizacja danych na podstawie wprowadzonych zmian
                    string newName = new TextRange(Imie.Document.ContentStart, Imie.Document.ContentEnd).Text.Trim();
                    string newLastName = new TextRange(Nazwisko.Document.ContentStart, Nazwisko.Document.ContentEnd).Text.Trim();
                    string newLogin = new TextRange(Loginn.Document.ContentStart, Loginn.Document.ContentEnd).Text.Trim();
                    string newPassword = new TextRange(Haslo.Document.ContentStart, Haslo.Document.ContentEnd).Text.Trim();

                    // Sprawdzenie, czy dane zostały zmodyfikowane
                    bool isNameModified = !string.Equals(originalName, newName);
                    bool isLastNameModified = !string.Equals(originalLastName, newLastName);
                    bool isLoginModified = !string.Equals(originalLogin, newLogin);
                    bool isPasswordModified = !string.Equals(originalPassword, newPassword);

                    // Aktualizacja danych w bazie tylko dla zmienionych pól
                    if (isNameModified || isLastNameModified || isLoginModified || isPasswordModified)
                    {
                        if (isNameModified) user.Name = newName;
                        if (isLastNameModified) user.LastName = newLastName;
                        if (isLoginModified) user.Login = newLogin;
                        if (isPasswordModified) user.Password = newPassword;

                        context.SaveChanges();

                        // Informacja o sukcesie
                        MessageBox.Show("Dane zostały pomyślnie zaktualizowane w bazie danych.");
                    }
                    else
                    {
                        // Informacja o braku zmian
                        MessageBox.Show("Brak zmian do zapisania.");
                    }
                }
                else
                {
                    // Obsługa sytuacji, gdy użytkownik nie istnieje w bazie danych
                    MessageBox.Show("Błąd: Użytkownik nie znaleziony w bazie danych.");
                }
            }
        }
        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Ponowne ustawienie marginesów po zmianie tekstu
            UpdateRichTextBoxMargin(Imie);
            UpdateRichTextBoxMargin(Nazwisko);
            UpdateRichTextBoxMargin(Loginn);
            UpdateRichTextBoxMargin(Haslo);
            UpdateRichTextBoxMargin(Rola);
        }

        private void UpdateRichTextBoxMargin(RichTextBox richTextBox)
        {
            Paragraph paragraph = richTextBox?.Document?.Blocks?.FirstBlock as Paragraph;
            if (paragraph != null)
            {
                paragraph.Margin = new Thickness(30, 0, 0, 0);
            }
        }

        private void Dane()
        {
            using (var context = new SeaPortContext(new DbContextOptions<SeaPortContext>()))
            {
                var user = context.Users.FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    // Aktualizacja RichTextBox 'Imie'
                    Imie.Document.Blocks.Clear();
                    Paragraph imieParagraph = new Paragraph(new Run(user.Name));
                    imieParagraph.FontFamily = new FontFamily("Arial");
                    imieParagraph.FontSize = 16;
                    imieParagraph.Foreground = Brushes.Black; // Ustaw kolor tekstu
                    Imie.Document.Blocks.Add(imieParagraph);

                    // Aktualizacja RichTextBox 'Nazwisko'
                    Nazwisko.Document.Blocks.Clear();
                    Paragraph nazwiskoParagraph = new Paragraph(new Run(user.LastName));
                    nazwiskoParagraph.FontFamily = new FontFamily("Arial");
                    nazwiskoParagraph.FontSize = 16;
                    nazwiskoParagraph.Foreground = Brushes.Black; // Ustaw kolor tekstu
                    Nazwisko.Document.Blocks.Add(nazwiskoParagraph);

                    // Aktualizacja RichTextBox 'Loginn'
                    Loginn.Document.Blocks.Clear();
                    Paragraph loginnParagraph = new Paragraph(new Run(user.Login));
                    loginnParagraph.FontFamily = new FontFamily("Arial");
                    loginnParagraph.FontSize = 16;
                    loginnParagraph.Foreground = Brushes.Black; // Ustaw kolor tekstu
                    Loginn.Document.Blocks.Add(loginnParagraph);

                    // Aktualizacja RichTextBox 'Haslo'
                    Haslo.Document.Blocks.Clear();
                    Paragraph hasloParagraph = new Paragraph(new Run(user.Password));
                    hasloParagraph.FontFamily = new FontFamily("Arial");
                    hasloParagraph.FontSize = 16;
                    hasloParagraph.Foreground = Brushes.Black; // Ustaw kolor tekstu
                    Haslo.Document.Blocks.Add(hasloParagraph);

                    

                    // Przekształcenie roli na czytelny dla użytkownika format
                    string roleDisplay = user.UserRole == "Admin" ? "Administrator" : "Użytkownik";

                    // Aktualizacja RichTextBox 'Rola'
                    Rola.Document.Blocks.Clear();
                    Paragraph rolaParagraph = new Paragraph(new Run(roleDisplay));
                    rolaParagraph.Margin = new Thickness(30, 0, 0, 0); // Ustawienie marginesu
                    rolaParagraph.FontSize = 16;

                    // Dodanie Paragraph do FlowDocument
                    FlowDocument flowDocument = new FlowDocument(rolaParagraph);

                    // Ustawienie FlowDocument jako zawartości RichTextBox
                    Rola.Document = flowDocument;
                }
                else
                {
                    // Obsługa sytuacji, gdy użytkownik nie istnieje w bazie danych
                    MessageBox.Show("Błąd: Użytkownik nie znaleziony w bazie danych.");
                }
            }
        }


       

        private void MotywComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Sprawdź, czy coś jest wybrane
            if (MotywComboBox.SelectedItem != null)
            {
                // Pobierz obiekt ComboBoxItem
                ComboBoxItem selectedItem = (ComboBoxItem)MotywComboBox.SelectedItem;

                // Pobierz tekst z Content
                string wybranaOpcja = selectedItem.Content.ToString();

                // Zgłoś zdarzenie do okna głównego
                WyborZmieniony?.Invoke(this, wybranaOpcja);
            }
        }


    }
}
