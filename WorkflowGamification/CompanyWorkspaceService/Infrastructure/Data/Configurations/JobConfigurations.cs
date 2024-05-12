using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class JobConfigurations : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(j => j.Id);

            builder.OwnsOne(j => j.JobMetadata);

            builder.OwnsMany(j => j.JobAnswers)
                .OwnsOne(a => a.AttachedFile);
        }
    }
}
