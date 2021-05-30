using FluentPOS.Application.Extensions;
using FluentPOS.Application.Features.Extensions;
using FluentPOS.Infrastructure.Extensions;
using FluentPOS.Infrastructure.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddApplicationServices(_configuration)
                .AddApplicationFeatures()
                .AddInfrastructureServices(_configuration)
                .AddSharedInfrastructureServices()
                .AddRouting(options => options.LowercaseUrls = true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseInfrastructureMiddlewares()
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}