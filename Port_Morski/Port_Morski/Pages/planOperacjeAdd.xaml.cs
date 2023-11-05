﻿using MaterialDesignThemes.Wpf;
using System.Runtime.InteropServices;
using Port_Morski.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using static Port_Morski.Pages.planOperacje;

namespace Port_Morski.Pages
{
    /// <summary>
    /// Interaction logic for planDokiAdd.xaml
    /// </summary>
    public partial class planOperacjeAdd : UserControl
    {
        public planOperacjeAdd()
        {
            InitializeComponent();
            Btn_Exit.Click += Btn_Exit_Click;
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            // Your DbContext class, replace 'YourDbContext' with your actual class.
            using (var context = new SeaPortContext())
            {
                // Retrieve ships and docks from the database.
                List<Ship> ships = context.Ships.ToList();
                List<Models.Dock> docks = context.Docks.ToList();

                // Set the items source for the ComboBoxes.
                shipComboBox.ItemsSource = ships;
                dockComboBox.ItemsSource = docks;
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {

            this.Visibility = Visibility.Collapsed;
        }

        public void AddSS_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(shipComboBox.Text) || string.IsNullOrWhiteSpace(dockComboBox.Text) || string.IsNullOrWhiteSpace(Operacja.Text))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.");
                return;
            }

            using (var context = new SeaPortContext())
            {
                try
                {
                    var newOperation = new Operations
                    {
                        Operation = Operacja.Text,
                        ShipId = GetSelectedShipId(),
                        DockId = GetSelectedDockId(),
                        Approved = false,
                        Date = date.SelectedDate,
                    };

                    context.Operationss.Add(newOperation);
                    context.SaveChanges();

                    MessageBox.Show("Pomyślnie zarejestrowano operację do bazy danych.");

                    this.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wystąpił błąd podczas rejestracji operacji do bazy danych: " + ex.InnerException.Message);
                }
            }

        }

        private int GetSelectedShipId()
        {
            if (shipComboBox.SelectedItem is Ship selectedShip)
            {
                return selectedShip.Id;
            }

            return -1;
        }

        private int GetSelectedDockId()
        {
            if (dockComboBox.SelectedItem is Models.Dock selectedDock)
            {
                return selectedDock.Id;
            }

            return -1;
        }
    }
}