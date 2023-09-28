using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using VehicleOrganizer.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace VehicleOrganizer.Infrastructure;

public class DataBaseContext : BaseDbContext
{
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<MileageHistory> MileageHistory { get; set; }
    public DbSet<OperationalActivity> OperationalActivities { get; set; }

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

