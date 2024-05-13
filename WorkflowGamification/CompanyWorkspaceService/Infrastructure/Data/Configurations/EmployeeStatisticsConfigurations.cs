using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class EmployeeStatisticsConfigurations : IEntityTypeConfiguration<EmployeeStatistics>
    {
        public void Configure(EntityTypeBuilder<EmployeeStatistics> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
