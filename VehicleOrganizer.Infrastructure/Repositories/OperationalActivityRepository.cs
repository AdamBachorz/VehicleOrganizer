using BachorzLibrary.Common.Extensions;
using BachorzLibrary.DAL.DotNetSix.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IList<OpertationalActivitySummary>> GetOpertationalActivitiesForUserToRemindAsync(User user, 
            (int DateDays, int Milage) referenceThreshold, DateTime referenceDate, bool shouldSetReminderDate)
        {
            var operationalActivitiesForUser = await _db.OperationalActivities
                .Include(oa => oa.Vehicle)
                .Where(oa => oa.Vehicle.User.Id == user.Id)
                .ToListAsync();

            operationalActivitiesForUser = operationalActivitiesForUser
                .Where(oa => oa.IsDateOperated ? oa.ToNextAct(referenceDate) <= referenceThreshold.DateDays 
                                               : oa.ToNextAct(referenceDate) <= referenceThreshold.Milage)
                .ToList();

            if (operationalActivitiesForUser.IsNullOrEmpty()) 
            {
                return null;
            }

            if (shouldSetReminderDate)
            {
                operationalActivitiesForUser.ForEach(oa => oa.ReminderDate = referenceDate);
                _db.UpdateRange(operationalActivitiesForUser);
                await _db.SaveChangesAsync();
            }

            return OpertationalActivitySummary.BuildList(operationalActivitiesForUser, referenceDate);
        }
    }
}
