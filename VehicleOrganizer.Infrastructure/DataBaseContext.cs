using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using VehicleOrganizer.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Xceed.Document.NET;
using Newtonsoft.Json;
using VehicleOrganizer.Domain.Abstractions;
using Microsoft.Extensions.Options;

namespace VehicleOrganizer.Infrastructure;

public class DataBaseContext : BaseDbContext
{
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
        string configFile;
#if DEBUG
        configFile = Codes.Files.DevConfig;
#else
        environment = Codes.Files.ProdConfig;           
#endif
        var config = JsonConvert.DeserializeObject<EFCCustomConfig>(File.ReadAllText(configFile));
        if (config.IsProduction)
        {
            optionsBuilder.UseNpgsql(config.ConnectionString);
        }
        else
        {
            optionsBuilder.UseSqlite(config.ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        

    }
}

