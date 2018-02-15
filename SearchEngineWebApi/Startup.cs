using ElasticSearchEngineService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchEngineDomain;
using SolrSearchEngineService;

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
            services.AddSingleton<ISearchEngineService, ElasticServiceImpl>(s=>new ElasticServiceImpl(connectionString));
        }
        
        private void SetupSolr(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("SolrSearchEngine");
            services.AddSingleton<ISearchEngineService, SolrServiceImpl>(s=>new SolrServiceImpl(connectionString));
        }
    }
}
