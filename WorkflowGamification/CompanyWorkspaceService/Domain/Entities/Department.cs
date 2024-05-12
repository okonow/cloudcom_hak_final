using Domain.Common;

namespace Domain.Entities
{
    public class Department : BaseEntity
    {
        public required string DepartmentName { get; set; }

        public string? DepartmentDescription { get; set; } 

        public Guid DirectorId { get; set; }

        public IList<Guid>? DepartmentEmployeesId { get; set; } = [];
    }
}
