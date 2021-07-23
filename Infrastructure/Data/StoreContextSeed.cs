using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext Context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!Context.productBrands.Any())
                {
                    var brandData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    foreach (var item in brand)
                    {
                        Context.productBrands.Add(item);
                    }
                    await Context.SaveChangesAsync();
                }

                if (!Context.productTypes.Any())
                {
                    var typeData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var type = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    foreach (var item in type)
                    {
                        Context.productTypes.Add(item);
                    }
                    await Context.SaveChangesAsync();
                }

                if (!Context.Products.Any())
                {
                    var productData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var product = JsonSerializer.Deserialize<List<Product>>(productData);
                    foreach (var item in product)
                    {
                        Context.Products.Add(item);
                    }
                    await Context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}