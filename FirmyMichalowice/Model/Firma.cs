using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Model
{
    public class Firma
    {
        public string id { get; set; }
        public string nazwa { get; set; }
        public AdresDzialanosci adresDzialanosci { get; set; }
        public AdresKorespondencyjny adresKorespondencyjny { get; set; }
        public Wlasciciel wlasciciel { get; set; }
        public IList<Obywatelstwo> obywatelstwa { get; set; }
        public string[] pkd { get; set; }
        public string pkdGlowny { get; set; }
        public IList<Spolka> spolki { get; set; }
        //[DataType(DataType.Date)]
        public string dataRozpoczecia { get; set; }
        //[DataType(DataType.Date)]
        public string dataZawieszenia { get; set; }
        public string status { get; set; }
        public string link { get; set; }
    }

    public class Spolka
    {
        //[DataType(DataType.Date)]
        public string dataZawieszenia { get; set; }
        public string nip { get; set; }
        public string regon { get; set; }

    }

    public class Obywatelstwo
    {
        public string symbol { get; set; }
        public string kraj { get; set; }
    }
    public class Wlasciciel
    {
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string nip { get; set; }
        public string regon { get; set; }
    }

    public class AdresKorespondencyjny: AdresDzialanosci
    {
        //public string ulica { get; set; }
        //public string budynek { get; set; }
        //public string miasto { get; set; }
        //public string wojewodztwo { get; set; }
        //public string powiat { get; set; }
        //public string gmina { get; set; }
        //public string kraj { get; set; }
        //public string kod { get; set; }
    }

    public class AdresDzialanosci
    {
        public string ulica { get; set; }
        public string budynek { get; set; }
        public string miasto { get; set; }
        public string wojewodztwo { get; set; }
        public string powiat { get; set; }
        public string gmina { get; set; }
        public string kraj { get; set; }
        public string kod { get; set; }
    }
}
