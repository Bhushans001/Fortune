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
        [MaxLength(100,ErrorMessage = "Name is neccessary")]
        [RegularExpression("^[a-z A-Z]*$", ErrorMessage = "The Category Name can only contain letters.")]
        public string Name { get; set; }
        [DisplayName("Display Number")]
        [Range(1,100,ErrorMessage = "the number should be between 1 - 100")]
        public int DisplayNumber { get; set; }
    }
}
