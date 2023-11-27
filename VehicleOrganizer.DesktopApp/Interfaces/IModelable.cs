namespace VehicleOrganizer.DesktopApp.Interfaces
{
    public interface IModelable<M>
    {
        M Model { get; set; }
        void FillUpControls(M model);
        M ApplyModelDataFromControls();
        void SaveChangesToExistingEntity(M newData);
    }
}
