using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public class OpertationalActivitySummary
    {
        public string VehicleName { get; set; }
        public IList<string> ActivitySummaries { get; set; } = new List<string>();

        private OpertationalActivitySummary(string vehicleName, IList<string> activitySummaries)
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
                var activitiesForSummaryPropmpts = operationalActivities.Where(oa => oa.Vehicle.Id == vehicle.Id)
                    .Select(a => a.SummaryPrompt(referenceDate))
                    .ToList();

                var insurancTerminationPrompt = vehicle.InsuranceTerminationPrompt(referenceDate);
                if (insurancTerminationPrompt is not null)
                {
                    activitiesForSummaryPropmpts.Add(insurancTerminationPrompt);
                }

                var summary = new OpertationalActivitySummary(vehicle.Name, activitiesForSummaryPropmpts);                
                summaries.Add(summary);               
            }

            return summaries;
        }
    }
}
