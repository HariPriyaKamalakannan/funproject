using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        { 
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if(!context.ProductBrands.Any())
        {
            var brandsData=File.ReadAllText(path+@"/Data/SeedData/brands.json");
            var brands=JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            context.ProductBrands.AddRange(brands);
        }
        if(!context.ProductTypes.Any())
        {
            var typeData=File.ReadAllText(path+@"/Data/SeedData/types.json");
            var types=JsonSerializer.Deserialize<List<ProductType>>(typeData);
            context.ProductTypes.AddRange(types);
        }
        if(!context.Products.Any())
        {
            var ProductsData=File.ReadAllText(path+@"/Data/SeedData/products.json");
            var Product=JsonSerializer.Deserialize<List<Product>>(ProductsData);
            context.Products.AddRange(Product);
        }
        if(!context.DeliveryMethods.Any())
        {
            var deliveryData=File.ReadAllText(path+@"/Data/SeedData/delivery.json");
            var methods=JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
            context.DeliveryMethods.AddRange(methods);
        }
        if(context.ChangeTracker.HasChanges())await context.SaveChangesAsync();

        }
    }
}