namespace VehicleOrganizer.Infrastructure.Abstractions.Validators.Criteria
{
    public class VehicleValidationCriteria : BaseValidationCriteria
    {
        public bool ShouldCheckOilType { get; set; }
        public bool VehicleTypeIsNotSelected { get; set; }
        public bool ShouldCheckMileage { get; set; }
        public bool MileageIsNotDigit { get; set; }
        public bool MileageIsNegative { get; set; }
    }
}
