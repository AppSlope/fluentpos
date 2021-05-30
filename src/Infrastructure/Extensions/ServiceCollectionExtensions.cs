using FluentPOS.Application.Abstractions.DbContexts;
using FluentPOS.Application.Abstractions.DI;
using FluentPOS.Application.Abstractions.EFContexts;
using FluentPOS.Application.Exceptions;
using FluentPOS.Application.Interfaces.Services.Auth;
using FluentPOS.Application.Interfaces.Services.Users;
using FluentPOS.Application.Settings;
using FluentPOS.Infrastructure.Constants;
using FluentPOS.Infrastructure.Persistence.Contexts.Dapper;
using FluentPOS.Infrastructure.Persistence.Contexts.EFCore;
using FluentPOS.Infrastructure.Identity;
using FluentPOS.Infrastructure.Services.Auth;
using FluentPOS.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace FluentPOS.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        
        public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddVersioning();
            services.AddContexts(configuration);
            services.ConfigureAppSettings(configuration);
            services.AddSwaggerDocumentation();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();
            return services;
        }
        private static IServiceCollection AddContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(configuration.GetConnectionString(PersistenceConstants.DefaultConnectionName), serverVersion));
            services.AddIdentity<ExtendedIdentityUser, ExtendedIdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>() ?? throw new DBContextNullException());
            services.AddScoped<IDbWriteContext, DapperDbWriteContext>();
            services.AddScoped<IDbReadContext, DapperDbReadContext>();
            return services;
        }
        private static IServiceCollection ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            return services;
        }

        private static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        private static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}\FluentPOS.API.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "FluentPOS.API",
                    License = new OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }
    }
}