using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Corteva.IO.ImageryServices.Formatters;
using Corteva.IO.ImageryServices.Handlers;
using Corteva.IO.ImageryServices.Models;
using Corteva.IO.ImageryServices.Services;
using Corteva.IO.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using PolicyServer.Runtime.Client;
using Microsoft.Extensions.Caching.Distributed;

namespace Corteva.IO.ImageryServices
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
            services.AddMvc
                (
                    options =>
                         options.OutputFormatters.Add(new WebApi.Hal.JsonHalMediaTypeOutputFormatter(new JsonSerializerSettings { Formatting = Formatting.Indented }, ArrayPool<char>.Shared))
                )
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Corteva IO Imagery", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<ProducesFilter>();
                c.OperationFilter<AddDefaultValuesFilter>();
                c.EnableAnnotations();
            });

            HttpClient orionServiceHttpClient = new HttpClient()
            {
                BaseAddress = new Uri(@"https://app.encirca.pioneer.com/")
            };

            var encircaOAuthCredentials = GetEncircaOAuthCredentials();
            string encircaOAuthToken = RetrieveOAuthToken(encircaOAuthCredentials);
            orionServiceHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", encircaOAuthToken);
            ICropHealthImageryService cropHealthImageryService = new CropHealthImageryService(orionServiceHttpClient);
            services.AddSingleton(cropHealthImageryService);

            services.AddPolicyServerClient(Configuration.GetSection("Policy"));

            services.AddDistributedRedisCache
                (
                    options =>
                    {
                        options.Configuration = "localhost:32773";
                        options.InstanceName = "sample";
                    }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            //, IApplicationLifetime lifetime, IDistributedCache cache)
        {
            app.MapWhen(
            context => context.Request.Path.Value.ToString().ToLower().Contains("crophealth"),
            appBranch =>
            {
                app.RegisterCropHealthImageryHandler();
                app.UseMvc();
            }
            );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //lifetime.ApplicationStarted.Register(() =>
            //{
            //    var currentTimeUTC = DateTime.UtcNow.ToString();
            //    byte[] encodedCurrentTimeUTC = Encoding.UTF8.GetBytes(currentTimeUTC);
            //    var options = new DistributedCacheEntryOptions()
            //        .SetSlidingExpiration(TimeSpan.FromSeconds(20));
            //    cache.Set("cachedTimeUTC", encodedCurrentTimeUTC, options);
            //});

            //app.MapWhen(
            // context => context.Request.Path.Value.ToString().ToLower().Contains("crophealth"),
            // appBranch =>
            // {
            //     app.RegisterCropHealthImageryHandler();
            // }
            // );

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;

            });

            //app.MapWhen(
            //context => context.Request.Path.Value.ToString().ToLower().Contains("crophealth"),
            //appBranch =>
            //{
            //    app.RegisterCropHealthImageryHandler();
            //}
            //);


            app.UseMvc();
           


        }

        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, IDistributedCache cache)
        //{
        //    lifetime.ApplicationStarted.Register(() =>
        //    {
        //        var currentTimeUTC = DateTime.UtcNow.ToString();
        //        byte[] encodedCurrentTimeUTC = Encoding.UTF8.GetBytes(currentTimeUTC);
        //        var options = new DistributedCacheEntryOptions()
        //            .SetSlidingExpiration(TimeSpan.FromSeconds(20));
        //        cache.Set("cachedTimeUTC", encodedCurrentTimeUTC, options);
        //    });
        //}

        private OAuthCredentials GetEncircaOAuthCredentials()
        {

            return new OAuthCredentials
            {
                OAuthId = "iodigital-z@testing.encirca.pioneer.com",
                Password = "Twm7uF61AbQ1N^uYzt%+K",
                URL = "https://app.encirca.pioneer.com/token/v1/jwt"
            };
        }

        private string RetrieveOAuthToken(OAuthCredentials credentials)
        {
            string oAuthToken = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(new
                {
                    username = credentials.OAuthId,
                    password = credentials.Password
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(credentials.URL, content).Result;

                var bearerData = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    oAuthToken = JObject.Parse(bearerData)["id_token"].ToString();

                }
            }
            return oAuthToken;
        }
             
    }

    public static class CropHealthImageryHandlerExtensions
    {
        public static IApplicationBuilder RegisterCropHealthImageryHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CropHealthImageryHandler>();
            return app;
        }
    }


}
