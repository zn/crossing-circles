using CrossingCirclesWeb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;

namespace CrossingCirclesWeb
{
    public class Startup
    {
        IConfiguration Configuration { get; set; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CrossingCirclesDatabaseSettings>(
                Configuration.GetSection("CrossingCirclesDatabase"));

            services.AddSingleton<HistoryService>();
            
            services.AddRazorPages()
                .AddJsonOptions(
                    options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}