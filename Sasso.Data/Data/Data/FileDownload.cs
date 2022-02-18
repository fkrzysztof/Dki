using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sasso.Data.Data.Data
{
    public class FileDownload
    {
        [Key]
        public int FileID { get; set; }
        public string Path { get; set; }

        public int? ProjectsID { get; set; }
        [ForeignKey("ProjectsID")]
        public Projects Projects { get; set; }

        [NotMapped]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.doc|.docx|.pdf)$", ErrorMessage = "akceptowalne fomaty to: .doc, .docx, .pdf")]
        public IFormFile FileItem { get; set; }
    }
}