using FluentValidation;
using StoreApi.Core.DomainObjects;
using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace StoreApi.Sales.Domain
{
    public class Coupon : Entity
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? PriceDiscount { get; private set; }
        public int Quantity { get; private set; }
        public CouponType TypeDiscount { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public DateTime? UsedDate { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        //EF Relation
        public ICollection<Order> Orders { get; set; }

        internal ValidationResult ValidateVoucher()
        {
            return new VoucherValidation().Validate(this);
        }
    }

    public class VoucherValidation : AbstractValidator<Coupon>
    {
        public VoucherValidation()
        {
            RuleFor(v => v.ExpirationDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Voucher expired");

            RuleFor(v => v.Active)
                .Equal(true)
                .WithMessage("Voucher not valid anymore");

            RuleFor(v => v.Used)
                .Equal(false)
                .WithMessage("Voucher already used");

            RuleFor(v => v.Quantity)
                .GreaterThan(0)
                .WithMessage("Voucher no longer available");
        }
    }
}
