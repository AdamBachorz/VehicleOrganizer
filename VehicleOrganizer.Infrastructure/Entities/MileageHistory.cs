using BachorzLibrary.Common.DbModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleOrganizer.Infrastructure.Entities
{
    public class MileageHistory : Entity
    {
        public int Mileage { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AddDate { get; set; } = DateTime.Now.Date;
        public Vehicle Vehicle { get; set; }
    }
}
