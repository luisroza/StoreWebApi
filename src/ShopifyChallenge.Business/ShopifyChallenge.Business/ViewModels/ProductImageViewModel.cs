using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Catalog.Application.ViewModels
{
    public class ProductImageViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        public string FileBase { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
