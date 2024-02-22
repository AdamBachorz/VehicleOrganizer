using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using VehicleOrganizer.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using BachorzLibrary.DAL.DotNetSix.Utils;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Domain.Abstractions.Utils;

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
        //optionsBuilder.EnableSensitiveDataLogging();
        string configFile = EnvUtils.GetValueDependingOnEnvironment(Codes.Files.DevConfig, Codes.Files.ProdConfig);
        DbContextUtils.ExplicitConfig(optionsBuilder, configFile);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

