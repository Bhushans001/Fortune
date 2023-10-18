using System.ComponentModel.DataAnnotations;

namespace Fortune.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }    
        public int DisplayNUmber { get; set; }
    }
}
