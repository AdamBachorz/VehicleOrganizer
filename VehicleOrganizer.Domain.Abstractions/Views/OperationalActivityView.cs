namespace VehicleOrganizer.Domain.Abstractions.Views
{
    public class OperationalActivityView
    {
        public int Reference { get; set; }
        public string Name { get; set; }
        public string LastOperationDateOrMileageWhenPerformed { get; set; }
        public string SummaryPrompt { get; set; }
    }
}
