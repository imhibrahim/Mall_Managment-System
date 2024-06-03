using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mall_Managment_System.Models
{
    public class FooditemViewModel
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Foodcourt")] // Change this to match the navigation property name
        public int FoodCourt_id { get; set; }

        public Foodcourt foodcourt { get; set; } // Navigation property

        [Column("Food_Name", TypeName = "varchar(100)")]
        [Required]
        public string Name { get; set; }

        [Column("Description", TypeName = "varchar(max)")]
        [Required]
        public string Description { get; set; }

        [Column("Price", TypeName = "int")]
        [Required]
        public int Price { get; set; }

        [Column("Food_Image", TypeName = "varchar(100)")]
        [Required]
        public IFormFile Photo { get; set; }
    }
}
