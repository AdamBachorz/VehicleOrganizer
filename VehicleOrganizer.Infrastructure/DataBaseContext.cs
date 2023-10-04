﻿using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using VehicleOrganizer.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VehicleOrganizer.Domain.Abstractions.Utils;
using VehicleOrganizer.Domain.Abstractions;

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
        string environment;
#if DEBUG
        environment = "DEV";
#else
        environment = "PROD";
#endif

        var configFile = FileUtils.GetProperFileByEnv(Codes.Files.DevConfig, Codes.Files.ProdConfig);

        if (!File.Exists(configFile))
        {
            throw new FileLoadException("Config file not found", environment);
        }

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

