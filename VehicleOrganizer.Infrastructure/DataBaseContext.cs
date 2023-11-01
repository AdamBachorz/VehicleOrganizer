using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using VehicleOrganizer.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using BachorzLibrary.DAL.DotNetSix.Utils;
using BachorzLibrary.Common.Utils;
using VehicleOrganizer.Domain.Abstractions;

namespace VehicleOrganizer.Infrastructure;

public class DataBaseContext : BaseDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<MileageHistory> MileageHistories { get; set; }
    public DbSet<OperationalActivity> OperationalActivities { get; set; }

    public DataBaseContext() : base()
    {
    }

    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        DbContextUtils.ExplicitConfig(optionsBuilder, EnvUtils.GetValueDependingOnEnvironment(Codes.Files.DevConfig, Codes.Files.ProdConfig));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

