using BachorzLibrary.Common.DbModel;

namespace VehicleOrganizer.Infrastructure.Entities
{
    public class MileageHistory : Entity
    {
        public int Mileage { get; set; }
        public DateTime AddDate { get; set; } = DateTime.Now.Date;
        public Vehicle Vehicle { get; set; }
    }
}
