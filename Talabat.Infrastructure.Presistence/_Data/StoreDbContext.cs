using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Talabat.Core.Domain.Entites.Orders;
using Talabat.Core.Domain.Entites.Products;
using Talabat.Infrastructure.Presistence._Common;
using Talabat.Infrastructure.Presistence.Identity;

namespace Talabat.Infrastructure.Presistence.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
                 type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(StoreDbContext));
        }
        public DbSet<Product> Products {get;set;}
        public DbSet<ProductBrand> Brands {get;set;}
        public DbSet<ProductCategory> Categories {get;set;}
        public DbSet<Order> Orders {get;set;}
        public DbSet<OrderItem> OrderItems {get;set;}
        public DbSet<DeliveryMethod> DeliveryMethods {get;set;}
    }
}
