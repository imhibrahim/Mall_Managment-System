using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mall_Managment_System.Models
{
    public class Gallary
    {
        [Key] public int Id { get; set; }
        [Required]
        [Column("Name", TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column("Image", TypeName = "varchar(100)")]
        [Required]
        public string Image { get; set; }
    }
}
