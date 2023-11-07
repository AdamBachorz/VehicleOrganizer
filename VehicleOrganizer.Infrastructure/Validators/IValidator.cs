namespace VehicleOrganizer.Infrastructure.Validators
{
    public interface IValidator<T>
    {
        public IEnumerable<string> Validate(T targetType);
    }
}
