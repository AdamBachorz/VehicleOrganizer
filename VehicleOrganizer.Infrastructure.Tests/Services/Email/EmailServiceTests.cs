using BachorzLibrary.Common.Extensions;
using BachorzLibrary.Common.Tools.Email;
using BachorzLibrary.Common.Tools.Html;
using System.Text;
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

            _fixture.Customize<User>(x => x.With(x => x.Email, Codes.AdminEmail));

            _sut = new EmailService(new EmailSender(settings), _fixture.Create<HtmlHelper>(), 
                new OperationalActivityRepository(_db), new VehicleRepository(_db));
        }

        [Test]
        [Explicit]
        public async Task ShouldSendReminderEmail_RemindUserAboutActivitiesAsync()
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

                DummyActivityMileageBased(vehicle5, 10000, 1000),
            };

            await _db.OperationalActivities.AddRangeAsync(operationalActivities);
            await _db.SaveChangesAsync();

            var criteria = new OperationalActivityCriteria()
            {
                ReferenceDate = referenceDate,
                MileageToRemind = 50
            };

            await _sut.RemindUserAboutActivitiesAsync(user, criteria);

            Assert.Pass();
        }

        [Test]
        [Explicit]
        public async Task ShouldSendReminderEmail_RemindUserAboutVehicleInsuranceOrTechnicalReviewAsync()
        {
            var user = _fixture.Create<User>();
            var referenceDate = new DateTime(2024, 1, 1);
            var vehicle1 = DummyVehicle(user);
            var vehicle2 = DummyVehicle(user);
            vehicle2.NextTechnicalReview = referenceDate.AddDays(10);

            var vehicle3 = DummyVehicle(user);
            vehicle3.InsuranceTermination = referenceDate.AddDays(10);

            var vehicle4 = DummyVehicle(user);
            vehicle4.InsuranceTermination = referenceDate.AddDays(-10);

            var vehicles = new List<Vehicle>
            {
                vehicle1, vehicle2, vehicle3, vehicle4
            };

            await _db.Vehicles.AddRangeAsync(vehicles);
            await _db.SaveChangesAsync();

            await _sut.RemindUserAboutVehicleInsuranceOrTechnicalReviewAsync(user, referenceDate);

            Assert.Pass();
        }

        [Test]
        [Explicit]
        public async Task ShouldInformAdmin_InformAdminAboutProblemAsync()
        {
            var errors = new List<string>();
            StringBuilder sb = null;
            try
            {
                var a = 1;
                var b = 0;
                var r = a/b;
            } 
            catch (Exception ex) 
            {
                sb = new StringBuilder();
                sb.AppendLine(ex.FullMessageWithStackTrace());
                errors.Add(sb.ToString());
            }
            
            try
            {
                var arr = new int[1];
                var v = arr[3];
            } 
            catch (Exception ex) 
            {
                sb = new StringBuilder();
                sb.AppendLine(ex.FullMessageWithStackTrace());
                errors.Add(sb.ToString());
            }

            await _sut.InformAdminAboutProblemAsync(errors);

            Assert.Pass();
        }
        
        [Test]
        [Explicit]
        public async Task ShouldNotInformAdminWhenNoErrors_InformAdminAboutProblemAsync()
        {
            await _sut.InformAdminAboutProblemAsync(new List<string>());
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
