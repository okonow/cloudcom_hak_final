﻿using Contracts.CreateUserContracts;
using MassTransit;
using UserAuthenticationService.Common.Interfaces.Identity;
using UserAuthenticationService.Data;

namespace UserAuthenticationService.Consumers.UserConsumers
{
    public class CreateUserConsumer(IUserService userService, IServiceProvider serviceProvider) : IConsumer<CreateUserSagaCommand>
    {
        private readonly IUserService _userService = userService;

        public async Task Consume(ConsumeContext<CreateUserSagaCommand> context)
        {
            try
            {
                var message = context.Message;
                var receivedData = message.User;

                var user = new ApplicationUser
                {
                    Id = receivedData.Id.ToString(),
                    FirstName = receivedData.FristName,
                    LastName = receivedData.LatName,
                    Email = receivedData.Email,
                    PasswordHash = receivedData.Password
                };

                var result = await _userService.CreateUserAsync(user);

                if (result.Succeeded)
                {

                    await context.Publish(new UserCreatedEvent { CorrelationId = receivedData.Id });
                    await context.RespondAsync<UserCreatedEvent>(new() { CorrelationId = receivedData.Id });
                }
                else
                    await context.RespondAsync<UserNotCreatedEvent>(new() { Errors = result.Errors });
            }
            catch (Exception ex)
            {
                // TODO: добавить нормальное логгирование
                Console.WriteLine(ex.Message);
                await context.RespondAsync<UserNotCreatedEvent>(new() { Errors = [ex.Message] });
            }
        }
    }
}