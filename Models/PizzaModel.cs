using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    [Table("Pizza")]
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? PhotoPath { get; set; }
        [NotMapped()]
        public IFormFile? File { get; set; } = null; 

    }

    public class PizzaList
    {
        public List<Pizza> FoodMenu { get; set; }
        public PizzaList(){
            FoodMenu = new List<Pizza>();
        }
    }
}
