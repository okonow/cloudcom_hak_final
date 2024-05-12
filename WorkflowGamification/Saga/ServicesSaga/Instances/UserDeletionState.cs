using MassTransit;

namespace ServicesSaga.Instances
{
    public class UserDeletionState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public int CurrentState { get; set; }

        public required string JwtToken { get; set; }
    }
}
