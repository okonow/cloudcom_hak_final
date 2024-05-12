using Contracts.CreateUserContracts;
using Contracts.DeleteUserContracts;
using MassTransit;
using ServicesSaga.Instances;

namespace ServicesSaga.StateMachines
{
    public class UserDeletionSaga : MassTransitStateMachine<UserDeletionState>
    {
        public UserDeletionSaga()
        {
            InstanceState(s => s.CurrentState);

            Event(() => UserDeletionRequested, x => x.CorrelateById(c => c.Message.UserId));
            Event(() => UserDeleted, x => x.CorrelateById(context => context.Message.CorrelationId));

            Initially(
                When(UserDeletionRequested)
                    .Then(context =>
                    {
                        context.Instance.CorrelationId = context.Message.UserId;
                        Console.WriteLine($"Пользователь с ID: {context.Message.UserId} в процессе удаления");
                    })
                    .TransitionTo(AwaitingUserDeletion));

            During(AwaitingUserDeletion,
                When(UserDeleted)
                    .Then(context =>
                    {
                        Console.WriteLine($"Пользователь с ID: {context.Message.CorrelationId} удалён");
                    })
                    .Publish(context => new DeleteShoppingListSagaCommand
                    {
                        UserId = context.Message.CorrelationId
                    })
                    .Publish(context => new DeleteWalletSagaCommand
                    {
                        UserId = context.Message.CorrelationId
                    })
                    .Finalize());

            SetCompletedWhenFinalized();
        }

        public State AwaitingUserDeletion { get; private set; }

        public Event<DeleteUserSagaCommand> UserDeletionRequested { get; set; }
        public Event<UserDeletedEvent> UserDeleted { get; set; }

    }
}
