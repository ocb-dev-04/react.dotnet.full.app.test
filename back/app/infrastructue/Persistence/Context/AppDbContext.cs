﻿using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public sealed class AppDbContext 
    : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}