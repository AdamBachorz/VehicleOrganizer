using BachorzLibrary.DAL.DAO;
using VehicleOrganizer.Infrastructure.Criteria;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Services.Email;

namespace VehicleOrganizer.Infrastructure.Repositories.Interfaces
{
    public interface IOperationalActivityRepository : IBaseDao<OperationalActivity, int>
    {
        Task<IList<OperationalActivity>> GetOperationalActivitiesForVehicleAndUserAsync(int vehicleId, User user);
        Task<IList<OperationalActivitySummary>> GetOperationalActivitiesForUserToRemindAsync(User user, OperationalActivityCriteria criteria);
    }
}
