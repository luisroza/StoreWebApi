using Store.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreateDate { get; private set; }
        public int InventoryQuantity { get; private set; }
        public List<ProductImage> ProductImages { get; set; }

        protected Product() { }
        public Product(string name, string description, bool active, decimal price, DateTime createDate)
        {
            Name = name;
            Description = description;
            Active = active;
            Price = price;
            CreateDate = createDate;
            ProductImages = new List<ProductImage>();

            Validate();
        }

        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

        public void ChangeDescription(string description)
        {
            AssertionConcern.AssertArgumentNotEmpty(description, "Description cannot not be empty");
            Description = description;
        }

        public void DecreaseInventory(int quantity)
        {
            if (quantity < 0) quantity *= -1;
            if (!HasInventory(quantity)) throw new DomainException("There are no items enough in the inventory");
            InventoryQuantity -= quantity;
        }

        public void AddInventory(int quantity)
        {
            InventoryQuantity += quantity;
        }

        public bool HasInventory(int quantity)
        {
            return InventoryQuantity >= quantity;
        }

        public void AddImage(ProductImage productImage)
        {
            ProductImages?.Add(productImage);
        }

        public void RemoveImage(ProductImage productImage)
        {
            ProductImages?.Remove(productImage);
        }

        public void Validate()
        {
            AssertionConcern.AssertArgumentNotEmpty(Name, "Name cannot be empty");
            AssertionConcern.AssertArgumentNotEmpty(Description, "Description cannot be empty");
            AssertionConcern.AssertArgumentLesserThan(Price, 1, "Product's price cannot be empty");
            if (!ProductImages.Any()) throw new DomainException("A product must have at least one image");
        }
    }
}
