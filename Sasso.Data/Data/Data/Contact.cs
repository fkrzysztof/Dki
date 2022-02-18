using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sasso.Data.Data.Data
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        public string NIP { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        public string REGON { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        public string KRS { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
