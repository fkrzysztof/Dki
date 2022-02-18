using System.ComponentModel.DataAnnotations;

namespace Sasso.Data.Data.Data
{
    public class ProjectsPage
    {
        [Key]
        public int ProjectsPageId { get; set; }
        public string Text { get; set; }
    }
}
