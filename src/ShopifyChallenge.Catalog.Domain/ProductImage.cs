using ShopifyChallenge.Core.DomainObjects;
using System;

namespace ShopifyChallenge.Catalog.Domain
{
    public class ProductImage : Entity
    {
        public Guid ProductId { get; set; }
        public DateTime CreateDate { get; private set; }

        public Product Product { get; set; }

        // EF Relation
        protected ProductImage() { }

        public ProductImage(Guid productId, DateTime createDate)
        {
            ProductId = productId;
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
        }
    }
}
