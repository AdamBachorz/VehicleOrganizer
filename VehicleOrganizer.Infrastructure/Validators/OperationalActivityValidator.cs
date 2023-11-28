using BachorzLibrary.Common.Extensions;
using VehicleOrganizer.Infrastructure.Abstractions.Validators;
using VehicleOrganizer.Infrastructure.Abstractions.Validators.Criteria;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Validators
{
    public class OperationalActivityValidator : IValidator<OperationalActivity, OperationalActivityValidationCriteria>
    {
        
        public IEnumerable<string> Validate(OperationalActivity operationalActivity, OperationalActivityValidationCriteria criteria = null)
        {
            criteria ??= new OperationalActivityValidationCriteria();

            if (operationalActivity.Name.HasNotValue())
            {
                yield return "Brak nazwy aktywności";
            }

            if (!criteria.ActivityIsDateOperated)
            {
                if (criteria.MileageWhenPerformedIsNotDigit)
                {
                    yield return "W polu przebiegu pojazdu w momencie wykonania operacji nie podano liczby";
                }

                if (criteria.MileageStepIsNotDigit)
                {
                    yield return "W polu, w którym powinna znajdować się ilość kilometrów do następnej operacji nie podano liczby";
                }

                if (criteria.MileageWhenPerformedIsNegative)
                {
                    yield return "Przebieg pojazdu w momencie wykonania operacji nie może byc ujemny";
                }

                if (criteria.MileageStepIsNegative)
                {
                    yield return "Ilość kilometrów do następnej operacji nie może byc ujemny";
                }
            }
            else // Mileage operated
            {
                if (criteria.MileageWhenPerformedIsLessThanLatestMileage)
                {
                    yield return "Podany przebieg pojazdu w momencie wykonania operacji jest mniejszy, niż jego aktualny przebieg";
                }
            }
        }
    }
}
