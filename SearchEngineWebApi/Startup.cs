using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchEngineDomain;
using SearchEngineDomain.Data;
using SolrNet;
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
            SetupSolr(services);

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

        private void SetupSolr(IServiceCollection services)
        {

            var solrConnectionString = "http://192.168.99.100:8983/solr/solrdocument";
            SolrNet.Startup.Init<SolrFileInfo>(solrConnectionString);

            services.AddSingleton<ISearchEngineService, SolrServiceImpl>();
        }
    }
}
