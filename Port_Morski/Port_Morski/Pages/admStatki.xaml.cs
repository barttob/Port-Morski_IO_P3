﻿using iTextSharp.text.pdf;
using iTextSharp.text;
using Port_Morski.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logika interakcji dla klasy admStatki.xaml
    /// </summary>
    public partial class admStatki : UserControl
    {
        private SeaPortContext context = new SeaPortContext();

        public admStatki()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            var ships = context.Ships.ToList();
            datagridStatki.ItemsSource = ships;
        }
        internal void Refresh()
        {
            context.SaveChanges();
            var ship = context.Ships.ToList();
            datagridStatki.ItemsSource = ship; ;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Ship ship)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Ships.Remove(ship);
                    context.SaveChanges();
                    LoadData();
                }
            }

        }
        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            Button? modifyButton = sender as Button;
            if (modifyButton != null)
            {
                // Retrieve the user object associated with the clicked button.
                Ship? ship = modifyButton.Tag as Ship;

                if (ship != null)
                {
                    // Show the user data in the modifyUser component.
                    modyfikujStatek.Visibility = Visibility.Visible;

                    // Set the DataContext of modifyUser to the user object.
                    modyfikujStatek.DataContext = ship;
                }
            }
        }
        private void DODAJ_Click(object sender, RoutedEventArgs e)
        {
            dodajStatek.Visibility = Visibility.Visible;
        }

        private int exportCounter = 1;

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz wybrany format
            string selectedFormat = (exportFormatComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            // Ustal stałą lokalizację, np. folder "Exporty" w folderze projektu
            string exportFolderPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Exporty");

            // Sprawdź, czy folder istnieje; jeśli nie, utwórz go
            if (!System.IO.Directory.Exists(exportFolderPath))
            {
                System.IO.Directory.CreateDirectory(exportFolderPath);
            }

            // Utwórz pełną ścieżkę do pliku eksportowanego
            string exportFileName = $"ExportedShipData{exportCounter}.{selectedFormat}";
            string exportFilePath = System.IO.Path.Combine(exportFolderPath, exportFileName);

            // Zwiększ licznik dla następnych eksportów
            exportCounter++;

            // Wybierz odpowiednią funkcję eksportu w zależności od wybranego formatu
            switch (selectedFormat.ToLower())
            {
                case "pdf":
                    ExportDataToPdf(exportFilePath);
                    break;
                case "csv":
                    ExportDataToCsv(exportFilePath);
                    break;
                // Dodaj obsługę innych formatów, jeśli potrzebujesz
                default:
                    MessageBox.Show("Nieobsługiwany format eksportu.");
                    break;
            }
        }

        private void ExportDataToPdf(string filePath)
        {
            try
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // Utwórz styl dla nagłówka
                iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);

                // Dodaj nagłówek "Dane użytkowników"
                iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph("Dane zarejestrowanych statkow");
                header.Alignment = Element.ALIGN_CENTER;
                document.Add(header);

                // Dodaj pusty wiersz jako odstęp
                document.Add(new iTextSharp.text.Paragraph(" "));

                // Dodaj nagłówki kolumn
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;

                foreach (var column in datagridStatki.Columns)
                {
                    if (column.Header != null)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.Header.ToString()));
                        table.AddCell(cell);
                    }
                }

                // Dodaj dane
                foreach (var item in datagridStatki.Items)
                {
                    for (int i = 0; i < datagridStatki.Columns.Count; i++)
                    {
                        if (datagridStatki.Columns[i] is DataGridBoundColumn boundColumn)
                        {
                            var binding = boundColumn.Binding as Binding;
                            if (binding != null)
                            {
                                var propertyPath = binding.Path.Path;
                                var property = item.GetType().GetProperty(propertyPath);

                                // Pomijaj kolumny niebędące danymi, takie jak opcje (buttons)
                                if (propertyPath == "Name" || propertyPath == "Type" || propertyPath == "Capacity")
                                {
                                    PdfPCell cell = new PdfPCell(new Phrase(property?.GetValue(item)?.ToString() ?? " "));
                                    table.AddCell(cell);
                                }
                            }
                        }
                    }
                }

                document.Add(table);
                document.Close();

                string fullPath = System.IO.Path.GetFullPath(filePath);
                MessageBox.Show($"Eksport do PDF zakończony pomyślnie! Plik zapisano w: {fullPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas eksportu do PDF: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportDataToCsv(string filePath)
        {
            try
            {
                // Pobierz dane
                StringBuilder csvData = new StringBuilder();

                // Dodaj nagłówek "Dane użytkowników systemu"
                csvData.AppendLine("Dane zarejestrowanych statkow");

                // Dodaj nagłówki kolumn do CSV
                foreach (var column in datagridStatki.Columns)
                {
                    if (column.Header != null)
                    {
                        csvData.Append($"{column.Header},");
                    }
                }

                csvData.AppendLine(); // Nowa linia po nagłówkach

                // Dodaj dane do CSV
                foreach (var item in datagridStatki.Items)
                {
                    for (int i = 0; i < datagridStatki.Columns.Count; i++)
                    {
                        if (datagridStatki.Columns[i] is DataGridBoundColumn boundColumn)
                        {
                            var binding = boundColumn.Binding as Binding;
                            if (binding != null)
                            {
                                var propertyPath = binding.Path.Path;
                                var property = item.GetType().GetProperty(propertyPath);

                                // Pomijaj kolumny niebędące danymi, takie jak opcje (buttons)
                                if (propertyPath == "Name" || propertyPath == "Type" || propertyPath == "Capacity")
                                {
                                    csvData.Append($"{property?.GetValue(item)?.ToString()},");
                                }
                            }
                        }
                    }

                    csvData.AppendLine(); // Nowa linia po każdym wierszu danych
                }

                // Zapisz do pliku CSV
                File.WriteAllText(filePath, csvData.ToString());

                string fullPath = System.IO.Path.GetFullPath(filePath);
                MessageBox.Show($"Eksport do CSV zakończony pomyślnie! Plik zapisano w: {fullPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas eksportu do CSV: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
