using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mall_Managment_System.Models
{
    public class Foodcourt
    {
        [Key]
        public int ID { get; set; }
        [Column("FoodCourt_Name", TypeName = "varchar(100)")]
        [Required]
        public string Name { get; set; }
        [Column("Description", TypeName = "varchar(max)")]
        [Required]
        public string Description { get; set; }
        [Column("Image", TypeName = "varchar(100)")]
      
        public string Image { get; set; }
    }
}
