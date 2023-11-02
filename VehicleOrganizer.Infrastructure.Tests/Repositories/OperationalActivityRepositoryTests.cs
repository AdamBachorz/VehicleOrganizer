using Microsoft.EntityFrameworkCore;
using VehicleOrganizer.Domain.Abstractions;
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

            var operationalActivities = new List<OperationalActivity>
            {
                DummyActivityDateBased(vehicle1, new DateTime(2023, 1, 6)),
                DummyActivityDateBased(vehicle1, new DateTime(2023, 5, 1)),
                DummyActivityDateBased(vehicle1, new DateTime(2023, 12, 1)),

                DummyActivityDateBased(vehicle2, new DateTime(2023, 1, 8)),
                DummyActivityDateBased(vehicle2, new DateTime(2023, 6, 1)),

                DummyActivityDateBased(vehicle3, new DateTime(2023, 12, 31)),
                DummyActivityDateBased(vehicle4, new DateTime(2023, 12, 31)),
            };

            await _db.OperationalActivities.AddRangeAsync(operationalActivities);
            await _db.SaveChangesAsync();

            var criteria = new OperationalActivityCriteria
            {
                ReferenceDate = referenceDate,
                ShouldSetReminderDate = shouldSetReminderDate,
            };

            var summaries = await _sut.GetOpertationalActivitiesForUserToRemindAsync(user, criteria);

            Assert.Multiple(() =>
            {
                Assert.That(summaries, Is.Not.Null.Or.Empty);
                Assert.That(summaries, Has.Count.EqualTo(2));

                Assert.That(summaries[0].VehicleName, Is.EqualTo(vehicle1.Name));
                Assert.That(summaries[0].ActivitySummaries, Has.Count.EqualTo(1));

                Assert.That(summaries[1].VehicleName, Is.EqualTo(vehicle2.Name));
                Assert.That(summaries[1].ActivitySummaries, Has.Count.EqualTo(1));

                if (shouldSetReminderDate)
                {
                    foreach (var activity in operationalActivities.Where(oa => oa.ReminderDate.HasValue))
                    {
                        Assert.That(activity.ReminderDate, Is.EqualTo(referenceDate)); 
                    }
                }
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
    }
}
