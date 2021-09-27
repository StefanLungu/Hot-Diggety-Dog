using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Extensions
{
    public static class HostExtensions
    {
        public async static Task<IHost> SeedInventoryProducts(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            DataContext context = services.GetService<DataContext>();

            if (!context.InventoryProducts.Any())
            {
                await CreateInventoryProducts(context);
            }

            return host;
        }

        private async static Task CreateInventoryProducts(DataContext context)
        {
            foreach (Product product in context.Products)
            {
                await context.InventoryProducts.AddAsync(
                    new InventoryProduct { Id = Guid.NewGuid(), ProductId = product.Id, Product = product, Quantity = 0 }
                );
            }
            await context.SaveChangesAsync();
        }
    }
}
