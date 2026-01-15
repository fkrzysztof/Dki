using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sasso.Data.Data.Data
{
    public class About
    {
        [Key]
        public int AboutID { get; set; }
        [Display(Name = "Temat (np: O firmie)")]
        public string Maintext { get; set; }
        [Display(Name = "Rozwinięcie")]
        public string Text { get; set; }

        public ICollection<Partners> Partners { get; set; }

    }
}
