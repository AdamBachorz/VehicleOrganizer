using VehicleOrganizer.Infrastructure.Criteria;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public interface IEmailService
    {
        Task RemindUserAboutActivitiesAsync(User user, OperationalActivityCriteria criteria = null);
        Task RemindAllUsersAboutActivitiesAsync();
    }
}
