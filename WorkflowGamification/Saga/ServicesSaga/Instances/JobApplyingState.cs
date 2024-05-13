using MassTransit;

namespace ServicesSaga.Instances
{
    public class JobApplyingState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public int CurrentState { get; set; }

        public Guid EmployeeId { get; set; }

        public decimal MoneyAmount { get; set; }
    }
}
