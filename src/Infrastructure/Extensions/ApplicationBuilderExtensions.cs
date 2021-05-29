using FluentPOS.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace FluentPOS.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseInfrastructureMiddlewares(this IApplicationBuilder app)
        {
            app.UseGlobalErrorHandler();
            app.UseSwaggerDocumentation();
            return app;
        }
        private static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalErrorHandler>();
            return app;
        }

        private static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DefaultModelsExpandDepth(-1);
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "FluentPOS");
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
            return app;
        }
    }
}