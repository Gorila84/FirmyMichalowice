using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Model
{
    public class GeolocationFromGoogle
    {
        public GeolocationFromGoogle()
        {

        }
        public IList<AddressComponents> address_components { get; set; } 
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
    }

    public class Geometry
    {
        public latLng location { get; set; }
    }


    public class AddressComponents
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public IList<string> types { get; set; }
    }
}
