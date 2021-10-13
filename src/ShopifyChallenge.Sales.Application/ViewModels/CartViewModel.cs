﻿using System;
using System.Collections.Generic;

namespace Store.Sales.Application.ViewModels
{
    public class CartViewModel
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public string CouponCode { get; set; }

        public List<CartOrderLineViewModel> Lines { get; set; } = new List<CartOrderLineViewModel>();
        public CartPaymentViewModel Payment { get; set; }
    }
}
