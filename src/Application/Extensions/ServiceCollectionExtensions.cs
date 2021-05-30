using FluentPOS.Application.PipelineBehaviors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FluentPOS.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _config)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddDistributedMemoryCache();
            if (_config.GetSection("PipelineSettings:Caching").Value == "True")
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            }
            if (_config.GetSection("PipelineSettings:Logging").Value == "True")
            {
               //Add Request Logging Behavior here
            }
            return services;
        }
    }
}