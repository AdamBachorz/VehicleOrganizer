using BachorzLibrary.Common.Extensions;
using BachorzLibrary.DAL.DotNetSix.Repositories;
using Microsoft.EntityFrameworkCore;
using VehicleOrganizer.Infrastructure.Criteria;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Services.Email;

namespace VehicleOrganizer.Infrastructure.Repositories
{
    public class OperationalActivityRepository : EFCBaseRepository<OperationalActivity, DataBaseContext, int>, IOperationalActivityRepository
    {
        public OperationalActivityRepository(DataBaseContext db) : base(db)
        {
        }

        public async Task<IList<OpertationalActivitySummary>> GetOpertationalActivitiesForUserToRemindAsync(User user, OperationalActivityCriteria criteria)
        {
            var operationalActivitiesForUser = await _db.OperationalActivities
                .Include(oa => oa.Vehicle)
                .Where(oa => oa.Vehicle.User.Id == user.Id)
                .ToListAsync();

            operationalActivitiesForUser = operationalActivitiesForUser
                .Where(oa => oa.IsDateOperated ? oa.ToNextAct(criteria.ReferenceDate) <= criteria.DaysToRemind
                                               : oa.ToNextAct(criteria.ReferenceDate) <= criteria.MileageToRemind)
                .ToList();

            if (operationalActivitiesForUser.IsNullOrEmpty()) 
            {
                return null;
            }

            if (criteria.ShouldSetReminderDate)
            {
                operationalActivitiesForUser.ForEach(oa => oa.ReminderDate = criteria.ReferenceDate);
                _db.UpdateRange(operationalActivitiesForUser);
                await _db.SaveChangesAsync();
            }

            return OpertationalActivitySummary.BuildList(operationalActivitiesForUser, criteria.ReferenceDate);
        }
    }
}
