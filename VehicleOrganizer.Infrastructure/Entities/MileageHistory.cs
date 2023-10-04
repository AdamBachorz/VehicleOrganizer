using BachorzLibrary.Common.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleOrganizer.Infrastructure.Entities
{
    public class MileageHistory : Entity
    {
        public int Mileage { get; set; }
        public DateTime AddDate { get; set; } = DateTime.Now.Date;
        public Vehicle Vehicle { get; set; }
    }
}
