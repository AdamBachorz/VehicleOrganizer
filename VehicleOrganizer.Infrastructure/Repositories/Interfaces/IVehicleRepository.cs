using BachorzLibrary.DAL.DAO;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Repositories.Interfaces
{
    public interface IVehicleRepository : IBaseDao<Vehicle, int>
    {
        bool UserHasVehicle(User user);
        Task<IList<Vehicle>> GetVehiclesForUser(User user, bool includeSold = false);
        Task<Vehicle> AddVehicle(Vehicle vehcle, int mileage);
        Task UpdateMileage(Vehicle vehicle, int mileage);
        Task SaleVehicle(Vehicle vehicle, DateTime saleDate);
    }
}
