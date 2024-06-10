using System.ComponentModel.DataAnnotations;

namespace Mall_Managment_System.Models
{
	public class Users
	{
		[Required]
		[Key]
        public int Userid { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

		[Required]
		[StringLength(50)]
		public string LasttName { get; set; }


		[Required]
		[StringLength(50)]
		public string Email { get; set; }

		[Required]
		[StringLength(20)]
		public string PhoneNumber { get; set; }

        [Required]
        [StringLength(10)]
		public string Password { get; set; }


		public string UserActive { get; set; }

		public string UserLogin { get; set; } = "0";


        public string Rolls { get; set; }



    }
}
