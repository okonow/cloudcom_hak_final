using Contracts.CreateUserContracts;
using Contracts.DeleteUserContracts;
using Dependencies;
using MassTransit;
using UserAuthenticationService.Consumers.RoleConsumers;
using UserAuthenticationService.Consumers.UserConsumers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MassTransitInjection
    {
        public static IServiceCollection AddMassTransitServices(this IServiceCollection services)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<CreateUserConsumer>();
                config.AddConsumer<DeleteUserConsumer>();
                config.AddConsumer<AddRoleToUserConsumer>();

                config.UsingRabbitMq((context, rabbitConfig) =>
                {
                    rabbitConfig.Host(RabbitMqConnection.HOST, "/", h =>
                    {
                        h.Username(RabbitMqConnection.USERNAME);
                        h.Password(RabbitMqConnection.PASSWORD);
                    });
                    rabbitConfig.ConfigureEndpoints(context);
                });
                EndpointConvention.Map<CreateUserSagaCommand>(new Uri($"queue:CreateUser"));
                EndpointConvention.Map<CreateUserWalletSagaCommand>(new Uri($"queue:CreateWallet"));

                EndpointConvention.Map<DeleteUserSagaCommand>(new Uri($"queue:DeleteUser"));
            });

            return services;
        }
    }
}
