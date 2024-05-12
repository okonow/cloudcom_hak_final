using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models
{
    public class EmployeeStatisticsVM : IMapWith<EmployeeStatistics>
    {
        public int CompletedEasyJobsCount { get; set; }

        public int CompletedNormalJobsCount { get; set; }

        public int CompletedDifficultJobsCount { get; set; }

        public TimeSpan AverageTimeForCompletingJob { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeStatistics, EmployeeStatisticsVM>();
        }
    }
}
