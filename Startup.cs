using DesafioBack.Config;
using DesafioBack.Contracts;
using DesafioBack.Data;
using DesafioBack.Data.Repositories;
using DesafioBack.Data.Repositories.shared;
using DesafioBack.Data.Shared;
using DesafioBack.Services;
using DesafioBack.Services.Videos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DesafioBackAcelera
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSingleton<IMyDatabase, MyDatabase>();
            services.AddSingleton<IRepository, Repository>();
        
            services.AddSingleton<ISqlSnippets, SqlSnippets>();

            services.AddSingleton<IVideosService, VideosService>();

            services.AddSingleton<IYoutubeApiRoutes, YoutubeApiRoutes>();
            services.AddSingleton<IYoutubeApiService, YoutubeApiService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioBackAcelera", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioBackAcelera v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
