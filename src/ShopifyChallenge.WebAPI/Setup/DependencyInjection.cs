using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShopifyChallenge.Catalog.Application.Services;
using ShopifyChallenge.Catalog.Data;
using ShopifyChallenge.Catalog.Data.Repository;
using ShopifyChallenge.Catalog.Domain;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Communication.Messages.IntegrationEvents;
using ShopifyChallenge.Core.Communication.Messages.Notifications;
using ShopifyChallenge.Payment.Application.AntiCorruption;
using ShopifyChallenge.Payment.Application.Events;
using ShopifyChallenge.Payment.Application.Services;
using ShopifyChallenge.Payment.Data;
using ShopifyChallenge.Payment.Data.Repository;
using ShopifyChallenge.Payment.Domain;
using ShopifyChallenge.Sales.Application.Commands;
using ShopifyChallenge.Sales.Application.Events;
using ShopifyChallenge.Sales.Application.Services;
using ShopifyChallenge.Sales.Data;
using ShopifyChallenge.Sales.Data.Repository;
using ShopifyChallenge.Sales.Domain;

namespace ShopifyChallenge.WebAPI.Setup
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

            services.AddScoped<IRequestHandler<AddOrderLineCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateOrderLineCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveOrderLineCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyCouponCommand, bool>, OrderCommandHandler>();

            services.AddScoped<INotificationHandler<DraftOrderStartedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<UpdateOrderEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<AddOrderLineEvent>, OrderEventHandler>();

            // Payment
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICreditCardPaymentFacade, CreditCardPaymentFacade>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();
            services.AddScoped<PaymentContext>();
            services.AddScoped<INotificationHandler<OrderInventoryConfirmedEvent>, PaymentEventHandler>();
        }
    }
}
