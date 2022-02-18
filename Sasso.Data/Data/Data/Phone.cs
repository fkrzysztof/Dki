using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sasso.Data.Data.Data
{
    public class Phone
    {
        [Key]
        public int PhoneID { get; set; }

        [Phone]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string NamePhone { get; set; }

        public int? AddressID { get; set; }
        [ForeignKey("AddressID")]
        public Address Address { get; set; }
    }
}
