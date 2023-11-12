using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.EntityFrameworkCore;
using Port_Morski.Models;
using iText.IO.Font.Constants;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;



namespace Port_Morski.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy GenerowanieRaportow.xaml
    /// </summary>
    public partial class GenerowanieRaportow : UserControl
    {
        public GenerowanieRaportow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz dane z bazy danych przy użyciu Entity Framework
            List<Operations> operationsData;
            List<ShipSchedule> shipScheduleData;
            using (var context = new SeaPortContext())
            {
                operationsData = context.Operationss
                    .Select(o => new Operations
                    {
                        Id = o.Id,
                        Operation = o.Operation,
                        ShipId = o.ShipId,
                        DockId = o.DockId,
                        Approved = o.Approved,
                        Date = o.Date
                    })
                    .ToList();

                shipScheduleData = context.ShipSchedules
                    .Include(schedule => schedule.Ship)
                    .Include(schedule => schedule.Dock)
                    .ToList();
            }

            // Utwórz nowy plik PDF
            string pdfFilePath = Path.Combine(Environment.CurrentDirectory, "raporcik.pdf");
            using (var document = new PdfDocument())
            {
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Dodaj napis "Raport" czcionką o wielkości 50
                XFont titleFont = new XFont("Arial", 50, XFontStyle.Bold);
                float xTitlePosition = 200;
                float yTitlePosition = 60; // Zmieniono wartość Y
                gfx.DrawString("Raport", titleFont, XBrushes.Black, xTitlePosition, yTitlePosition);

                // Dodaj odstęp o wielkości 80
                float yPosition = yTitlePosition + 80; // Zmieniono wartość Y

                // Dodaj napis "Operacje"
                XFont titleFont2 = new XFont("Arial", 12, XFontStyle.Bold);
                float xTitlePosition2 = 10;
                float yTitlePosition2 = 100; // Zmieniono wartość Y
                gfx.DrawString("Operacje portowe", titleFont2, XBrushes.Black, xTitlePosition2, yTitlePosition2);

                // Dodaj odstęp o wielkości 40
                float yPosition2 = yTitlePosition2 + 40; // Zmieniono wartość Y

                // Dodaj nagłówki kolumn dla operacji
                XFont headerFont = new XFont("Arial", 12, XFontStyle.Bold);
                float xHeaderPosition = 10;
                gfx.DrawString("ID", headerFont, XBrushes.Black, xHeaderPosition, yPosition2);
                gfx.DrawString("Operation", headerFont, XBrushes.Black, xHeaderPosition + 80, yPosition2);
                gfx.DrawString("Ship ID", headerFont, XBrushes.Black, xHeaderPosition + 160, yPosition2);
                gfx.DrawString("Dock ID", headerFont, XBrushes.Black, xHeaderPosition + 240, yPosition2);
                gfx.DrawString("Approved", headerFont, XBrushes.Black, xHeaderPosition + 320, yPosition2);
                gfx.DrawString("Date", headerFont, XBrushes.Black, xHeaderPosition + 400, yPosition2);

                // Dodaj dane operacji do dokumentu PDF
                XFont font = new XFont("Arial", 12); // Ustawienie czcionki Arial
                yPosition2 += 25; // Początkowa pozycja Y dla zawartości tabeli

                foreach (var operation in operationsData)
                {
                    float xPosition = 10; // Początkowa pozycja X dla każdego wiersza

                    gfx.DrawString($"{operation.Id}", font, XBrushes.Black, xPosition, yPosition2);
                    xPosition += 80;

                    gfx.DrawString($"{operation.Operation}", font, XBrushes.Black, xPosition, yPosition2);
                    xPosition += 80;

                    gfx.DrawString($"{operation.ShipId}", font, XBrushes.Black, xPosition, yPosition2);
                    xPosition += 80;

                    gfx.DrawString($"{operation.DockId}", font, XBrushes.Black, xPosition, yPosition2);
                    xPosition += 80;

                    gfx.DrawString($"{operation.Approved}", font, XBrushes.Black, xPosition, yPosition2);
                    xPosition += 80;

                    gfx.DrawString($"{operation.Date}", font, XBrushes.Black, xPosition, yPosition2);

                    yPosition2 += 25; // Zwiększenie pozycji Y na potrzeby kolejnego wiersza
                }

                // Dodaj odstęp o wielkości 80
                float yPosition3 = yPosition2 + 80; // Zmieniono wartość Y

                // Dodaj napis "Rozkład Działania Portu"
                XFont titleFont3 = new XFont("Arial", 12, XFontStyle.Bold);
                float xTitlePosition3 = 10;
                gfx.DrawString("Rozkład Działania Portu", titleFont3, XBrushes.Black, xTitlePosition3, yPosition3);

                // Dodaj odstęp o wielkości 40
                float yPosition4 = yPosition3 + 40; // Zmieniono wartość Y

                // Dodaj nagłówki kolumn dla rozkładu działania portu
                gfx.DrawString("ID", headerFont, XBrushes.Black, xHeaderPosition, yPosition4);
                gfx.DrawString("Ship ID", headerFont, XBrushes.Black, xHeaderPosition + 80, yPosition4);
                gfx.DrawString("Dock ID", headerFont, XBrushes.Black, xHeaderPosition + 160, yPosition4);
                gfx.DrawString("Arrival Time", headerFont, XBrushes.Black, xHeaderPosition + 240, yPosition4);
                gfx.DrawString("Departure Time", headerFont, XBrushes.Black, xHeaderPosition + 320, yPosition4);

                // Dodaj dane rozkładu działania portu do dokumentu PDF
                yPosition4 += 25; // Początkowa pozycja Y dla zawartości tabeli

                foreach (var schedule in shipScheduleData)
                {
                    float xPosition = 10; // Początkowa pozycja X dla każdego wiersza

                    gfx.DrawString($"{schedule.Id}", font, XBrushes.Black, xPosition, yPosition4);
                    xPosition += 80;

                    gfx.DrawString($"{schedule.Ship}", font, XBrushes.Black, xPosition, yPosition4);
                    xPosition += 80;

                    gfx.DrawString($"{schedule.DockId}", font, XBrushes.Black, xPosition, yPosition4);
                    xPosition += 80;

                    //gfx.DrawString($"{schedule.ArrivalTime}", font, XBrushes.Black, xPosition, yPosition4);
                    //xPosition += 80;

                    //gfx.DrawString($"{schedule.DepartureTime}", font, XBrushes.Black, xPosition, yPosition4);

                    yPosition4 += 25; // Zwiększenie pozycji Y na potrzeby kolejnego wiersza
                }


                // Dodaj stopkę z datą i godziną generowania raportu
                XFont footerFont = new XFont("Arial", 8);
                string formattedDateTime = $"Wygenerowano dnia: {DateTime.Now.ToString("dd.MM.yyyy")} godzina: {DateTime.Now.ToString("HH:mm")}";
                gfx.DrawString(formattedDateTime, footerFont, XBrushes.Black, page.Width - 200, page.Height - 30);


                document.Save(pdfFilePath);
            }
            MessageBox.Show("Raport został zapisany do pliku PDF.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

            Console.WriteLine($"Raport został zapisany do pliku PDF: {pdfFilePath}");
        }


    }
}
