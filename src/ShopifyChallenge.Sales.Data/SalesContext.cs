using Microsoft.EntityFrameworkCore;
using ShopifyChallenge.Core.Communication;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Data;
using ShopifyChallenge.Sales.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;
using ShopifyChallenge.Core.Communication.Messages;

namespace ShopifyChallenge.Sales.Data
{
    public class SalesContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        //DbContextOptions will have some configuration on StartUp
        public SalesContext(DbContextOptions<SalesContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //All varchar columns will have 100 as max length instead of MAX, on db creation time
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetMaxLength(100);

            modelBuilder.Ignore<Event>();

            //Search for all entities and its mappings configured on the project
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesContext).Assembly);

            modelBuilder.HasSequence<int>("OrderSequence").StartsAt(1000).IncrementsBy(1);
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            //ChangeTracker - EntityFramework's tracking mapper - it has all changes
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreateDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreateDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreateDate").IsModified = false;
                }
            }

            var success = await base.SaveChangesAsync() > 0;
            if (success) await _mediatorHandler.PublishEvents(this);

            return success;
        }
    }
}
