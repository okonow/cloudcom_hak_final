using Contracts.Models;
using MassTransit;

namespace ServicesSaga.Instances
{
    public class UserCreationState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public int CurrentState { get; set; }
    }
}
