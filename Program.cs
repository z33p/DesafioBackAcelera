using System.Threading.Tasks;
using DesafioBack.Data.Shared;
using DesafioBack.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DesafioBackAcelera
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await host.Services.GetService<IMyDatabase>().InitDatabase(OnDbInitialized: async () =>
            {
                await host.Services.GetService<IYoutubeApiService>().SeedDatabase();
            });

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
