using AutoMapper;
using System.Xml;
using VehicleOrganizer.Core.Config;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Domain.Abstractions.Enums;
using VehicleOrganizer.Domain.Abstractions.Extensions;
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

        [TestCase(VehicleType.Car, "5W-40", "5W-40")]
        [TestCase(VehicleType.Truck, "5W-40", "5W-40")]
        [TestCase(VehicleType.Bus, "5W-40", "5W-40")]
        [TestCase(VehicleType.Motorcycle, "something", Codes.None)]
        [TestCase(VehicleType.Trailer, "something", Codes.None)]
        public void ShouldMap_Vehicle(VehicleType vehicleType, string oilType, string expectedOilType)
        {
            var source = new Vehicle 
            {
                Name = _fixture.Create<string>(),
                VehicleType = vehicleType,
                OilType = oilType,
            };
            source.MileageHistory = new List<MileageHistory> 
            { 
                new MileageHistory 
                { 
                    Vehicle = source, 
                    Mileage = _fixture.Create<int>() 
                } 
            };

            var result = _mapper.Map<VehicleView>(source);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Name, Is.EqualTo(source.Name));
                Assert.That(result.OilType, Is.EqualTo(expectedOilType));
                Assert.That(result.IsOilBased, Is.EqualTo(source.VehicleType.IsOilBased()));
                Assert.That(result.DaysToInsuranceExpires, Does.EndWith("dni"));
                Assert.That(result.DaysToNextTechnicalReview, Does.EndWith("dni"));
                Assert.That(result.LatestMileage, Does.EndWith("km"));
            });
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ShouldMap_OperationalActivity(bool isDateOperated)
        {
            var source = _fixture.Create<OperationalActivity>();
            source.IsDateOperated = isDateOperated;

            var result = _mapper.Map<OperationalActivityView>(source);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Reference, Is.EqualTo(source.Id));
                Assert.That(result.Name, Is.EqualTo(source.Name));
                Assert.That(result.LastOperationDateOrMileageWhenPerformed, Is.EqualTo(isDateOperated ? source.LastOperationDate.ToShortDateString() : source.MileageWhenPerformed.ToString()));
                Assert.That(result.SummaryPrompt, Does.EndWith(isDateOperated ? "dni" : "kilometrów"));
            });
        }
    }
}