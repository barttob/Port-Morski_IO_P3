﻿using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for planOperacje.xaml
    /// </summary>
    public partial class planOperacje : UserControl
    {
        private SeaPortContext context = new SeaPortContext();
        public planOperacje()
        {
            InitializeComponent();
            LoadData();
        }

        internal void LoadData()
        {
            var operations = context.Operacje
        .Include(operation => operation.Dock)
        .Include(operation => operation.Ship)        
        .ToList();
            datagridOperacje.ItemsSource = operations;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Models.Operacje Operacje)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Usunięcie rekordu bezpośrednio z bazy danych, pomijając EF Core
                        context.Database.ExecuteSqlRaw($"DELETE FROM SeaPort.dbo.Operacje WHERE id = {Operacje.Id}");

                        // Nie używamy context.SaveChanges(), ponieważ już wykonaliśmy operację DELETE

                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        // Obsługa błędu
                    }
                }
            }
        }


        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            Button? modifyButton = sender as Button;
            if (modifyButton != null)
            {
                // Retrieve the user object associated with the clicked button.
                Operacje? Operacje = modifyButton.Tag as Operacje;

                if (Operacje != null)
                {
                    // Show the user data in the modifyUser component.
                    planOperacjeModify.Visibility = Visibility.Visible;

                    // Set the DataContext of modifyUser to the user object.
                    planOperacjeModify.DataContext = Operacje;
                }
            }
        }
        private void DODAJ_Click(object sender, RoutedEventArgs e)
        {
            planOperacjeAdd.Visibility = Visibility.Visible;
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
            string exportFileName = $"ExportedPortOperationData{exportCounter}.{selectedFormat}";
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
                iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph("Plan operacji w porcie");
                header.Alignment = Element.ALIGN_CENTER;
                document.Add(header);

                // Dodaj pusty wiersz jako odstęp
                document.Add(new iTextSharp.text.Paragraph(" "));

                // Dodaj nagłówki kolumn
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;

                foreach (var column in datagridOperacje.Columns)
                {
                    if (column.Header != null)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.Header.ToString()));
                        table.AddCell(cell);
                    }
                }

                // Dodaj dane
                foreach (var item in datagridOperacje.Items)
                {
                    for (int i = 0; i < datagridOperacje.Columns.Count; i++)
                    {
                        if (datagridOperacje.Columns[i] is DataGridBoundColumn boundColumn)
                        {
                            var binding = boundColumn.Binding as Binding;
                            if (binding != null)
                            {
                                var propertyPath = binding.Path.Path;
                                var property = item.GetType().GetProperty(propertyPath);

                                // Pomijaj kolumny niebędące danymi, takie jak opcje (buttons)
                                if (propertyPath == "Operation" || propertyPath == "ShipId" || propertyPath == "DockId" || propertyPath == "Approved" || propertyPath == "Date")
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
                csvData.AppendLine("Plan operacji w porcie");

                // Dodaj nagłówki kolumn do CSV
                foreach (var column in datagridOperacje.Columns)
                {
                    if (column.Header != null)
                    {
                        csvData.Append($"{column.Header},");
                    }
                }

                csvData.AppendLine(); // Nowa linia po nagłówkach

                // Dodaj dane do CSV
                foreach (var item in datagridOperacje.Items)
                {
                    for (int i = 0; i < datagridOperacje.Columns.Count; i++)
                    {
                        if (datagridOperacje.Columns[i] is DataGridBoundColumn boundColumn)
                        {
                            var binding = boundColumn.Binding as Binding;
                            if (binding != null)
                            {
                                var propertyPath = binding.Path.Path;
                                var property = item.GetType().GetProperty(propertyPath);

                                // Pomijaj kolumny niebędące danymi, takie jak opcje (buttons)
                                if (propertyPath == "Operation" || propertyPath == "ShipId" || propertyPath == "DockId" || propertyPath == "Approved" || propertyPath == "Date")
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
