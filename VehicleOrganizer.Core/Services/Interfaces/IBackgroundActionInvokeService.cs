namespace VehicleOrganizer.Core.Services.Interfaces
{
    public interface IBackgroundActionInvokeService
    {
        IList<string> CurrentErrors();
        Task AuthorizeDefaultUserAsync();
        Task InvokeAllAsync();
        Task RunRemindersAsync();
    }
}
