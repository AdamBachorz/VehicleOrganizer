using BachorzLibrary.Common.Tools.Html;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Services.Email;

namespace VehicleOrganizer.Infrastructure.Tests.Services.Email
{
    public class EmailServiceTests : BaseDataBaseTests
    {
        private IEmailService _sut;
        private EmailSender _emailSenderService;
        private IOperationalActivityRepository _operationalActivityRepository;

        [SetUp]
        public void Setup()
        {
            base.Setup();

            _operationalActivityRepository = new OperationalActivityRepository(_db);
            var settings = new EmailSenderSettings
            {
                SmtpClientUrl = "smtp.poczta.onet.pl",
                SenderValues = _customConfig.ValuesBag["Sender"] as string,
                SenderEmail = "adar_1@op.pl",
                SenderHeader = Codes.AppName,
            };
            _emailSenderService = new EmailSender(settings);
            _sut = new EmailService(_emailSenderService, new HtmlHelper(), _operationalActivityRepository, null);
        }

        [Test]
        [Explicit]
        public async Task ShouldSedndREminderEmail_RemindUserAboutActivitiesAsync()
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

            foreach (var item in operationalActivities)
            {
                _operationalActivityRepository.Insert(item);
            }

            await _sut.RemindUserAboutActivitiesAsync(user);

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
