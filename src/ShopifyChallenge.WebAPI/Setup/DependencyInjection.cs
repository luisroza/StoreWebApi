using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Catalog.Application.Services;
using Store.Catalog.Data;
using Store.Catalog.Data.Repository;
using Store.Catalog.Domain;
using Store.Core.Communication.Mediator;
using Store.Core.Communication.Messages.IntegrationEvents;
using Store.Core.Communication.Messages.Notifications;
using Store.Payment.Application.AntiCorruption;
using Store.Payment.Application.Events;
using Store.Payment.Application.Services;
using Store.Payment.Data;
using Store.Payment.Data.Repository;
using Store.Payment.Domain;
using Store.Sales.Application.Commands;
using Store.Sales.Application.Events;
using Store.Sales.Application.Services;
using Store.Sales.Data;
using Store.Sales.Data.Repository;
using Store.Sales.Domain;

namespace Store.WebAPI.Setup
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
