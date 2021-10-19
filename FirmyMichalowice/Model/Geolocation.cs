using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Model
{
    public class Geolocation
    {
      public ProvidedLocation providedLocation { get; set; }
     
        public IList<Location> locations { get; set; }
        

    }

    public class Location
    {
        public string street { get; set; }
        public string adminArea6 { get; set; }
        public string adminArea6Type { get; set; }
        public string adminArea5 { get; set; }
        public string adminArea5Type { get; set; }
        public string adminArea4 { get; set; }
        public string adminArea4Type { get; set; }
        public string adminArea3 { get; set; }
        public string adminArea3Type { get; set; }
        public string adminArea1 { get; set; }
        public string adminArea1Type { get; set; }
        public string postalCode { get; set; }
        public string geocodeQualityCode { get; set; }
        public string geocodeQuality { get; set; }
        public bool dragPoint { get; set; }
        public char sideOfStreet { get; set; }
        public string linkId { get; set; }
        public string unknownInput { get; set; }
        public string type { get; set; }
        public latLng latLng { get; set; }
        public displayLatLng displayLatLng { get; set; }

        public string mapUrl { get; set; }
    }

    public class ProvidedLocation
    {
        public string location { get; set; }
    }

    public class displayLatLng: latLng
    {
    }

    public class latLng
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
    }
}
