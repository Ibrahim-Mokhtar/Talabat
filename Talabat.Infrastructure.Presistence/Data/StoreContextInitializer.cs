using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts.Presistance;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Infrastructure.Presistence.Data
{
    public class StoreContextInitializer(StoreContext DbContext) : IStoreContextInitializer
    {
        public async Task InitalizeAsync()
        {
            var pendingMigrations =await DbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await DbContext.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
            if (!DbContext.Brands.Any())
            {
                var brandsData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Presistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count() > 0)
                {
                    await DbContext.Set<ProductBrand>().AddRangeAsync(brands);
                    await DbContext.SaveChangesAsync();
                }
            }
            if (!DbContext.Categories.Any())
            {
                var categoriesData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Presistence/Data/Seeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories?.Count() > 0)
                {
                    await DbContext.Set<ProductCategory>().AddRangeAsync(categories);
                    await DbContext.SaveChangesAsync();
                }
            }
            if (!DbContext.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Presistence/Data/Seeds/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count() > 0)
                {
                    await DbContext.Set<Product>().AddRangeAsync(products);
                    await DbContext.SaveChangesAsync();
                }
            }
        }
    }
}
