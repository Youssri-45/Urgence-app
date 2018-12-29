using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;

namespace App10
{
   public class Places
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }

        public Places(string name, string latitude, string longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
           
        }
       
    }
}
