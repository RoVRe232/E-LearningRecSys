using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RecSysApi.Application;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain;
using RecSysApi.Infrastructure;
using RecSysApi.Infrastructure.Context;
using RecSysApi.Infrastructure.Repositories;
using RecSysApi.Presentation.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecSysApi
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
            services.AddControllers();

            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

            var connection = 
                @"Server=(localdb)\mssqllocaldb;Database=RecSysApiDb;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<RecSysApiContext>(options => options.UseSqlServer(connection));

            services.AddInfrsatructureLayerDependencies();
            services.AddDomainLayerDependencies();
            services.AddApplicationLayerDependencies();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RecSysApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecSysApi v1"));
            }

            app.UseHttpsRedirection();
            //TODO remove this once deployed to sandbox/production
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
