using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models.DepartmentModels
{
    public class DepartmentVM : IMapWith<Department>
    {
        public required Guid Id { get; set; }

        public required string DepartmentName { get; set; }

        public string? DepartmentDescription { get; set; }

        public Guid DirectorId { get; set; }

        public IList<Guid>? EmployeesId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Department, DepartmentVM>();
        }
    }
}
