using BachorzLibrary.Common.Extensions;
using VehicleOrganizer.Infrastructure.Abstractions.Validators;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Validators.Criteria;

namespace VehicleOrganizer.Infrastructure.Validators
{
    public class VehicleValidator : IValidator<Vehicle, VehicleValidationCriteria>
    {
        public IEnumerable<string> Validate(Vehicle vehicle, VehicleValidationCriteria criteria)
        {
            if (vehicle.Name.IsNullOrEmpty())
            {
                yield return "Brak nazwy pojazdu";
            }
        }
    }
}
