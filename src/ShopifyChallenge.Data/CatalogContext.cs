﻿using Microsoft.EntityFrameworkCore;
using ShopifyChallenge.Catalog.Domain;
using ShopifyChallenge.Core.Communication;
using ShopifyChallenge.Core.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using ShopifyChallenge.Core.Communication.Messages;

namespace ShopifyChallenge.Catalog.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        //DbContextOptions will have some configuration on StartUp
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //All varchar columns will have 100 as max length instead of MAX, on db creation time
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetMaxLength(100);

            modelBuilder.Ignore<Event>();

            //Search for all entities and its mappings configured on the project
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            //ChangeTracker - EntityFramework's tracking mapper
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

            //More than 1 row updated on DB
            return await base.SaveChangesAsync() > 0;
        }
    }
}
