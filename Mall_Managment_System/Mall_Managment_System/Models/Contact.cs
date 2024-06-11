using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mall_Managment_System.Models
{
    public class Contact
    {
        [Key]
        public int id { get; set; }
        [Column("Contact_Name", TypeName = "varchar(100)")]
        [Required]
        public string Name { get; set; }

        [Column("Contact_Email", TypeName = "varchar(100)")]
        [Required]
        public string Email { get; set; }

        [Column("Contact_Number", TypeName = "varchar(50)")]
        [Required]
        public string Number { get; set; }

        [Column("Contact_Massage", TypeName = "varchar(max)")]
        [Required]
        public string Massage { get; set; }

    }
}
