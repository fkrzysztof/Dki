using Microsoft.AspNetCore.Http;
using Sasso.Data.Data.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sald.Data.Data.Data
{
    public class Offer
    {
        [Key]
        public int OfferID { get; set; }
        public string Name { get; set; }
        public string LinkName { get; set; }
        public string Text { get; set; }
        public string TextMain { get; set; }
        //IMG
        [NotMapped]
        public IFormFile FormFileItem { get; set; }
        public MyFile Image { get; set; }

    }

}
