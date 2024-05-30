using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mall_Managment_System.Models
{
    public class Movies
    {
        [Key]
        public int Id { get; set; }
        [Column("Movie_Name", TypeName = "varchar(100)")]
        [Required]
        public string MovieName { get; set; }
        [Column("Description", TypeName = "varchar(max)")]
        [Required]
        public string Description { get; set; }
        [Column("Movie_Image", TypeName = "varchar(100)")]
        [Required]
        public string Image { get; set; }
        [Column("TotalSeats", TypeName = "int")]
        [Required]
        public int TotalSeats { get; set; }
        [Column("AvailableSeats", TypeName = "int")]
        [Required]
        public int AvailableSeats { get; set; }
    }
}
