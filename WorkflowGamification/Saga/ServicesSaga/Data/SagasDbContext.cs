using Microsoft.EntityFrameworkCore;
using ServicesSaga.Instances;

namespace ServicesSaga.Data
{
    public class SagasDbContext : DbContext
    {
        public SagasDbContext(DbContextOptions<SagasDbContext> options) : base(options)
        {
        }

        public DbSet<UserCreationState> UserCreationStates { get; set; }

        public DbSet<UserDeletionState> UserDeletionStates { get; set; }

        public DbSet<JobApplyingState> JobApplyStates { get; set; }
    }
}
