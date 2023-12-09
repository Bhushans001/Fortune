using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortunes.Models
{
     public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }    
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }    
        public string Author { get; set; }
        [Required]
        [DisplayName("List Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid price")]
        public double ListPrice {  get; set; }  
        [Required]
        [DisplayName("List For 1 - 50")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid price")]

        public double Price {  get; set; }   
        [Required]
        [Range(0, int.MaxValue, ErrorMessage ="Please enter valid price")]
        [DisplayName("List For 50+")]
        public double Price50 {  get; set; } 
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid price")]

        [DisplayName("List For 100+")]
        public double List100 {  get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public Category category { get; set; }  
        public string ImageUrl { get; set; }
    }
}
