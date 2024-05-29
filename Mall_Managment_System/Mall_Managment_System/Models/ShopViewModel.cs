using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mall_Managment_System.Models
{
    public class ShopViewModel
    {
        [Key]
        public int ID { get; set; }
        [Column("Shop_Name", TypeName = "varchar(100)")]
        [Required]
        public string Name { get; set; }
        [Column("Shop_Description", TypeName = "varchar(max)")]
        [Required]
        public string Description { get; set; }
        [Column("Shop_Image", TypeName = "varchar(100)")]
        [Required]
        public IFormFile Photo { get; set; }
    }
}
