using CompanyWorkspaceService.Consumers;
using CompanyWorkspaceService.Consumers.ShoppingListConsumers;
using Contracts.AddRoleToUserContracts;
using Contracts.ApplyJobContracts;
using Dependencies;
using MassTransit;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MassTransitInjection
    {
        public static IServiceCollection AddMassTransitServices(this IServiceCollection services)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<CreateEmployeeStatisticsConsumer>();
                config.AddConsumer<FinishJobConsumer>();

                config.UsingRabbitMq((context, rabbitConfig) =>
                {
                    rabbitConfig.Host(RabbitMqConnection.HOST, "/", h =>
                    {
                        h.Username(RabbitMqConnection.USERNAME);
                        h.Password(RabbitMqConnection.PASSWORD);
                    });
                    rabbitConfig.ConfigureEndpoints(context);
                });
                EndpointConvention.Map<CreateEmployeeStatisticsSagaCommand>(new Uri($"queue:CreateEmployeeStatistics"));
                EndpointConvention.Map<FinishJobSagaCommand>(new Uri($"queue:FinishJob"));
            });

            return services;
        }
    }
}
