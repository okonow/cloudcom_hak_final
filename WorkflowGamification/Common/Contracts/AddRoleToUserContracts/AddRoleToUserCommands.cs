namespace Contracts.AddRoleToUserContracts
{
    public record AddRoleToUserSagaCommand
    {
        public required Guid UserId { get; set; }

        public required string Role { get; set; }
    }

    public record CreateEmployeeStatisticsSagaCommand
    {
        public required Guid UserId { get; set; }
    }
}
