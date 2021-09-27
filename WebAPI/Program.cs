using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using WebApi.Extensions;

namespace WebApi
{
    public static class Program
    {
        public async static Task Main(string[] args)
        {
            (await CreateHostBuilder(args).Build().SeedInventoryProducts()).Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
