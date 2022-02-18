using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sasso.Data.Data.Data
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }

        public string Name { get; set; }

        [Display(Name = "Siedziba główna")]
        public bool HeadOffice { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Kraj")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Numer")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Kod")]
        public string ZIPCode { get; set; }

        public int? ContactID { get; set; }
        [ForeignKey("ContactID")]
        public Contact Contact { get; set; }

        public ICollection<Phone> Phones { get; set; }
        public ICollection<Email> Emails { get; set; }
    }
}
