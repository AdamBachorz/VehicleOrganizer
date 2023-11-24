namespace VehicleOrganizer.Infrastructure.Abstractions.Validators.Criteria
{
    public class OperationalActivityValidationCriteria : BaseValidationCriteria
    {
        public bool ActivityIsDateOperated { get; set; }
        public bool MileageWhenPerformedIsNotDigit { get; set; }
        public bool MileageWhenPerformedIsNegative { get; set; }
        public bool MileageStepIsNotDigit { get; set; }
        public bool MileageStepIsNegative { get; set; }
    }
}
