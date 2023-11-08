using VehicleOrganizer.Infrastructure.Criteria;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public interface IEmailService
    {
        Task RemindUserAboutActivitiesAsync(User user, OperationalActivityCriteria criteria = null);
        Task RemindUserAboutVehicleInsuranceOrTechnicalReviewAsync(User user, DateTime referenceDate);
        Task InformAdminAboutProblemAsync(IList<string> errors);
    }
}
