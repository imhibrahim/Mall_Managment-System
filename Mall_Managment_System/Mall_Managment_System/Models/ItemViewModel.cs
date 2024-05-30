﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mall_Managment_System.Models
{
    public class ItemViewModel
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Shop")] // Change this to match the navigation property name
        public int ShopId { get; set; }

        public Shops Shop { get; set; } // Navigation property

        [Column("Item_Name", TypeName = "varchar(100)")]
        [Required]
        public string ItemName { get; set; }

        [Column("Description", TypeName = "varchar(max)")]
        [Required]
        public string Description { get; set; }

        [Column("Item_Image", TypeName = "varchar(100)")]
        [Required]
        public IFormFile Photo { get; set; }

        [Column("Price", TypeName = "int")]
        [Required]
        public int Price { get; set; }
    }
}
