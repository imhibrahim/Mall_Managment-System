using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mall_Managment_System.Models
{
	public class Booking
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("movies")]
		public int MovieId { get; set; }
		public Movies movies { get; set; } // Navigation property

		[ForeignKey("users")]
		public int UserId { get; set; }
		public Users users { get; set; } // Navigation property

		[Column("Booking_Date")]
		[Required]
		public DateTime Booking_Date { get; set; }

        [Column("Booking_sets")]
        [Required]
        public string Booking_sets { get; set; }

        [Column("Number_Tickets")]
		[Required]
		public int Number_Tickets { get; set; }
	}


}
