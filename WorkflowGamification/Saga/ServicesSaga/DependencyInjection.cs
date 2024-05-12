using Contracts.CreateUserContracts;
using Contracts.DeleteUserContracts;
using Dependencies;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            //services.AddScoped<ISagaCommand, CreateUserSagaCommand>();
            //services.AddScoped<ISagaCommand, DeleteUserSagaCommand>();

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
                //config.AddRequestClient<CreateUserSagaCommand>();

                EndpointConvention.Map<CreateUserSagaCommand>(new Uri($"queue:CreateUser"));
                EndpointConvention.Map<CreateShoppingListSagaCommand>(new Uri($"queue:AddShoppingList"));
                EndpointConvention.Map<CreateUserWalletSagaCommand>(new Uri($"queue:CreateWallet"));

                EndpointConvention.Map<DeleteUserSagaCommand>(new Uri($"queue:DeleteUser"));
                EndpointConvention.Map<DeleteShoppingListSagaCommand>(new Uri($"queue:DeleteShoppingList"));
                EndpointConvention.Map<DeleteWalletSagaCommand>(new Uri($"queue:DeleteWallet"));

                GlobalTopology.Send.UseCorrelationId<CreateUserSagaCommand>(i => i.User.Id);

                config.AddSagaStateMachine<UserCreationSaga, UserCreationState>()
                    .InMemoryRepository();

                config.AddSagaStateMachine<UserDeletionSaga, UserDeletionState>()
                    .InMemoryRepository();
            });

            var secret = configuration["JWT:Secret"]
                ?? throw new InvalidOperationException("Secret not configured");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            return services;
        }
    }
}
