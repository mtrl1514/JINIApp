using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JINIApp.Shared.Models;

namespace JINIApp.Server.Data
{
    public class JINIAppServerContext : DbContext
    {
        public JINIAppServerContext (DbContextOptions<JINIAppServerContext> options)
            : base(options)
        {
        }

        public DbSet<JINIApp.Shared.Models.Item> Items { get; set; }

        public DbSet<JINIApp.Shared.Models.Customer> Customers { get; set; }

        public DbSet<JINIApp.Shared.Models.Revenue> Revenues { get; set; }

        public DbSet<JINIApp.Shared.Models.SalesOrderItem> SalesOrderItems { get; set; }

        public DbSet<JINIApp.Shared.Models.SalesOrder> SalesOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Revenue>().ToTable("Revenue");
            modelBuilder.Entity<SalesOrderItem>().ToTable("SalesOrderItem");
            modelBuilder.Entity<SalesOrder>().ToTable("SalesOrder");
        }
    }
}
