using Microsoft.EntityFrameworkCore;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Infrastructure.Presistence.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);
        }
        public DbSet<Product> Products {get;set;}
        public DbSet<ProductBrand> Brands {get;set;}
        public DbSet<ProductCategory> Categories {get;set;}
    }
}
