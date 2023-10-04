using BachorzLibrary.Common.DbModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleOrganizer.Infrastructure.Entities
{
    public class OperationalActivity : Entity
    {
        public string Name { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime LastOperationDate { get; set; } = DateTime.Now.Date;
        public int MileageWhenPerformed { get; set; }
        public bool IsDateOperated { get; set; }

        /// <summary>
        /// How many kilometers can car be driven to the next this type operation 
        /// </summary>
        public int MileageStep { get; set; }

        /// <summary>
        /// How many years can pass to the next this type operation 
        /// </summary>
        public int YearsStep { get; set; }

        [NotMapped]
        public DateTime NextOperationDate => LastOperationDate.AddYears(YearsStep);
        [NotMapped]
        public int NextOperationAtMilage => MileageWhenPerformed + MileageStep;

        [NotMapped]
        public int ToNextAct => IsDateOperated ? (int)(NextOperationDate - LastOperationDate).TotalDays : NextOperationAtMilage - Vehicle.LatestMileage;
    }
}
