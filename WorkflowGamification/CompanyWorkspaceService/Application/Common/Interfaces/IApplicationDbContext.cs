﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Department> Departments { get; }

        DbSet<Job> Jobs { get; }

        DbSet<EmployeeStatistics> Statistics { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
