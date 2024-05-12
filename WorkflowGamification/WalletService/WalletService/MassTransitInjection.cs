using Contracts.ApplyJobContracts;
using Contracts.CreateUserContracts;
using Contracts.DeleteUserContracts;
using Dependencies;
using MassTransit;
using WalletService.Consumers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MassTransitInjection
    {
        public static IServiceCollection AddMassTransitServices(this IServiceCollection services)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<CreateWalletConsumer>();
                config.AddConsumer<SendMoneyToOtherUserConsumer>();
                config.AddConsumer<DeleteWalletConsumer>();

                config.UsingRabbitMq((context, rabbitConfig) =>
                {
                    rabbitConfig.Host(RabbitMqConnection.HOST, "/", h =>
                    {
                        h.Username(RabbitMqConnection.USERNAME);
                        h.Password(RabbitMqConnection.PASSWORD);
                    });
                    rabbitConfig.ConfigureEndpoints(context);
                });
                EndpointConvention.Map<CreateUserWalletSagaCommand>(new Uri($"queue:CreateWallet"));
                EndpointConvention.Map<DeleteWalletSagaCommand>(new Uri($"queue:DeleteWallet"));
                EndpointConvention.Map<SendToMoneyToUserSagaCommand>(new Uri($"queue:SendMoney"));
            });

            return services;
        }
    }
}
