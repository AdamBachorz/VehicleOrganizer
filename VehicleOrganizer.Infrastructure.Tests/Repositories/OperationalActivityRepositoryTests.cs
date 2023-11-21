using Microsoft.EntityFrameworkCore;
using VehicleOrganizer.Infrastructure.Criteria;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.Infrastructure.Tests.Repositories
{
    public class OperationalActivityRepositoryTests : BaseDataBaseTests
    {
        private IOperationalActivityRepository _sut;

        [SetUp]
        public void Setup()
        {
            base.Setup();

            _sut = new OperationalActivityRepository(_db);
        }

        [Test]
        public async Task ShouldReturnActivitiesForVehicleAndUser_GetOpertationalActivitiesForVehicleAndUserAsync()
        {
            var user = _fixture.Create<User>();
            var otherUser = _fixture.Create<User>();
            var vehicle = DummyVehicle(user);
            var otherVehicle = DummyVehicle(otherUser);

            var operationalActivities = new List<OperationalActivity>
            {
                DummyActivityDateBased(vehicle, _fixture.Create<DateTime>()),
                DummyActivityDateBased(vehicle, _fixture.Create<DateTime>()),
                DummyActivityDateBased(vehicle, _fixture.Create<DateTime>()),

                DummyActivityDateBased(otherVehicle, _fixture.Create<DateTime>()),
                DummyActivityDateBased(otherVehicle, _fixture.Create<DateTime>()),
            };

            await _db.OperationalActivities.AddRangeAsync(operationalActivities);
            await _db.SaveChangesAsync();

            var result = await _sut.GetOperationalActivitiesForVehicleAndUserAsync(vehicle.Id, user);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null.Or.Empty);
                Assert.That(result, Has.Count.EqualTo(3));

                Assert.That(result.All(oa => oa.Vehicle.Id == vehicle.Id && oa.Vehicle.User.Id.Equals(user.Id)), Is.True);
            });
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task ShouldReturnActivitySummariesBasedOnDataBaseInfo_GetOpertationalActivitiesToRemindAsync(bool shouldSetReminderDate) 
        {
            var user = _fixture.Create<User>();
            var referenceDate = new DateTime(2024, 1, 1);
            var vehicle1 = DummyVehicle(user);
            var vehicle2 = DummyVehicle(user);

            var vehicle3 = DummyVehicle(user);
            vehicle3.InsuranceTermination = referenceDate.AddDays(10);

            var vehicle4 = DummyVehicle(user);
            vehicle4.InsuranceTermination = referenceDate.AddDays(-10);

            var vehicle5 = DummyVehicle(user);
            vehicle5.MileageHistory = new List<MileageHistory> { new MileageHistory { Vehicle = vehicle5, Mileage = 10500 } };

            var operationalActivities = new List<OperationalActivity>
            {
                DummyActivityDateBased(vehicle1, new DateTime(2023, 1, 6)),
                DummyActivityDateBased(vehicle1, new DateTime(2023, 5, 1)),
                DummyActivityDateBased(vehicle1, new DateTime(2023, 12, 1)),

                DummyActivityDateBased(vehicle2, new DateTime(2023, 1, 8)),
                DummyActivityDateBased(vehicle2, new DateTime(2023, 6, 1)),

                DummyActivityDateBased(vehicle3, new DateTime(2023, 12, 31)),
                DummyActivityDateBased(vehicle4, new DateTime(2023, 12, 31)),

                DummyActivityMileageBased(vehicle5, 10000, 1000),
            };

            await _db.OperationalActivities.AddRangeAsync(operationalActivities);
            await _db.SaveChangesAsync();

            var criteria = new OperationalActivityCriteria
            {
                ReferenceDate = referenceDate,
                ShouldSetReminderDate = shouldSetReminderDate,
            };

            var summaries = await _sut.GetOperationalActivitiesForUserToRemindAsync(user, criteria);

            Assert.Multiple(() =>
            {
                Assert.That(summaries, Is.Not.Null.Or.Empty);
                Assert.That(summaries, Has.Count.EqualTo(3));

                Assert.That(summaries[0].VehicleName, Is.EqualTo(vehicle1.Name));
                Assert.That(summaries[0].ActivitySummaries, Has.Count.EqualTo(1));

                Assert.That(summaries[1].VehicleName, Is.EqualTo(vehicle2.Name));
                Assert.That(summaries[1].ActivitySummaries, Has.Count.EqualTo(1));

                Assert.That(summaries[2].VehicleName, Is.EqualTo(vehicle5.Name));
                Assert.That(summaries[2].ActivitySummaries, Has.Count.EqualTo(1));

                if (shouldSetReminderDate)
                {
                    foreach (var activity in operationalActivities.Where(oa => oa.ReminderDate.HasValue))
                    {
                        Assert.That(activity.ReminderDate, Is.EqualTo(referenceDate));
                    }
                }
            });
        }

        [Test]
        public async Task ShouldNotReturnActivitySummariesDueToReminderDate_GetOpertationalActivitiesToRemindAsync()
        {
            var user = _fixture.Create<User>();
            var referenceDate = new DateTime(2024, 5, 5);
            var reminderDate = new DateTime(2024, 5, 1);

            var vehicle = DummyVehicle(user);
            vehicle.MileageHistory = new List<MileageHistory> { new MileageHistory { Vehicle = vehicle, Mileage = 10500 } };

            var activityWithRemindDate = DummyActivityMileageBased(vehicle, 10000, 1000);
            activityWithRemindDate.ReminderDate = reminderDate;

            var operationalActivities = new List<OperationalActivity>
            {
                activityWithRemindDate
            };

            await _db.OperationalActivities.AddRangeAsync(operationalActivities);
            await _db.SaveChangesAsync();

            var criteria = new OperationalActivityCriteria
            {
                ReferenceDate = referenceDate,
                ShouldSetReminderDate = true,
            };

            var summaries = await _sut.GetOperationalActivitiesForUserToRemindAsync(user, criteria);
            var activitiesFromDb = await _db.OperationalActivities.ToListAsync();

            Assert.Multiple(() =>
            {
                Assert.That(summaries, Is.Null.Or.Empty);
                Assert.That(activitiesFromDb.First().ReminderDate, Is.EqualTo(reminderDate));
            });
        }
        

        private Vehicle DummyVehicle(User user)
        {
            return new Vehicle
            {
                Name = _fixture.Create<string>(),
                OilType = _fixture.Create<string>(),
                User = user,
            };
        }

        private OperationalActivity DummyActivityDateBased(Vehicle vehicle, DateTime lastOperationDate)
        {
            return new OperationalActivity
            {
                Name = _fixture.Create<string>(),
                Vehicle = vehicle,
                IsDateOperated = true,
                LastOperationDate = lastOperationDate,
                YearsStep = 1,
            };
        }

        private OperationalActivity DummyActivityMileageBased(Vehicle vehicle, int mileageWhenPerformed, int mileageStep)
        {
            return new OperationalActivity
            {
                Name = _fixture.Create<string>(),
                Vehicle = vehicle,
                IsDateOperated = false,
                MileageWhenPerformed = mileageWhenPerformed,
                MileageStep = mileageStep,
            };
        }
    }
}
