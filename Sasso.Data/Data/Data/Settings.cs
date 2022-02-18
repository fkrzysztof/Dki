using Microsoft.AspNetCore.Http;
using Sasso.Data.Data.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sald.Data.Data.Data
{
    public class Settings
    {
        public int SettingsID { get; set; }
        [Display(Name = "Logo")]
        public MyFile Logo { get; set; }
        [NotMapped]
        public IFormFile LogoForm { get; set; }
        
        [InverseProperty("BackgroundList")]
        public ICollection<MyFile> Background { get; set; }
        [NotMapped]
        public IFormFile[] BackgroundForm { get; set; }
        public string CookieInfo { get; set; }
        public string HeadText { get; set; }
    }
}
