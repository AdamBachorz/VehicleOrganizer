using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using Newtonsoft.Json;
using VehicleOrganizer.Domain.Abstractions;

namespace VehicleOrganizer.Infrastructure.Tests
{
    public abstract class BaseTests
    {
        protected IFixture _fixture;
        protected IEFCCustomConfig _customConfig;

        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization()
            {
                ConfigureMembers = true
            });
            _customConfig = JsonConvert.DeserializeObject<EFCCustomConfig>(File.ReadAllText(Codes.Files.DevConfig));
        }
    }
}