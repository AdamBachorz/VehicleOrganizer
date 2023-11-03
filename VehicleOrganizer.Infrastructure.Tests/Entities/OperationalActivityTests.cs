using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Tests.Entities
{
    public class OperationalActivityTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
            base.Setup();
        }

        [Test]
        public void ShouldReturnNextOperationDate_NextOperationDate()
        {
            var operationalActivity = new OperationalActivity
            {
                Name = _fixture.Create<string>(),
                Vehicle = DummyVehicle(),
                LastOperationDate = new DateTime(2023, 5, 5),
                YearsStep = 2,
            };

            var expectedDate = new DateTime(2025, 5, 5);

            Assert.That(operationalActivity.NextOperationDate, Is.EqualTo(expectedDate));
        }

        [Test]
        public void ShouldReturnNextOperationAtMilage_NextOperationAtMilage()
        {
            var operationalActivity = new OperationalActivity
            {
                Name = _fixture.Create<string>(),
                Vehicle = DummyVehicle(),
                MileageWhenPerformed = 100000,
                MileageStep = 1000,
            };

            var expectedValue = 101000;

            Assert.That(operationalActivity.NextOperationAtMilage, Is.EqualTo(expectedValue));
        }
        
        [TestCase(false)]
        [TestCase(true)]
        public void ShouldReturnValueToNextActAndSummaryPromptDependingOnDateOperatedFlag_ToNextAct_SummaryPrompt(bool isDateOperated)
        {
            var vehcicle = DummyVehicle();
            vehcicle.MileageHistory = new List<MileageHistory>
            {
                new MileageHistory 
                {
                    Mileage = 100990, 
                    Vehicle = vehcicle
                }
            };
            var operationalActivity = new OperationalActivity
            {
                Name = _fixture.Create<string>(),
                Vehicle = vehcicle,
                IsDateOperated = isDateOperated,
                LastOperationDate = new DateTime(2023, 5, 5),
                YearsStep = 2,
                MileageWhenPerformed = 100000,
                MileageStep = 1000,
            };

            const int ExpectedMileageValue = 10;
            const int ExpectedDays = 4;
            var referenceDate = new DateTime(2025, 5, 1);

            Assert.Multiple(() =>
            {
                Assert.That(operationalActivity.ToNextAct(referenceDate), Is.EqualTo(isDateOperated ? ExpectedDays : ExpectedMileageValue));
                string prompt = operationalActivity.SummaryPrompt(referenceDate);
                Assert.That(prompt, Is.Not.Null.Or.Empty);
                Assert.That(prompt, Does.Contain(operationalActivity.Name));
                Assert.That(prompt, Does.EndWith(isDateOperated ? "dni" : "kilometrów")); 
            });
        }

        private Vehicle DummyVehicle()
        {
            return new Vehicle
            {
                Name = _fixture.Create<string>(),
                OilType = _fixture.Create<string>(),
                User = _fixture.Create<User>(),
            };
        }
    }
}
