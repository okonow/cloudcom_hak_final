using Application.Jobs.Commands.PatchCommands;
using Contracts.ApplyJobContracts;
using MassTransit;
using MediatR;

namespace CompanyWorkspaceService.Consumers
{
    public class FinishJobConsumer(ISender sender) : IConsumer<FinishJobSagaCommand>
    {
        private readonly ISender _sender = sender;

        // UNDONE
        public async Task Consume(ConsumeContext<FinishJobSagaCommand> context)
        {
            try
            {
                var message = context.Message;
                await _sender.Send(new FinishJobCommand { JobId = message.JobId });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}
