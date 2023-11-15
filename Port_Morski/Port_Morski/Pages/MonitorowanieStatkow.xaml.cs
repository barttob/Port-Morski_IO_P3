using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MigraDoc.DocumentObjectModel.Shapes;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Port_Morski.Models;
using Syncfusion.UI.Xaml.Maps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy MonitorowanieStatkow.xaml
    /// </summary>
    public partial class MonitorowanieStatkow : UserControl
    {
        public MonitorowanieStatkow()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            using (var context = new SeaPortContext())
            {
                var ships = context.Ships.ToList();
                //datagridStatkiFront.ItemsSource = ships;
                

                // Populate ship coordinates in ViewModel using ShipCoordinates class
                ViewModel view = new ViewModel(ships);
                datagridStatkiFront.ItemsSource = view.ShipModels;
                this.DataContext = view;
                // Bind ship coordinates to the map
                SfMap maps = new SfMap();
                ImageryLayerExt shape = new ImageryLayerExt();
                shape.Markers = view.Models;
                maps.Layers.Add(shape);
                shape.Center = new Point(55.5, 18); // Set default center for the map
                shape.Radius = 25;
                shape.DistanceType = DistanceType.KiloMeter;
                this.mapa = maps; // Assuming 'mapa' is your map control
            }
        }


    }

    public class ViewModel
    {
        public ObservableCollection<CoordinatesModel> Models { get; set; }
        public ObservableCollection<ShipModel> ShipModels { get; set; }
        public ViewModel(List<Ship>? ships)
        {
            this.Models = new ObservableCollection<CoordinatesModel>();
            this.ShipModels = new ObservableCollection<ShipModel>();
            Random random = new Random();

            // Generate random coordinates around 55 and 18 latitude and longitude
            double baseLatitude = 55.5;
            double baseLongitude = 18.0;

            foreach (var ship in ships)
            {
                double randomLatOffset = (random.NextDouble() - 0.5) * 100.0 / 111.0; // Approximately 1 degree is around 111 kilometers
                double randomLongOffset = (random.NextDouble() - 0.5) * 100.0 / (111.0 * Math.Cos(baseLatitude * Math.PI / 180.0)); // Correct for longitude based on latitude

                double newLatitude = baseLatitude + randomLatOffset;
                double newLongitude = baseLongitude + randomLongOffset;

                string latitudeDirection = (newLatitude >= 0) ? "N" : "S";
                string longitudeDirection = (newLongitude >= 0) ? "E" : "W";

                this.Models.Add(new CoordinatesModel()
                {
                    Label = ship.Name,
                    Latitude = $"{Math.Abs(newLatitude):0.####}{latitudeDirection}",
                    Longitude = $"{Math.Abs(newLongitude):0.####}{longitudeDirection}"
                });

                ShipModel newShipModel = new ShipModel
                {
                    Id = ship.Id,
                    Name = ship.Name,
                    Latitude = $"{Math.Abs(newLatitude):0.####}{latitudeDirection}",
                    Longitude = $"{Math.Abs(newLongitude):0.####}{longitudeDirection}",
                    Type = ship.Type,
                    Capacity = ship.Capacity
                };

                this.ShipModels.Add(newShipModel);

            }
        }
    }

    public class CoordinatesModel
    {
        public string Label { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }

    public class ShipModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string? Type { get; set; }
        public int? Capacity { get; set; }
    }



    public class ImageryLayerExt : ImageryLayer
    {
        protected override string GetUri(int X, int Y, int Scale)
        {
            var link = "http://mt1.google.com/vt/lyrs=y&x=" + X.ToString() + "&y=" + Y.ToString() + "&z=" + Scale.ToString();
            return link;
        }
    }


}
