using VehicleOrganizer.Domain.Abstractions;

namespace VehicleOrganizer.Infrastructure.Criteria
{
    public class OperationalActivityCriteria
    {
        public int DaysToRemind { get; set; } = Codes.Defaults.DaysToRemindAboutActivity;
        public int MileageToRemind { get; set; } = Codes.Defaults.MileageToRemindAboutActivity;
        public DateTime ReferenceDate { get; set; } = DateTime.Now.Date;
        public bool ShouldSetReminderDate { get; set; } = false;
    }
}
