using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SSNZ.Steam2019.Service.Services;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Swagger;

namespace SSNZ.Steam2019.Service
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    public class Startup
    {
        //From: https://github.com/spetz/asp-net-core-samples/tree/master/src/8.Redis/App
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SSNZ Steam API",
                    Description = "My SSNZ Steam ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Sam Smith", Email = "samsmithnz@gmail.com", Url = "www.samsmithnz.com" }
                });
            });

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config["CacheConnection"];

            services.AddSingleton<IRedisService, RedisService>();
            var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            var database = connectionMultiplexer.GetDatabase(0);
            services.AddSingleton<IDatabase>(_ => database);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("MyCorsPolicy"); //https://stackoverflow.com/questions/31942037/how-to-enable-cors-in-asp-net-core
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
