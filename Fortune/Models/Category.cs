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
        [MaxLength(10,ErrorMessage = "Name is neccessary")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "The Category Name can only contain letters.")]
        public string Name { get; set; }
        [DisplayName("Display Number")]
        [Range(1,100,ErrorMessage = "custom validation")]
        public int DisplayNumber { get; set; }
    }
}
