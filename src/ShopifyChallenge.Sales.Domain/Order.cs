using FluentValidation.Results;
using ShopifyChallenge.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopifyChallenge.Sales.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid? CouponId { get; private set; }
        public bool CouponUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreateDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        //Class use only
        private readonly List<OrderLine> _orderLines;

        //public reading of _orderLines
        public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

        //EF relation
        public virtual Coupon Coupon { get; private set; }

        public Order(Guid customerId, bool couponUsed, decimal discount, decimal totalPrice)
        {
            CustomerId = customerId;
            CouponUsed = couponUsed;
            Discount = discount;
            TotalPrice = totalPrice;
            _orderLines = new List<OrderLine>();

            Validate();
        }

        //EF Relation
        protected Order()
        {
            _orderLines = new List<OrderLine>();
        }

        public void CalculateTotalPriceDiscount()
        {
            if (!CouponUsed) return;

            decimal discount = 0;
            var price = TotalPrice;

            if (Coupon.TypeDiscount == CouponType.Percentage)
            {
                if (Coupon.Percentage.HasValue)
                {
                    Discount = (price * Coupon.Percentage.Value) / 100;
                    price -= Discount;
                }
            }
            else
            {
                if (Coupon.PriceDiscount.HasValue)
                {
                    Discount = Coupon.PriceDiscount.Value;
                    price -= Discount;
                }
            }

            TotalPrice = price < 0 ? 0 : price;
            Discount = discount;
        }

        public void CalculateOrderPrice()
        {
            TotalPrice = OrderLines.Sum(p => p.CalculatePrice());
            CalculateTotalPriceDiscount();
        }

        public ValidationResult ApplyCoupon(Coupon coupon)
        {
            var validationResult = coupon.ValidateVoucher();
            if (!validationResult.IsValid) return validationResult;

            Coupon = coupon;
            CouponUsed = true;
            CalculateOrderPrice();

            return validationResult;
        }

        public bool ExistingOrderLine(OrderLine item)
        {
            return _orderLines.Any(p => p.ProductId == item.ProductId);
        }

        public void AddOrderLine(OrderLine item)
        {
            if (item.IsValid()) return;

            item.AssociateOrder(Id);

            if (ExistingOrderLine(item))
            {
                var existentOrderLine = _orderLines.FirstOrDefault(p => p.ProductId == item.ProductId);
                existentOrderLine.AddUnit(item.Quantity);
                item = existentOrderLine;

                _orderLines.Remove(existentOrderLine);
            }

            item.CalculatePrice();
            _orderLines.Add(item);

            CalculateOrderPrice();
        }

        public void RemoveOrderLine(OrderLine item)
        {
            if (!item.IsValid()) return;

            var existentOrderLine = OrderLines.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existentOrderLine == null)
                throw new DomainException("This order line does not belong to the order");
            _orderLines.Remove(existentOrderLine);

            CalculateOrderPrice();
        }

        public void UpdateOrderLine(OrderLine item)
        {
            if (!item.IsValid()) return;
            item.AssociateOrder(Id);

            var existentOrderLine = OrderLines.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existentOrderLine == null)
                throw new DomainException("This order line does not belong to the order");

            _orderLines.Remove(existentOrderLine);
            _orderLines.Add(item);

            CalculateOrderPrice();
        }

        public void UpdateUnits(OrderLine item, int units)
        {
            item.UpdateUnit(units);
            UpdateOrderLine(item);
        }

        public void MakeDraft() => OrderStatus = OrderStatus.Draft;

        public void StarOrder() => OrderStatus = OrderStatus.Started;

        public void FinalizeOrder() => OrderStatus = OrderStatus.Paid;

        public void CancelOrder() => OrderStatus = OrderStatus.Cancelled;

        public static class OrderFactory
        {
            public static Order NewDraftOrder(Guid customerId)
            {
                var order = new Order { CustomerId = customerId };
                order.MakeDraft();
                return order;
            }
        }

        public void Validate()
        {
            AssertionConcern.AssertArgumentNotNull(CustomerId, "CustomerId cannot be empty");
            AssertionConcern.AssertArgumentNotNull(_orderLines, "Order Line list must be initialized");
            AssertionConcern.AssertArgumentLesserThan(TotalPrice, 1, "Order's price needs to more than zero");
        }
    }
}
