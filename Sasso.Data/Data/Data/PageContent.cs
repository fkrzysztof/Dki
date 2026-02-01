using Microsoft.AspNetCore.Http;
using Sald.Data.Data.Data;
using Sasso.Data.Data.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Engine.Data.Data.Data
{
    public class PageContent
    {
        [Key]
        public int Id { get; set; }
        public string PageKey { get; set; }   // np. "Home", "Apartment"
        public string Culture { get; set; }   // "pl-PL", "en-US"
        public string Title { get; set; }
        public string Description { get; set; }

        public string PdfContent { get; set; }  // kolumna do PDF convert

        public int? ApartmentID { get; set; }
        [ForeignKey("ApartmentID")]
        public Apartment Apartment { get; set; }


        
        [NotMapped]
        public IFormFile FormFileItems { get; set; }

        // PDF przechowywany w bazie jako obiekt MyFile
        public int? PdfFileId { get; set; }  // <-- jawny FK
        public MyFile PdfFile { get; set; }

    }
}
