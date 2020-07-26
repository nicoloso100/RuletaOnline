using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RuletaOnline.Configuration;
using RuletaOnline.Configuration.AppSettings;
using RuletaOnline.Infrastructure;
using RuletaOnline.Infrastructure.Repositories;
using RuletaOnline.Services;

namespace RuletaOnline
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ServerConfigurationInjection(services: services);
            DBContextInjection(services: services);
            RepositoriesLayerInjection(services: services);
            ServicesLayerInjection(services: services);
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void ServerConfigurationInjection(IServiceCollection services)
        {
            var dbConfig = new AppSettingsConfig();
            Configuration.Bind(dbConfig);
            var serverConfiguration = new ServerConfiguration(dbConfig);
            services.AddSingleton<IServerConfiguration>(serverConfiguration);
        }
        private void DBContextInjection(IServiceCollection services)
        {
            services.AddScoped<IRouletteContext, RouletteContext>();
        }
        private void RepositoriesLayerInjection(IServiceCollection services)
        {
            services.AddScoped<IRouletteRepository, RouletteRepository>();
        }

        private void ServicesLayerInjection(IServiceCollection services)
        {
            services.AddScoped<IRouletteService, RouletteService>();
        }
    }
}
