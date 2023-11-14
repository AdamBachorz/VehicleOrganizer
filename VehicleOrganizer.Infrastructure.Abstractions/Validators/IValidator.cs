using BachorzLibrary.Common.Extensions;

namespace VehicleOrganizer.Infrastructure.Abstractions.Validators
{
    public interface IValidator<T, VC> where VC : BaseValidationCriteria
    {
        IEnumerable<string> Validate(T targetType, VC criteria = null);

        public IEnumerable<string> ValidateToBulletPointList(T targetType, VC criteria = null, string bulletPointer = "-")
            => Validate(targetType, criteria).Select(x => $"{bulletPointer} {x}");
        public string? ValidateToBulletPointString(T targetType, VC criteria = null, string bulletPointer = "-") 
            => ValidateToBulletPointList(targetType, criteria, bulletPointer).Join(Environment.NewLine);
    }
}
