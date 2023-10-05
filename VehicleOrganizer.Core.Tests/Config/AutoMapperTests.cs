using AutoMapper;
using VehicleOrganizer.Core.Config;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Core.Tests.Config
{
    public class AutoMapperTests : BaseTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            base.Setup();

            _mapper = new Mapper(new MapperConfiguration(AutoMapperFixture.Create()));
        }

        [Test]
        public void ShoudMap_Vehicle()
        {
            var source = new Vehicle 
            {
                Name = _fixture.Create<string>(),
            };
            source.MileageHistory = new List<MileageHistory> 
            { 
                new MileageHistory { Vehicle = source, Mileage = _fixture.Create<int>() } 
            };

            var result = _mapper.Map<VehicleView>(source);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Name, Is.EqualTo(source.Name));
                Assert.That(result.DaysToInsuranceExpires, Does.EndWith("dni"));
                Assert.That(result.LatestMileage, Does.EndWith("km"));
            });
        }
    }
}