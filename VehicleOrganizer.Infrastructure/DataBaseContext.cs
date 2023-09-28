using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using VehicleOrganizer.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace VehicleOrganizer.Infrastructure;

public class DataBaseContext : BaseDbContext
{
    

    public DataBaseContext()
    {
    }

    public DataBaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        

    }
}

