using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using Talabat.Core.Domain.Entites.Orders;
using Talabat.Core.Domain.Entites.Products;
using Talabat.Infrastructure.Presistence._Common;

namespace Talabat.Infrastructure.Presistence.Data
{
    internal class StoreDbInitializer(StoreDbContext DbContext) :DbInitializer(DbContext), IStoreDbInitializer
    {
      

        public override async Task SeedAsync()
        {
            if (!DbContext.Brands.Any())
            {
                var brandsData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Presistence/_Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count() > 0)
                {
                    await DbContext.Set<ProductBrand>().AddRangeAsync(brands);
                    await DbContext.SaveChangesAsync();
                }
            }
            if (!DbContext.Categories.Any())
            {
                var categoriesData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Presistence/_Data/Seeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories?.Count() > 0)
                {
                    await DbContext.Set<ProductCategory>().AddRangeAsync(categories);
                    await DbContext.SaveChangesAsync();
                }
            }
            if (!DbContext.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Presistence/_Data/Seeds/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count() > 0)
                {
                    await DbContext.Set<Product>().AddRangeAsync(products);
                    await DbContext.SaveChangesAsync();
                }
            }
            if (!DbContext.DeliveryMethods.Any())
            {
                var deliveryMethodsData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Presistence/_Data/Seeds/delivery.json");
                var deliveryMehods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                if (deliveryMehods?.Count() > 0)
                {
                    await DbContext.Set<DeliveryMethod>().AddRangeAsync(deliveryMehods);
                    await DbContext.SaveChangesAsync();
                }
            }
        }
    }
}
