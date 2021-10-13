using Microsoft.EntityFrameworkCore;
using Store.Core.Communication.Mediator;
using Store.Core.Communication.Messages;
using Store.Core.Data;
using Store.Payment.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Payment.Data
{
    public class PaymentContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PaymentContext(DbContextOptions<PaymentContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler ?? throw new ArgumentException(nameof(mediatorHandler));
        }

        public DbSet<Domain.Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public async Task<bool> Commit()
        {
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

            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetMaxLength(100);
            }

            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(
                r => r.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
