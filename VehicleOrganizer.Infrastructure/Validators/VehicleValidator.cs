using BachorzLibrary.Common.Extensions;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Validators
{
    public class VehicleValidator : IValidator<Vehicle>
    {
        public IEnumerable<string> Validate(Vehicle vehicle)
        {
            if (vehicle.Name.IsNullOrEmpty())
            {
                yield return "Brak nazwy pojazdu";
            }
        }
    }
}
