namespace VehicleOrganizer.DesktopApp.Interfaces
{
    public interface IModelable<M>
    {
        void FillUpControls(M model);
        M ApplyModelDataFromControls();
    }
}
