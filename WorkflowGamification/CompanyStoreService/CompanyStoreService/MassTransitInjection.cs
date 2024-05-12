using CompanyStoreService.Consumers.ShoppingListConsumers;
using Contracts.CreateUserContracts;
using Contracts.DeleteUserContracts;
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
                config.AddConsumer<CreateShoppingListConsumer>();
                config.AddConsumer<DeleteShoppingListConsumer>();

                config.UsingRabbitMq((context, rabbitConfig) =>
                {
                    rabbitConfig.Host(RabbitMqConnection.HOST, "/", h =>
                    {
                        h.Username(RabbitMqConnection.USERNAME);
                        h.Password(RabbitMqConnection.PASSWORD);
                    });
                    rabbitConfig.ConfigureEndpoints(context);
                });
                EndpointConvention.Map<CreateShoppingListSagaCommand>(new Uri($"queue:CreateShoppingList"));
                EndpointConvention.Map<DeleteShoppingListSagaCommand>(new Uri($"queue:CreateShoppingList"));
            });

            return services;
        }
    }
}
