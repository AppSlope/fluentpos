using FluentPOS.Application.Abstractions.DateTimes;
using FluentPOS.Application.Abstractions.Serializations;
using FluentPOS.Infrastructure.Shared.Serializations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Infrastructure.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedInfrastructureServices(this IServiceCollection services)
        {
            services
            .AddSingleton<ISerializerService, SerializerService>();

            return services;
        }
    }
}
