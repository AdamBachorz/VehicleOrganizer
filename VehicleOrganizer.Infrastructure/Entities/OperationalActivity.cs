using BachorzLibrary.Common.DbModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleOrganizer.Infrastructure.Entities
{
    public class OperationalActivity : Entity
    {
        public string Name { get; set; }
        public Vehicle Vehicle { get; set; }
        public bool IsDateOperated { get; set; }
        public int MileageStep { get; set; }
        public int YearsStep { get; set; }
        public int MileageWhenPerformed { get; set; }
        public DateTime LastOperationDate { get; set; } = DateTime.Now.Date;

        [NotMapped]
        public DateTime NextOperationDate => LastOperationDate.AddYears(YearsStep);
        [NotMapped]
        public int NextOperationAtMilage => MileageWhenPerformed + MileageStep;

        [NotMapped]
        public int ToNextAct => IsDateOperated ? (int)(NextOperationDate - LastOperationDate).TotalDays : NextOperationAtMilage - Vehicle.LatestMileage;
    }
}
