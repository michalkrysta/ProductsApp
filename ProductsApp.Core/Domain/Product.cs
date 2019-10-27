﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsApp.Core.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }

        public string Category { get; set; }
    }
}