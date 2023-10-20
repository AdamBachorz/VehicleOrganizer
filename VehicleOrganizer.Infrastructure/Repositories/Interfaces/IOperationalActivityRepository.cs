using BachorzLibrary.DAL.DAO;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Services.Email;

namespace VehicleOrganizer.Infrastructure.Repositories.Interfaces
{
    public interface IOperationalActivityRepository : IBaseDao<OperationalActivity, int>
    {
        Task<IList<OpertationalActivitySummary>> GetOpertationalActivitiesForUserToRemindAsync(User user, (int DateDays, int Milage) referenceThreshold, 
            DateTime referenceDate, bool shouldSetReminderDate);
    }
}
