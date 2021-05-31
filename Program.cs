using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioBack;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DesafioBackAcelera
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            await MyApp.Init();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
