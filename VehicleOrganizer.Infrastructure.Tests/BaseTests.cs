namespace VehicleOrganizer.Infrastructure.Tests
{
    public abstract class BaseTests
    {
        protected IFixture _fixture;

        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization()
            {
                ConfigureMembers = true
            });
        }
    }
}