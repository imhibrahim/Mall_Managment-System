using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mall_Managment_System.Models
{
	public class Feedback
	{
		[Key]
		public int Id { get; set; }

		[Column("Environment", TypeName = "varchar(200)")]
		public string Environment { get; set; }

		[Column("Rating", TypeName = "int")]
		[Required]
		public int Rating { get; set; }

		[Column("Message", TypeName = "varchar(max)")]
		[Required]
		public string Message { get; set; }

		[Column("FeedbackDate")]
		[Required]
		public DateTime FeedbackDate { get; set; }

	

	
	}
}
