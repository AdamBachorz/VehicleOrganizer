using BachorzLibrary.Common.DbModel;
using BachorzLibrary.Common.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Domain.Abstractions.Enums;

namespace VehicleOrganizer.Infrastructure.Entities
{
    public class Vehicle : Entity
    {
        public string Name { get; set; }
        public int YearOfProduction { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now.Date;
        public DateTime RegistrationDate { get; set; } = DateTime.Now.Date;
        public VehicleType VehicleType { get; set; }
        public string OilType { get; set; }
        public DateTime InsuranceConclusion { get; set; } = DateTime.Now.Date;
        public DateTime InsuranceTermination { get; set; } = DateTime.Now.Date.AddYears(1);
        public DateTime LastTechnicalReview { get; set; } = DateTime.Now.Date;
        public DateTime NextTechnicalReview { get; set; } = DateTime.Now.Date.AddYears(1);
        public DateTime? SaleDate { get; set; }
        public User User { get; set; } = User.Default;
        public IList<MileageHistory> MileageHistory { get; set; }
        public IList<OperationalActivity> OperationalActivities { get; set; }

        [NotMapped]
        public bool IsSold => SaleDate.HasValue;
        [NotMapped]
        public int LatestMileage => MileageHistory.IsNotNullOrEmpty() ? MileageHistory?.LastOrDefault()?.Mileage ?? 0 : 0;
        
        public int DaysToInsuranceExpires(DateTime referenceDate) => (int)(InsuranceTermination - referenceDate).TotalDays;
        public int DaysToNextTechnicalReview(DateTime referenceDate) => (int)(NextTechnicalReview - referenceDate).TotalDays;
        
        public string? InsuranceTerminationPrompt(DateTime referenceDate)
        {
            var daysToInsuranceTermination = DaysToInsuranceExpires(referenceDate);
            if (daysToInsuranceTermination <= Codes.Defaults.DaysToRemindAboutInsuranceTermination)
            {
                return daysToInsuranceTermination > 0
                    ? $"Ubezpieczenie wygasa za {daysToInsuranceTermination} dni"
                    : $"Ubezpieczenie wygasło {Math.Abs(daysToInsuranceTermination)} dni temu";
            } else return null;
        }
        
        public string? TechnicalReviewPrompt(DateTime referenceDate)
        {
            var daysToNextTechnicalReview = DaysToNextTechnicalReview(referenceDate);
            if (daysToNextTechnicalReview <= Codes.Defaults.DaysToRemindAboutTechnicalReview)
            {
                return daysToNextTechnicalReview > 0
                    ? $"Przegląd techniczny wygasa za {daysToNextTechnicalReview} dni"
                    : $"Przegląd techniczny wygasł {Math.Abs(daysToNextTechnicalReview)} dni temu";
            } else return null;
        }
    }
}
