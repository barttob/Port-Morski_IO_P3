using LiveCharts.Wpf;
using LiveCharts;
using Port_Morski.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Port_Morski
{
    class StatusyLadunkowModel
    {
        public SeriesCollection SeriesCollection { get; set; }

        public string[] Statuses { get; set; }


        public StatusyLadunkowModel() 
        {
            
        }
    }
}
