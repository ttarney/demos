using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Corteva.IO.CropHealthImagery.Models;
using Corteva.IO.CropHealthImagery.Services;
using Corteva.IO.SwaggerSupport;
using Corteva.IO.SwaggerSupport.Operation_Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;

namespace Corteva.IO.CropHealthImagery
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
            services.AddMvc()
                //(
                //    options =>
                //        options.OutputFormatters.Insert(0, new CropHealthModelFormatter());
                //)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Corteva IO Crop Health Imagery", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<ProducesFilter>();
                c.OperationFilter<AddDefaultValuesFilter>();
                c.EnableAnnotations();
            });

            // add http clients

            HttpClient orionServiceHttpClient = new HttpClient()
            {
                BaseAddress = new Uri(@"https://app.encirca.pioneer.com/")
            };

            var encircaOAuthCredentials = GetEncircaOAuthCredentials();
            string encircaOAuthToken = RetrieveOAuthToken(encircaOAuthCredentials);
            orionServiceHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", encircaOAuthToken);
            ICropHealthImageryService cropHealthImageryService = new CropHealthImageryService(orionServiceHttpClient);
            services.AddSingleton(cropHealthImageryService);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
               
            });
            app.UseMvc();
        }
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
}
