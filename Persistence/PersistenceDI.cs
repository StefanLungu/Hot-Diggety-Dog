using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repository.v1;
using Persistence.Resources;

namespace Persistence
{
    public static class PersistenceDI
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString(Constants.DefaultConnectionString));
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>))
                    .AddScoped(typeof(IOrdersRepository), typeof(OrdersRepository))
                    .AddScoped(typeof(IUsersRepository), typeof(UsersRepository))
                    .AddScoped(typeof(IHotDogStandsRepository), typeof(HotDogStandsRepository))
                    .AddScoped(typeof(IInventoryProductsRepository), typeof(InventoryProductsRepository))
                    .AddScoped(typeof(IStandProductRepository), typeof(StandProductRepository))
                    .AddScoped(typeof(IProductsRequestsRepository), typeof(ProductsRequestsRepository));
        }
    }
}
