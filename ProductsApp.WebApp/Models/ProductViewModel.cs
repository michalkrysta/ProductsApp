﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsApp.WebApp.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Prompt = "123,45")]
        public decimal Cost { get; set; }

        [Required]
        public string Category { get; set; }
    }
}