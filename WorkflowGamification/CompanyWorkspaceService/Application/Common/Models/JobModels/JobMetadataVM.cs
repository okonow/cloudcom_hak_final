using Application.Common.Interfaces;
using AutoMapper;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Models.JobModels
{
    public class JobMetadataVM : IMapWith<JobMetadata>
    {
        public DateTime Deadline { get; set; }

        public Complexity Complexity { get; set; }

        public Guid CreatorId { get; set; }

        public Guid? WorkerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobMetadata, JobMetadataVM>().ForMember(m => m.Complexity,
                opt => opt.MapFrom(d => (int)d.Complexity));
        }
    }
}
