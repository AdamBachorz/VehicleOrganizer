using BachorzLibrary.Common.DbModel;
using BachorzLibrary.Common.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleOrganizer.Domain.Abstractions.Enums;

namespace VehicleOrganizer.Infrastructure.Entities
{
    public class Vehicle : Entity
    {
        public string Name { get; set; }
        public VehicleType VehicleType { get; set; }
        public string OilType { get; set; }
        public DateTime InsuranceConclusion { get; set; } = DateTime.Now.Date;
        public DateTime InsuranceTermination { get; set; } = DateTime.Now.Date.AddYears(1);
        public User User { get; set; } = User.Default;
        public IList<MileageHistory> MileageHistory { get; set; }
        public IList<OperationalActivity> OperationalActivities { get; set; }

        [NotMapped]
        public int LatestMileage => MileageHistory.IsNotNullOrEmpty() ? MileageHistory?.LastOrDefault()?.Mileage ?? 0 : 0;
        [NotMapped]
        public int DaysToInsuranceExpires => (int)(InsuranceTermination - InsuranceConclusion).TotalDays;
    }
}
