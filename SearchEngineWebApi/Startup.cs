using ElasticSearchEngineService;
using ElasticSearchEngineService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchEngineDomain.Interfaces;

namespace SolrWebService
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
            ElasticSetup(services);
            //SetupSolr(services);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private void ElasticSetup(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("ElasticSearchEngine");
            services.AddSingleton<ISearchEngineService<ElasticFileInfo>>(s=>new ElasticServiceImpl<ElasticFileInfo>(connectionString));
        }
        
        private void SetupSolr(IServiceCollection services)
        {
            //var connectionString = Configuration.GetConnectionString("SolrSearchEngine");
            //services.AddSingleton<ISearchEngineService>(s=>new SolrServiceImpl(connectionString));
        }
    }
}
