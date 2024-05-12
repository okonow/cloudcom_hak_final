using Contracts.CreateUserContracts;
using MassTransit;
using ServicesSaga.Instances;

namespace ServicesSaga.StateMachines
{
    public class UserCreationSaga : MassTransitStateMachine<UserCreationState>
    {
        public UserCreationSaga()
        {
            InstanceState(x => x.CurrentState);

            Event(() => UserCreationRequested, x => x.CorrelateById(c => c.Message.User.Id));
            Event(() => UserCreated, x => x.CorrelateById(context => context.Message.CorrelationId));

            Initially(
                When(UserCreationRequested)
                    .Then(context =>
                    {
                        context.Instance.CorrelationId = context.Message.User.Id;
                        Console.WriteLine($"Пользователь с ID: {context.Message.User.Id } в процессе создания");
                    })
                    .TransitionTo(AwaitingUserCreation));

            During(AwaitingUserCreation,
                When(UserCreated)
                    .Then(context =>
                    {
                        Console.WriteLine($"Пользователь с ID: {context.Message.CorrelationId} создан");
                    })
                    .Publish(context => new CreateShoppingListSagaCommand
                    {
                        CorrelationId = context.Message.CorrelationId
                    })
                    .Publish(context => new CreateUserWalletSagaCommand
                    {
                        CorrelationId = context.Message.CorrelationId
                    })
                    .Finalize());

            SetCompletedWhenFinalized();
        }
        public State AwaitingUserCreation { get; private set; }

        public Event<CreateUserSagaCommand> UserCreationRequested { get; set; }
        public Event<UserCreatedEvent> UserCreated { get; set; }
    }
}
