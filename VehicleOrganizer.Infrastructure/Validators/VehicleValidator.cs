using BachorzLibrary.Common.Extensions;
using VehicleOrganizer.Infrastructure.Abstractions.Validators;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Validators.Criteria;

namespace VehicleOrganizer.Infrastructure.Validators
{
    public class VehicleValidator : IValidator<Vehicle, VehicleValidationCriteria>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleValidator(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public IEnumerable<string> Validate(Vehicle vehicle, VehicleValidationCriteria criteria = null)
        {
            criteria ??= new VehicleValidationCriteria();

            if (vehicle.Name.IsNullOrEmpty())
            {
                yield return "Brak nazwy pojazdu";
            }

            if (criteria.ShouldCheckOilType)
            {
                yield return "Nie podano typu oleju";
            }

            if (criteria.VehicleTypeIsNotSelected)
            {
                yield return "Nie wybrano typu pojazdu";
            }

            if (criteria.ShouldCheckMileage && criteria.MileageIsNotDigit)
            {
                yield return "W polu przebiegu pojazdu nie podano liczby";
            }

            var yearOfProduction = vehicle.YearOfProduction;
            var purchaseDate = vehicle.PurchaseDate.Date;
            var registrationDate = vehicle.RegistrationDate.Date;

            if (registrationDate < purchaseDate) 
            {
                yield return "Data rejestracji pojazdu nie może być wcześniejsza, niż data jego zakupu";
            }

            if (yearOfProduction > purchaseDate.Year)
            {
                yield return "Podany rok produkcji jest wyższy, niż rok, w którym pojazd został zakupiony";
            }

            if (yearOfProduction > registrationDate.Year)
            {
                yield return "Podany rok produkcji jest wyższy, niż rok, w którym pojazd został zarejestrowany";
            }

            var insuranceConclusionDate = vehicle.InsuranceConclusion.Date;
            var insuranceTerminationDate = vehicle.InsuranceTermination.Date;

            var lastTechnicalReviweDate = vehicle.LastTechnicalReview.Date;
            var nextTechnicalReviweDate = vehicle.NextTechnicalReview.Date;
        }
    }
}
