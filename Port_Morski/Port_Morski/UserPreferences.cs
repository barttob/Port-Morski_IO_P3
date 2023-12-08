using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Port_Morski
{
    [Serializable]
    [XmlRoot("UserPreferences")]
    public class UserPreferences
    {
        public int UserId { get; set; }
        public string SelectedTheme { get; set; }
        public string SelectedSize { get; set; }
    }
}
