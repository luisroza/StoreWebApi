using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopifyChallenge.Catalog.Application.ViewModels
{
    public class ProductImageViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime CreateDate { get; set; }

        public ProductViewModel Product { get; set; }

        public IEnumerable<ProductViewModel> ProductList { get; set; }
    }
}
