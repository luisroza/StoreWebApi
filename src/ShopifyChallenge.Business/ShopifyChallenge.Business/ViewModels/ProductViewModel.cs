﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopifyChallenge.Catalog.Application.ViewModel
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{1} is the minimum value for {0}")]
        [Required(ErrorMessage = "{0} is required")]
        public int StockQuantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{1} is the minimum value for {0}")]
        [Required(ErrorMessage = "{0} is required")]
        public int Height { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{1} is the minimum value for {0}")]
        [Required(ErrorMessage = "{0} is required")]
        public int Width { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{1} is the minimum value for {0}")]
        [Required(ErrorMessage = "{0} is required")]
        public int Depth { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
