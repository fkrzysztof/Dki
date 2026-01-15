using Microsoft.AspNetCore.Http;
using Sasso.Data.Data.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Sald.Data.Data.Data
{
    public class Apartment
    {
        [Key]
        public int ApartmentID { get; set; }

            //Opis
            public string Opis { get; set; }
            public string Nazwa { get; set; }

            // Lokalizacja w budynku
            public int Pietro { get; set; }
            public int LiczbaPieterWBudynku { get; set; }

            // Metraż i układ
            public double Metraz { get; set; }
            public int LiczbaPokoi { get; set; }

            // Łazienka / WC
            public bool WcRazemZLazienka { get; set; }

            // Udogodnienia w mieszkaniu
            public bool Balkon { get; set; }
            public bool Winda { get; set; }
            public bool Piwnica { get; set; }
            public bool OgrzewaniePodlogowe { get; set; }
            public bool Klimatyzacja { get; set; }

            // Garaż / Parking
            public bool Garaz { get; set; }
            public bool MiejsceParkingoweNaZewnatrz { get; set; }
            public bool Ogrod { get; set; }
            public bool Taras { get; set; }

            // Dane adresowe
            public string Ulica { get; set; }
            public string NumerBudynku { get; set; }
            public string NumerMieszkania { get; set; }
            public string Miasto { get; set; }
            public string KodPocztowy { get; set; }
            public string Kraj { get; set; }

            [NotMapped]
            public string PelnyAdres
            {
                get
                {
                    return $"{Ulica} {NumerBudynku}/{NumerMieszkania}, {KodPocztowy} {Miasto}, {Kraj}";
                }
            }

        // Kontakt
        public string Email { get; set; }
            public string Telefon1 { get; set; }
            public string Telefon2{ get; set; }


        //IMG
        [NotMapped]
        public List<IFormFile> FormFileItems { get; set; }
        public List<MyFile> Photos { get; set; } = new List<MyFile>();
    }
}
