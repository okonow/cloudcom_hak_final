using Contracts.ApplyJobContracts;
using MassTransit;
using ServicesSaga.Instances;

namespace ServicesSaga.StateMachines
{
    public class JobApplyingSaga : MassTransitStateMachine<JobApplyingState>
    {
        public JobApplyingSaga()
        {
            InstanceState(s => s.CurrentState);

            Event(() => FinishJobRequested, s => s.CorrelateById(x => x.Message.DirectorId));
            Event(() => JobFinished, s => s.CorrelateById(x => x.Message.DirectorId));

            Initially(
                When(FinishJobRequested)
                    .Then(c =>
                    {
                        c.Instance.CorrelationId = c.Message.DirectorId;
                        c.Instance.EmployeeId = c.Message.EmployeeId;
                        c.Instance.MoneyAmount = c.Message.MoneyAmount;
                    })
                    .TransitionTo(AwaitingJobFinishing));
            During(AwaitingJobFinishing,
                When(JobFinished)
                .Publish(c => new SendToMoneyToUserSagaCommand
                {
                    SenderId = c.Instance.CorrelationId,
                    ReceiverId = c.Instance.EmployeeId,
                    MoneyAmount = c.Instance.MoneyAmount
                })
                .Finalize());
            SetCompletedWhenFinalized();
        }

        public State  AwaitingJobFinishing { get; private set; }

        public Event<FinishJobSagaCommand> FinishJobRequested { get; set; }

        public Event<JobFinishedEvent> JobFinished { get; set; }
    }
}
