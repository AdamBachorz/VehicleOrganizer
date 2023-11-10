using VehicleOrganizer.Infrastructure.Abstractions.Validators;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Validators.Criteria
{
    public class VehicleValidationCriteria : BaseValidationCriteria
    {
        public User User { get; set; }
    }
}
