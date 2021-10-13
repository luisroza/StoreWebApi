using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Catalog.Application.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

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

        [Range(1, int.MaxValue, ErrorMessage = "{1} is the minimum value for {0}")]
        [Required(ErrorMessage = "{0} is required")]
        public int InventoryQuantity { get; set; }
        
        public List<ProductImageViewModel> ProductImages { get; set; }
    }
}
