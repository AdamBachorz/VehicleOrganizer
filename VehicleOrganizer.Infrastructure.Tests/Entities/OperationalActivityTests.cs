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
            };
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
