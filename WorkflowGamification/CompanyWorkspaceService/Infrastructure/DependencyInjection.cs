using Application.Common.Interfaces;
using Domain.Enums;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationString = configuration.GetConnectionString("DbConnection");

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(configurationString);
            dataSourceBuilder.MapEnum<Complexity>();
            var dataSource = dataSourceBuilder.Build();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(dataSource);
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddTransient<IEmployeesStatisticsSorter, EmployeeStatisticsSorter>();
            return services;
        }
    }
}
