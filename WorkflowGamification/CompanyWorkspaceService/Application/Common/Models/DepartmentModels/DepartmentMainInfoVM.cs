using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models.DepartmentModels
{
    public class DepartmentMainInfoVM : IMapWith<Department>
    {
        public required string DepartmentName { get; set; }

        public string? DepartmentDescription { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Department, DepartmentMainInfoVM>();
        }
    }
}
