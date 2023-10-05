using BachorzLibrary.DAL.DAO;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Repositories.Interfaces
{
    public interface IVehicleRepository : IBaseDao<Vehicle, int>
    {
        Task<IList<Vehicle>> GetVehiclesForUser(User user, bool includeSold = false);
        Task<Vehicle> AddVehicle(Vehicle vehcle, int mileage);
        Task SaleVehicle(Vehicle vehcle, DateTime saleDate);
    }
}
