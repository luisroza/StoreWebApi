using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StoreApi.Catalog.Application.Services;
using StoreApi.Catalog.Data;
using StoreApi.Catalog.Data.Repository;
using StoreApi.Catalog.Domain;
using StoreApi.Core.Communication.Mediator;
using StoreApi.Core.Communication.Messages.Notifications;
using StoreApi.Payment.Application.AntiCorruption;
using StoreApi.Payment.Application.Services;
using StoreApi.Payment.Data;
using StoreApi.Payment.Data.Repository;
using StoreApi.Payment.Domain;
using StoreApi.Sales.Application.Services;
using StoreApi.Sales.Data;
using StoreApi.Sales.Data.Repository;
using StoreApi.Sales.Domain;

namespace StoreApi.WebApi.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator (wanna be BUS)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<CatalogContext>();

            // Sales
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<SalesContext>();

            // Payment
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICreditCardPaymentFacade, CreditCardPaymentFacade>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();
            services.AddScoped<PaymentContext>();
        }
    }
}
