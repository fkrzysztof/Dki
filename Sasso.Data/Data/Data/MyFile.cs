using Microsoft.AspNetCore.Http;
using Sald.Data.Data.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sasso.Data.Data.Data
{
    public class MyFile
    {
        [Key]
        public int FileID { get; set; }
        public string Path { get; set; }

        [NotMapped]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.doc|.docx|.pdf)$", ErrorMessage = "akceptowalne fomaty to: .doc, .docx, .pdf")]
        public IFormFile FileItem { get; set; }
        public string Tag { get; set; }

        //******* LINK
        public int? OfferID { get; set; }
        [ForeignKey("OfferID")]
        public Offer Offer { get; set; }

        public int? SettingsID { get; set; }
        [ForeignKey("SettingsID")]
        public Settings Logo { get; set; }
        public int? ProjectsID { get; set; }
        [ForeignKey("ProjectsID")]
        public Projects Projects { get; set; }

        [ForeignKey("BackgroundList")]
        public int? BackgroundListID { get; set; }
        public Settings BackgroundList { get; set; }

        [ForeignKey("FilesCollection")]
        public int? FilesCollectionID { get; set; }
        public Projects FilesCollection { get; set; }

        public int? ApartmentID { get; set; }
        [ForeignKey("ApartmentID")]
        public Apartment Apartment { get; set; }

    }
}

