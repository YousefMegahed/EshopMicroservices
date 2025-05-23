using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.API
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddCarter();

            //services.AddExceptionHandler<CustomExceptionHandler>();
            //services.AddHealthChecks()
            //    .AddSqlServer(configuration.GetConnectionString("Database")!);

            services.AddMediatR(cf =>
            cf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
