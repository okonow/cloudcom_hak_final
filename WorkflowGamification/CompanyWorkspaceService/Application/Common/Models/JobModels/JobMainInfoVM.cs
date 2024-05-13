using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models.JobModels
{
    public class JobMainInfoVM : IMapWith<Job>
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public bool IsFinished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Job, JobMainInfoVM>();
        }
    }
}
