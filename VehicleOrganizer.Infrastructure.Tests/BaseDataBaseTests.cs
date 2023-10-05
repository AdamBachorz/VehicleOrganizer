namespace VehicleOrganizer.Infrastructure.Tests
{
    public class BaseDataBaseTests : BaseTests
    {
        protected DataBaseContext _db;

        public void Setup()
        {
            base.Setup();
            _fixture.AddTestDatabaseContext();
            _db = _fixture.GetTestContext();
        }
    }
}
