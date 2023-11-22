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

        public async Task<OperationalActivity> AddOperationalActivityForVehicleAsync(int vehicleId, OperationalActivity operationalActivity)
        {
            var vehicle = await _db.Vehicles.FindAsync(vehicleId);

            if (vehicle is null)
            {
                throw new ArgumentNullException(nameof(vehicleId), "Vehicle with given ID does not exists");
            }

            operationalActivity.Vehicle = vehicle;
            await _db.OperationalActivities.AddAsync(operationalActivity);
            await _db.SaveChangesAsync();

            return operationalActivity;
        }

        public async Task<IList<OperationalActivity>> GetOperationalActivitiesForVehicleAndUserAsync(int vehicleId, User user)
        {
            return await _db.OperationalActivities.Where(oa => oa.Vehicle.Id == vehicleId && oa.Vehicle.User.Id.Equals(user.Id)).ToListAsync();
        }

        public async Task<IList<OperationalActivitySummary>> GetOperationalActivitiesForUserToRemindAsync(User user, OperationalActivityCriteria criteria)
        {
            var operationalActivitiesForUser = await _db.OperationalActivities
                .Include(oa => oa.Vehicle)
                .Where(oa => oa.Vehicle.User.Id.Equals(user.Id))
                .ToListAsync();

            operationalActivitiesForUser = operationalActivitiesForUser
                .Where(oa => oa.DaysAfterLastReminder(criteria.ReferenceDate) > criteria.DaysAboveWhichAnotherReminderCanBeSent)
                .ToList();

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

            return OperationalActivitySummary.BuildList(operationalActivitiesForUser, criteria.ReferenceDate);
        }
    }
}
