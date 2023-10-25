using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public interface IEmailService
    {
        Task RemindUserAboutActivitiesAsync(User user);
        Task RemindAllUsersAboutActivitiesAsync();
    }
}
