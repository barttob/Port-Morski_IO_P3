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
using Dock = Port_Morski.Models.Dock;
using Microsoft.EntityFrameworkCore;

namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy admPorty.xaml
    /// </summary>
    public partial class admPorty : UserControl
    {
        public SeaPortContext context = new SeaPortContext();
        public admPorty()
        {

            InitializeComponent();
            LoadData();

        }
        public void LoadData()
        {
            var docks = context.Docks.ToList();
            datagridPorty.ItemsSource = docks;
            datagridPorty.Loaded += DatagridPorty_Loaded; //<<---- obliczanie dopiero po załadowaniu kontrolki datagrid

        }
        private void DatagridPorty_Loaded(object sender, RoutedEventArgs e)
        {

            ObliczanieMagazynow();
            OblicznieTerminali();
        }


        public void ObliczanieMagazynow()
        {
            foreach (var item in datagridPorty.Items)
            {
                if (item is Dock dock)
                {
                    int dockId = dock.Id;
                    int iloscMagazynow = context.Magazines.Count(m => m.DockId == dockId);
                    var column = datagridPorty.Columns[3];

                    if (column != null)
                    {
                        var cellContent = column.GetCellContent(item) as TextBlock;
                        if (cellContent != null)
                        {
                            cellContent.Text = iloscMagazynow.ToString();
                        }
                    }
                }
            }




        }


        private void OblicznieTerminali()
        {
            foreach (var item in datagridPorty.Items)
            {
                if (item is Dock dock)
                {
                    int dockId = dock.Id;

                    // Pobierz ilość wszystkich terminali
                    int iloscMagazynow = context.Terminals.Count(t => t.DockId == dockId);

                    // Pobierz ilość zajętych terminali
                    int iloscZajetychTerminali = context.Terminals.AsEnumerable().Count(t => t.DockId == dockId && Convert.ToInt32(t.Available) == 0);



                    var column = datagridPorty.Columns[4];

                    if (column != null)
                    {
                        var cellContent = column.GetCellContent(item) as TextBlock;
                        if (cellContent != null)
                        {
                            // Ustaw tekst w formie "iloscMagazynow/iloscZajetychTerminali"
                            cellContent.Text = $"{iloscMagazynow}/{iloscZajetychTerminali}";
                        }
                    }
                }
            }


        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Models.Dock docks)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Docks.Remove(docks);
                    context.SaveChanges();
                    LoadData();
                }
            }
        }


        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            Dock selectedDock = (sender as Button)?.Tag as Dock;


            modyfikujPort userControl = new modyfikujPort();
            userControl.Id.Text = selectedDock.Id.ToString();
            userControl.Nazwa.Text = selectedDock.Name;

            mainGrid.Children.Add(userControl);
            userControl.LoadMagazinesData();

        }

        private void DODAJ_Click(object sender, RoutedEventArgs e)
        {
            dodajPort.Visibility = Visibility.Visible;
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
            string exportFileName = $"ExportedDockData{exportCounter}.{selectedFormat}";
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
                iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph("Lista dokow portu");
                header.Alignment = Element.ALIGN_CENTER;
                document.Add(header);

                // Dodaj pusty wiersz jako odstęp
                document.Add(new iTextSharp.text.Paragraph(" "));

                // Dodaj nagłówki kolumn
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;

                foreach (var column in datagridPorty.Columns)
                {
                    if (column.Header != null)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.Header.ToString()));
                        table.AddCell(cell);
                    }
                }

                // Dodaj dane
                foreach (var item in datagridPorty.Items)
                {
                    for (int i = 0; i < datagridPorty.Columns.Count; i++)
                    {
                        if (datagridPorty.Columns[i] is DataGridBoundColumn boundColumn)
                        {
                            var binding = boundColumn.Binding as Binding;
                            if (binding != null)
                            {
                                var propertyPath = binding.Path.Path;
                                var property = item.GetType().GetProperty(propertyPath);

                                // Pomijaj kolumny niebędące danymi, takie jak opcje (buttons)
                                if (propertyPath == "Name")
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
                csvData.AppendLine("Lista dokow portu");

                // Dodaj nagłówki kolumn do CSV
                foreach (var column in datagridPorty.Columns)
                {
                    if (column.Header != null)
                    {
                        csvData.Append($"{column.Header},");
                    }
                }

                csvData.AppendLine(); // Nowa linia po nagłówkach

                // Dodaj dane do CSV
                foreach (var item in datagridPorty.Items)
                {
                    for (int i = 0; i < datagridPorty.Columns.Count; i++)
                    {
                        if (datagridPorty.Columns[i] is DataGridBoundColumn boundColumn)
                        {
                            var binding = boundColumn.Binding as Binding;
                            if (binding != null)
                            {
                                var propertyPath = binding.Path.Path;
                                var property = item.GetType().GetProperty(propertyPath);

                                // Pomijaj kolumny niebędące danymi, takie jak opcje (buttons)
                                if (propertyPath == "Name")
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

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            {
                Dock? selectedDock = (sender as Button)?.Tag as Dock;


                infoDok infoControl = new infoDok();
                infoControl.ID.Text = selectedDock.Id.ToString();
                infoControl.Nazwa.Text = "Port: " + selectedDock.Name;

                mainGrid.Children.Add(infoControl);
                infoControl.LoadMagazinesData();
            }
        }
    }
}
