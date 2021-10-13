using System;

namespace Store.Sales.Application.ViewModels
{
    public class OrderViewModel
    {
        public int Code { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public int OrderStatus { get; set; }
    }
}
