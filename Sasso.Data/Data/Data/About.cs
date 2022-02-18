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
        [Display(Name = "Tekst główny")]
        public string Maintext { get; set; }
        [Display(Name = "O firmie")]
        public string Text { get; set; }

        public ICollection<Partners> Partners { get; set; }

    }
}
