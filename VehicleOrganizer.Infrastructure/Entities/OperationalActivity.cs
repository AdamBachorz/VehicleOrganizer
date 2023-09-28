using BachorzLibrary.Common.DbModel;
using System;
using System.Collections.Generic;
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

        public DateTime NextOperationDate => LastOperationDate.AddYears(YearsStep);
        public int NextOperationAtMilage => MileageWhenPerformed + MileageStep;

        public int ToNextAct => IsDateOperated ? (int)(NextOperationDate - LastOperationDate).TotalDays : NextOperationAtMilage - Vehicle.LatestMileage;
    }
}
