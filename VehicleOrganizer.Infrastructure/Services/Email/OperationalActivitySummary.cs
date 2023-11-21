using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public class OperationalActivitySummary
    {
        public string VehicleName { get; set; }
        public IList<string> ActivitySummaries { get; set; } = new List<string>();

        private OperationalActivitySummary(string vehicleName, IList<string> activitySummaries)
        {
            VehicleName = vehicleName;
            ActivitySummaries = activitySummaries;
        }

        public static IList<OperationalActivitySummary> BuildList(IList<OperationalActivity>? operationalActivities, DateTime referenceDate)
        {
            if (operationalActivities is null)
            {
                return null;
            }

            var vehicles = operationalActivities.Select(oa => oa.Vehicle).Distinct();

            var summaries = new List<OperationalActivitySummary>();
            foreach (var vehicle in vehicles)
            {
                var activitiesForSummaryPropmpts = operationalActivities.Where(oa => oa.Vehicle.Id == vehicle.Id)
                    .Select(a => a.SummaryPrompt(referenceDate))
                    .ToList();

                var summary = new OperationalActivitySummary(vehicle.Name, activitiesForSummaryPropmpts);                
                summaries.Add(summary);               
            }

            return summaries;
        }
    }
}
