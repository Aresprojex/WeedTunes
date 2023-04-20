using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using WeedTunes.Services;
using Microsoft.OpenApi.Models;
using WeedTunes.Utilities;

namespace WeedTunes
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
            ConfigureDIService(services);
            ConfigureSwagger(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCustomSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureDIService(IServiceCollection services)
        {
            services.AddScoped<IRecommendationService, RecommendationService>();
        }

        public void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                       new OpenApiInfo
                       {
                           Title = "WeedTunes API",
                           Version = "v1",
                           Description = "Weed tunes Platform",
                           Contact = new OpenApiContact
                           {
                               Name = "Weed Tunes",
                               Email = "Info@weedtunes.com"
                           },
                           License = new OpenApiLicense
                           {
                               Name = "MIT License",
                               Url = new Uri("https://en.wikipedia.org/wiki/MIT_Lincense")
                           }
                       });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using Bearer scheme. \r\n\r\n" +
                    "Enter 'Bearer' [space] and then your token in the input below.\r\n\r\n" +
                    "Example: \"Bearer 123456\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }

                });

                c.DescribeAllParametersInCamelCase();
            });
        }
    }
}