using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureToggle.Internal;
using featuretoggledemo.handlers;
using featuretoggleimpl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace featuretoggledemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange:true);
            Configuration = configuration;
            
            HostingEnvironment = env;
        }
        public IHostingEnvironment HostingEnvironment { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var provider = new AppSettingsProvider { Configuration = Configuration as IConfigurationRoot };
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton(new Printing { ToggleValueProvider = provider });
            services.AddSingleton(new Saving { ToggleValueProvider = provider });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.MapWhen
              (
                  context => context.Request.Path.Value.ToString().ToLower().Contains("featureone"),
                  appBranch =>
                  {
                      app.FeatureFeatureFlagHandler();
                  }
              );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseMvc();
        }
    }

    public static class FeatureToggleExtensions
    {
        public static IApplicationBuilder FeatureFeatureFlagHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<FeatureFlagRoutingHandler>();
            return app;
        }
    }

}
