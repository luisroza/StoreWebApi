using ShopifyChallenge.Core.DomainObjects;
using System;

namespace ShopifyChallenge.Catalog.Domain
{
    public class ProductImage : Entity
    {
        public Guid ProductId { get; set; }
        public DateTime CreateDate { get; private set; }
        public string Name { get; set; }

        public Product Product { get; set; }

        // EF Relation
        protected ProductImage() { }

        public ProductImage(Guid productId, string imageName, DateTime createDate)
        {
            ProductId = productId;
            Name = imageName;
            CreateDate = createDate;

            Validate();
        }

        public override string ToString()
        {
            return $"{ProductId} - {CreateDate}";
        }

        public void Validate()
        {
            AssertionConcern.AssertArgumentEquals(ProductId, Guid.Empty, "ProductId cannot be empty");
            AssertionConcern.AssertArgumentNotEmpty(Name,  "Images' name cannot be empty");
        }
    }
}
