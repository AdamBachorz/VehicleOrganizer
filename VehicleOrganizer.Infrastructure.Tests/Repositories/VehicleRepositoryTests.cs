using VehicleOrganizer.Domain.Abstractions.Enums;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.Infrastructure.Tests.Repositories
{
    public class VehicleRepositoryTests : BaseDataBaseTests
    {
        private IVehicleRepository _sut;

        [SetUp]
        public void Setup()
        {
            base.Setup();
            
            _sut = new VehicleRepository(_db);
        }

        [TestCase(true, 3)]
        [TestCase(false, 2)]
        public async Task ShouldRetrunListOfVehilcesForUser_GetVehiclesForUser(bool includeSold, int expectedResultListCount)
        {
            var targetUser = _fixture.Create<User>();
            var userOther = _fixture.Create<User>();

            var vehicles = new List<Vehicle>()
            {
                new Vehicle { Id = 1, Name = _fixture.Create<string>(),
                    OilType = _fixture.Create<string>(),
                    VehicleType = _fixture.Create<VehicleType>(), 
                    User = targetUser,
                },

                new Vehicle { Id = 2, Name = _fixture.Create<string>(),
                    OilType = _fixture.Create<string>(), 
                    VehicleType = _fixture.Create<VehicleType>(), 
                    User = targetUser,
                },

                new Vehicle { Id = 3, Name = _fixture.Create<string>(),
                    OilType = _fixture.Create<string>(), 
                    VehicleType = _fixture.Create<VehicleType>(), 
                    User = targetUser,
                    SaleDate = _fixture.Create<DateTime>() 
                },

                new Vehicle { Id = 4, Name = _fixture.Create<string>(),
                    OilType = _fixture.Create<string>(), 
                    VehicleType = _fixture.Create<VehicleType>(), 
                    User = userOther, 
                },

                new Vehicle { Id = 5, Name = _fixture.Create<string>(),
                    OilType = _fixture.Create<string>(), 
                    VehicleType = _fixture.Create<VehicleType>(), 
                    User = userOther, 
                },

                new Vehicle { Id = 6, Name = _fixture.Create<string>(),
                    OilType = _fixture.Create<string>(), 
                    VehicleType = _fixture.Create<VehicleType>(), 
                    User = userOther,
                    SaleDate = _fixture.Create<DateTime>() 
                },
            };

            _db.Vehicles.AddRange(vehicles);
            _db.SaveChanges();

            var resultVehicles = await _sut.GetVehiclesForUser(targetUser, includeSold);

            Assert.Multiple(() =>
            {
                Assert.That(resultVehicles, Is.Not.Null.Or.Empty);
                Assert.That(resultVehicles, Has.Count.EqualTo(expectedResultListCount));
                Assert.That(resultVehicles.All(v => v.User == targetUser), Is.True);
                if (includeSold)
                {
                    Assert.That(resultVehicles.Any(v => v.IsSold), Is.True);
                }
                else
                {
                    Assert.That(resultVehicles.All(v => !v.IsSold), Is.True);
                }
            });
        }

        [Test]
        public async Task ShouldAddVehicle_AddVehicle()
        {
            var vehicle = new Vehicle
            {
                Name = _fixture.Create<string>(),
                OilType = _fixture.Create<string>(),
            };
            const int Mileage = 100;

            var resultVehicle = await _sut.AddVehicle(vehicle, Mileage);

            Assert.Multiple(() =>
            {
                Assert.That(resultVehicle, Is.Not.Null);
                Assert.That(resultVehicle.Id, Is.GreaterThan(0));
                Assert.That(resultVehicle.User.Id, Is.EqualTo(User.Default.Id));
                Assert.That(resultVehicle.MileageHistory, Is.Not.Null.Or.Empty);
                Assert.That(resultVehicle.MileageHistory, Has.Count.EqualTo(1));
                Assert.That(resultVehicle.LatestMileage, Is.EqualTo(Mileage));
                Assert.That(resultVehicle.MileageHistory.First().Id, Is.GreaterThan(0));
                Assert.That(resultVehicle.MileageHistory.First().AddDate, Is.EqualTo(resultVehicle.PurchaseDate));
            });
        }

        [Test]
        public async Task ShouldSellVehicle_SellVehicle()
        {
            var vehicle = new Vehicle
            {
                Name = _fixture.Create<string>(),
                OilType = _fixture.Create<string>(),
            };
            const int Mileage = 100;
            var resultVehicle = await _sut.AddVehicle(vehicle, Mileage);
            var saleDate = _fixture.Create<DateTime>();

            await _sut.SaleVehicle(resultVehicle, saleDate);
            
            Assert.Multiple(() =>
            {
                Assert.That(resultVehicle, Is.Not.Null);
                Assert.That(resultVehicle.Id, Is.GreaterThan(0));
                Assert.That(resultVehicle.IsSold, Is.True);
            });
        }
    }
}
