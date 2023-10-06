using BachorzLibrary.DAL.DotNetSix.Repositories;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.Infrastructure.Repositories
{
    public class OperationalActivityRepository : EFCBaseRepository<OperationalActivity, DataBaseContext, int>, IOperationalActivityRepository
    {
        public OperationalActivityRepository(DataBaseContext db) : base(db)
        {
        }
    }
}
