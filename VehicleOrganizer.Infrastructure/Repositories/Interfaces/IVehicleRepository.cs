using BachorzLibrary.DAL.DAO;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Repositories.Interfaces
{
    public interface IVehicleRepository : IBaseDao<Vehicle, int>
    {
        bool UserHasVehicle(User user);
        bool UserHasVehicleWithName(User user, string vehicleName);
        Task<IList<Vehicle>> GetVehiclesForUserAsync(User user, bool includeSold = false);
        Task<Vehicle> AddVehicleAsync(Vehicle vehcle, int mileage);
        Task UpdateMileageAsync(Vehicle vehicle, int mileage);
        Task UpdateInsuranceDateAsync(Vehicle vehicle, DateTime newInsuranceConclusionDate);
        Task UpdateTechnicalReviewDateAsync(Vehicle vehicle, DateTime latestReviewDate);
        Task SaleVehicleAsync(Vehicle vehicle, DateTime saleDate);
        Task<IList<Vehicle>> GetVehiclesWithCloseInsuranceTermination(User user, DateTime referenceDate);
        Task<IList<Vehicle>> GetVehiclesWithCloseNextReviewDate(User user, DateTime referenceDate);
    }
}
