using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models.DepartmentModels
{
    public class DepartmentStaffVM : IMapWith<Department>
    {
        public Guid DirectorId { get; set; }

        public IList<Guid>? EmployeesId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Department, DepartmentStaffVM>();
        }
    }
}
