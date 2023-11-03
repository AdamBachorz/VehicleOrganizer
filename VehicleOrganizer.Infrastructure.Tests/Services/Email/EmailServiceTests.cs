using BachorzLibrary.Common.Tools.Email;
using BachorzLibrary.Common.Tools.Html;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Infrastructure.Criteria;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories;
using VehicleOrganizer.Infrastructure.Services.Email;

namespace VehicleOrganizer.Infrastructure.Tests.Services.Email
{
    public class EmailServiceTests : BaseDataBaseTests
    {
        private IEmailService _sut;

        [SetUp]
        public void Setup()
        {
            base.Setup();

            var settings = new EmailSenderSettings
            {
                SenderValues = _customConfig.ValuesBag["Sender"] as string,
                SenderHeader = Codes.AppName,
            };

            _sut = new EmailService(new EmailSender(settings), _fixture.Create<HtmlHelper>(), new OperationalActivityRepository(_db), null);
        }

        [Test]
        [Explicit]
        public async Task ShouldSendReminderEmail_RemindUserAboutActivitiesAsync()
        {
            var user = _fixture.Create<User>();
            user.Email = Codes.AdminEmail;
            var referenceDate = new DateTime(2024, 1, 1);
            var vehicle1 = DummyVehicle(user);
            var vehicle2 = DummyVehicle(user);

            var vehicle3 = DummyVehicle(user);
            vehicle3.InsuranceTermination = referenceDate.AddDays(10);

            var vehicle4 = DummyVehicle(user);
            vehicle4.InsuranceTermination = referenceDate.AddDays(-10);

            //TODO Extend with other options like mileage and add Insurance reference
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

            var criteria = new OperationalActivityCriteria()
            {
                ReferenceDate = referenceDate
            };

            await _sut.RemindUserAboutActivitiesAsync(user, criteria);

            Assert.Pass();
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
