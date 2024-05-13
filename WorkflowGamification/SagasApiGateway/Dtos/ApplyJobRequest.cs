namespace WorkflowGamificationWebApp.Server.Dtos
{
    public class ApplyJobRequest
    {
        public Guid JobId { get; set; }

        public Guid DirectorId { get; set; }

        public Guid EmployeeId { get; set; }

        public decimal MoneyAmount { get; set; }
    }
}
