using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sasso.Data.Data.Data
{
    public class Email
    {
        [Key]
        public int EmailID { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string EmailAdress { get; set; }

        public int? AddressID { get; set; }
        [ForeignKey("AddressID")]
        public Address Address { get; set; }

    }
}
