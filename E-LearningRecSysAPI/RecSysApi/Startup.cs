using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecSysApi.Application;
using RecSysApi.Application.Dtos.Account;
using RecSysApi.Domain;
using RecSysApi.Infrastructure;
using RecSysApi.Infrastructure.Context;
using RecSysApi.Presentation.Settings;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace RecSysApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            var token = Configuration.GetSection("TokenConfiguration");
            services.Configure<TokenConfigurationDTO>(token);

            var tokenSettings = token.Get<TokenConfigurationDTO>();
            var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = Environment.IsProduction();
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = tokenSettings.Issuer,
                        ValidAudience = tokenSettings.Audience
                    };
                });
            services.AddAuthorization(options =>
                {
                    options.AddPolicy("RefreshOnly", policy => policy.RequireClaim("RefreshToken"));
                    options.AddPolicy("AuthOnly", policy => policy.RequireClaim("AuthToken"));
                    options.AddPolicy("CustomerOnly", policy => policy.RequireClaim(ClaimTypes.Role, "customer"));
                    options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "admin"));
                });
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

            services.Configure<FormOptions>(options =>
            {
                
                options.MultipartBodyLengthLimit = 10000000000000;
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.AddInfrastructureLayerDependencies();
            services.AddDomainLayerDependencies();
            services.AddApplicationLayerDependencies();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RecSysApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecSysApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                builder.WithOrigins("http://localhost:5006")
                .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseResponseCompression();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
