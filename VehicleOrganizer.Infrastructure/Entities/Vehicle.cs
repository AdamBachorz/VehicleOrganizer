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
        [Column(TypeName = "datetime")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now.Date;
        [Column(TypeName = "datetime")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now.Date;
        public VehicleType VehicleType { get; set; }
        public string OilType { get; set; } = Codes.None;
        [Column(TypeName = "datetime")] 
        public DateTime InsuranceConclusion { get; set; } = DateTime.Now.Date;
        [Column(TypeName = "datetime")]
        public DateTime InsuranceTermination { get; set; } = DateTime.Now.Date.AddYears(1);
        [Column(TypeName = "datetime")]
        public DateTime LastTechnicalReview { get; set; } = DateTime.Now.Date;
        [Column(TypeName = "datetime")]
        public DateTime NextTechnicalReview { get; set; } = DateTime.Now.Date.AddYears(1);
        [Column(TypeName = "datetime")]
        public DateTime? SaleDate { get; set; } = null;
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
            var prefix = "Ubezpieczenie";
            var daysToInsuranceTermination = DaysToInsuranceExpires(referenceDate);
            if (daysToInsuranceTermination <= Codes.Defaults.DaysToRemindAboutInsuranceTermination)
            {
                if (daysToInsuranceTermination == 0)
                {
                    return $"{prefix} właśnie wygasło";
                }

                return daysToInsuranceTermination > 0
                    ? $"{prefix} wygasa za {daysToInsuranceTermination} dni"
                    : $"{prefix} wygasło {Math.Abs(daysToInsuranceTermination)} dni temu";
            } else return null;
        }
        
        public string? TechnicalReviewPrompt(DateTime referenceDate)
        {
            var prefix = "Przegląd techniczny";
            var daysToNextTechnicalReview = DaysToNextTechnicalReview(referenceDate);
            if (daysToNextTechnicalReview <= Codes.Defaults.DaysToRemindAboutTechnicalReview)
            {
                if (daysToNextTechnicalReview == 0)
                {
                    return $"{prefix} właśnie wygasł";
                }

                return daysToNextTechnicalReview > 0
                    ? $"{prefix} wygasa za {daysToNextTechnicalReview} dni"
                    : $"{prefix} wygasł {Math.Abs(daysToNextTechnicalReview)} dni temu";
            } else return null;
        }
    }
}
