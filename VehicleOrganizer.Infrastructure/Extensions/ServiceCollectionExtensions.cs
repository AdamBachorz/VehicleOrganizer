using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using BachorzLibrary.DAL.DotNetSix.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Repositories;

namespace VehicleOrganizer.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, string configFileContent, Action<DbContextOptionsBuilder> options)
        {
            var config = JsonConvert.DeserializeObject<EFCCustomConfig>(configFileContent);
            services.AddSingleton<IEFCCustomConfig>(config);
            services.AddDbContext<DataBaseContext>(options);

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IOperationalActivityRepository, OperationalActivityRepository>();
        }
    }
}
