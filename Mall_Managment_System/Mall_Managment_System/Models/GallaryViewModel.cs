using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mall_Managment_System.Models
{
    public class GallaryViewModel
    {

        [Key] 
        public int Id { get; set; }
        [Required]
        [Column("Name", TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column("Image", TypeName = "varchar(100)")]
  
        public IFormFile Photo { get; set; }
    }
}
