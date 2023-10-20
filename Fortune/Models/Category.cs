using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fortune.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Number")]
        public int DisplayNumber { get; set; }
    }
}
