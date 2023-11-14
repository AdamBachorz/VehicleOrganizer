using VehicleOrganizer.Infrastructure.Abstractions.Validators;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Validators.Criteria
{
    public class VehicleValidationCriteria : BaseValidationCriteria
    {
        public User User { get; set; } = User.Default;
        public bool ShouldCheckOilType { get; set; }
        public bool VehicleTypeIsNotSelected { get; set; }
        public bool ShouldCheckMileage { get; set; }
        public bool MileageIsNotDigit { get; set; }
    }
}
