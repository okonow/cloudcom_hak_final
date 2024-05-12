using Application.Common.Interfaces;
using AutoMapper;
using Domain.ValueObjects;

namespace Application.Common.Models.JobModels
{
    public class JobAnswerVM : IMapWith<JobAnswer>
    {
        public string? Description { get; set; }

        public StoredFile? AttachedFile { get; set; }

        public string? Comment { get; set; }

        public DateTime DepartureTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobAnswer, JobAnswerVM>();
        }
    }
}
