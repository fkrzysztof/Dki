using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sasso.Data.Data.Data
{
    public class Partners
    {
        [Key]
        public int PartnersID { get; set; }
        //[Display(Name ="Adres www")]
        //[DataType(DataType.Url)]
        //public string URL { get; set; }

        [NotMapped]
        public IFormFile FormFileItem { get; set; }
        public string MediaItem { get; set; }

        public int? AboutID { get; set; }
        [ForeignKey("AboutID")]
        public About About { get; set; }
    }
}
