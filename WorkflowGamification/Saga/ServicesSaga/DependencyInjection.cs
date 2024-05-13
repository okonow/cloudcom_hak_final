using Contracts.AddRoleToUserContracts;
using Contracts.ApplyJobContracts;
using Contracts.CreateUserContracts;
using Contracts.DeleteUserContracts;
using Dependencies;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using ServicesSaga.Instances;
using ServicesSaga.StateMachines;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSagas(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {               
                config.UsingRabbitMq((context, rabbitConfig) =>
                {                    
                    rabbitConfig.Host(RabbitMqConnection.HOST, "/", h =>
                    {
                        h.Username(RabbitMqConnection.USERNAME);
                        h.Password(RabbitMqConnection.PASSWORD);
                    });
                    rabbitConfig.ConfigureEndpoints(context);
                });
                config.AddDelayedMessageScheduler();
                
                EndpointConvention.Map<CreateUserSagaCommand>(new Uri($"queue:CreateUser"));
                EndpointConvention.Map<CreateShoppingListSagaCommand>(new Uri($"queue:AddShoppingList"));
                EndpointConvention.Map<CreateUserWalletSagaCommand>(new Uri($"queue:CreateWallet"));

                EndpointConvention.Map<AddRoleToUserSagaCommand>(new Uri($"queue:AddRoleToUser"));
                EndpointConvention.Map<CreateEmployeeStatisticsSagaCommand>(new Uri($"queue:CreateEmployeeStatstics"));

                EndpointConvention.Map<DeleteUserSagaCommand>(new Uri($"queue:DeleteUser"));
                EndpointConvention.Map<DeleteShoppingListSagaCommand>(new Uri($"queue:DeleteShoppingList"));
                EndpointConvention.Map<DeleteWalletSagaCommand>(new Uri($"queue:DeleteWallet"));

                EndpointConvention.Map<FinishJobSagaCommand>(new Uri($"queue:FinishJob"));
                EndpointConvention.Map < SendToMoneyToUserSagaCommand>(new Uri($"queue:SendMoneyToUser"));

                GlobalTopology.Send.UseCorrelationId<CreateUserSagaCommand>(i => i.User.Id);

                config.AddSagaStateMachine<UserCreationSaga, UserCreationState>()
                    .InMemoryRepository();

                config.AddSagaStateMachine<UserDeletionSaga, UserDeletionState>()
                    .InMemoryRepository();
            });
                       
            return services;
        }
    }
}
