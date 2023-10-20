using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public class OpertationalActivitySummary
    {
        public string VehicleName { get; set; }
        public IList<string> ActivitySummaries { get; set; } = new List<string>();

        public OpertationalActivitySummary(string vehicleName, IList<string> activitySummaries)
        {
            VehicleName = vehicleName;
            ActivitySummaries = activitySummaries;
        }

        public static IList<OpertationalActivitySummary> BuildList(IList<OperationalActivity>? operationalActivities, DateTime referenceDate)
        {
            if (operationalActivities is null)
            {
                return null;
            }

            var vehicles = operationalActivities.Select(oa => oa.Vehicle).Distinct();

            var summaries = new List<OpertationalActivitySummary>();
            foreach (var vehicle in vehicles)
            {
                var activitiesForSummary = operationalActivities.Where(oa => oa.Vehicle.Id == vehicle.Id);
                var summary = new OpertationalActivitySummary(vehicle.Name, activitiesForSummary.Select(a => a.SummaryPrompt(referenceDate)).ToList());
                // TODO Move to a different service
                //var daysToInsuranceTermination = vehicle.DaysToInsuranceExpires(referenceDate);
                //if (daysToInsuranceTermination <= 30)
                //{
                //    summary.ActivitySummaries.Add(daysToInsuranceTermination > 0 
                //        ? $"Ubezpieczenie wygasa za {daysToInsuranceTermination} dni"
                //        : $"Ubezpieczenie wygasło {Math.Abs(daysToInsuranceTermination)} dni temu");
                //}
                summaries.Add(summary);
            }

            return summaries;
        }
    }
}
