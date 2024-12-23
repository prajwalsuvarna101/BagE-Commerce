﻿using System.ComponentModel.DataAnnotations;

namespace Bag_E_Commerce.Models
{
    public class BagModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? name { get; set; }

        [Required]
        public string? description { get; set; }

        [Required]
        public decimal price { get; set; }

        [Required]
        public int quantity { get; set; }

        // Foreign key to CategoryModel
        [Required]
        public int CategoryId { get; set; }

        // Navigation property to CategoryModel
        public CategoryModel? category { get; set; }

        // Foreign key to VendorModel
        [Required]
        public int VendorId { get; set; }

        // Navigation property to VendorModel
        public VendorModel? vendor { get; set; }
    }
}
