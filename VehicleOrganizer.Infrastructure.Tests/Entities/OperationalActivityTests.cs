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
                Name = "Testowa aktywność",
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
                Name = "Testowa aktywność",
                Vehicle = DummyVehicle(),
                MileageWhenPerformed = 100000,
                MileageStep = 1000,
            };

            var expectedValue = 101000;

            Assert.That(operationalActivity.NextOperationAtMilage, Is.EqualTo(expectedValue));
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
