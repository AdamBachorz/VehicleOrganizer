namespace VehicleOrganizer.Infrastructure.Abstractions.Validators
{
    public interface IValidator<T, VC> where VC : BaseValidationCriteria
    {
        public IEnumerable<string> Validate(T targetType, VC cirteria);
    }
}
