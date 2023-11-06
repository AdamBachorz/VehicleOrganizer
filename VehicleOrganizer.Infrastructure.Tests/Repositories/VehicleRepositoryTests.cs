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

        [Test]
        public async Task ShouldReturnTrueOrFalse_UserHasVehicle()
        {
            var user = _fixture.Create<User>();

            var vehicle = new Vehicle
            {
                Name = _fixture.Create<string>(),
                OilType = _fixture.Create<string>(),
                User = user,
            };

            await _db.Vehicles.AddAsync(vehicle);
            await _db.SaveChangesAsync();

            Assert.That(_sut.UserHasVehicle(user), Is.True);
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

            await _db.Vehicles.AddRangeAsync(vehicles);
            await _db.SaveChangesAsync();

            var resultVehicles = await _sut.GetVehiclesForUserAsync(targetUser, includeSold);

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
            var mileage = _fixture.Create<int>();

            var resultVehicle = await _sut.AddVehicleAsync(vehicle, mileage);

            Assert.Multiple(() =>
            {
                Assert.That(resultVehicle, Is.Not.Null);
                Assert.That(resultVehicle.Id, Is.GreaterThan(0));
                Assert.That(resultVehicle.User.Id, Is.EqualTo(User.Default.Id));
                Assert.That(resultVehicle.MileageHistory, Is.Not.Null.Or.Empty);
                Assert.That(resultVehicle.MileageHistory, Has.Count.EqualTo(1));
                Assert.That(resultVehicle.LatestMileage, Is.EqualTo(mileage));
                Assert.That(resultVehicle.MileageHistory.First().Id, Is.GreaterThan(0));
                Assert.That(resultVehicle.MileageHistory.First().AddDate, Is.EqualTo(resultVehicle.PurchaseDate));
            });
        }

        [Test]
        public async Task ShouldUpdateMileage_UpdateMileage()
        {
            var user = _fixture.Create<User>();

            var vehicle = new Vehicle
            {
                Name = _fixture.Create<string>(),
                OilType = _fixture.Create<string>(),
                User = user
            };
            vehicle.MileageHistory = new List<MileageHistory> 
            {
                new MileageHistory{ Vehicle = vehicle, Mileage = 100 }
            };

            await _db.Vehicles.AddAsync(vehicle);
            await _db.SaveChangesAsync();

            var newMileage = 1000;
            await _sut.UpdateMileageAsync(vehicle, newMileage);

            var updatedVehicle = _db.Vehicles.FirstOrDefault(x => x.Id == vehicle.Id);
            Assert.Multiple(() =>
            {
                Assert.That(updatedVehicle, Is.Not.Null);
                Assert.That(updatedVehicle.Id, Is.GreaterThan(0));
                Assert.That(updatedVehicle.LatestMileage, Is.EqualTo(newMileage));
                Assert.That(updatedVehicle.MileageHistory, Has.Count.EqualTo(2));
            });

        }

        [Test]
        public async Task ShouldThrowException_SmallerMileage_UpdateMileage()
        {
            var user = _fixture.Create<User>();

            var vehicle = new Vehicle
            {
                Name = _fixture.Create<string>(),
                OilType = _fixture.Create<string>(),
                User = user
            };
            vehicle.MileageHistory = new List<MileageHistory> 
            {
                new MileageHistory{ Vehicle = vehicle, Mileage = 100 }
            };

            await _db.Vehicles.AddAsync(vehicle);
            await _db.SaveChangesAsync();

            var newMileage = 1;

            Assert.That(() => _sut.UpdateMileageAsync(vehicle, newMileage), Throws.ArgumentException);
        }

        [Test]
        public async Task ShouldSellVehicle_SellVehicle()
        {
            var vehicle = new Vehicle
            {
                Name = _fixture.Create<string>(),
                OilType = _fixture.Create<string>(),
            };
            var mileage = _fixture.Create<int>();
            var resultVehicle = await _sut.AddVehicleAsync(vehicle, mileage);
            var saleDate = _fixture.Create<DateTime>();

            await _sut.SaleVehicleAsync(resultVehicle, saleDate);
            
            Assert.Multiple(() =>
            {
                Assert.That(resultVehicle, Is.Not.Null);
                Assert.That(resultVehicle.Id, Is.GreaterThan(0));
                Assert.That(resultVehicle.IsSold, Is.True);
            });
        }
        
        [Test]
        public void ShouldThrowArgumentNullException_SellVehicle()
        {
            var notExistingVehicleInDb = new Vehicle();
            Assert.That(async () => await _sut.SaleVehicleAsync(notExistingVehicleInDb, _fixture.Create<DateTime>()), Throws.ArgumentNullException);
        }

        [Test]
        public async Task ShouldReturnVehiclesWithCloseInsuranceTermination_GetVehiclesWithCloseInsuranceTermination()
        {
            var targetUser = _fixture.Create<User>();
            var referenceDate = new DateTime(2024, 5, 5);
            var vehicles = new List<Vehicle>()
            {
                new Vehicle { Id = 1, Name = _fixture.Create<string>(),
                    OilType = _fixture.Create<string>(),
                    VehicleType = _fixture.Create<VehicleType>(),
                    User = targetUser,
                    InsuranceTermination = referenceDate.AddDays(60),
                },

                new Vehicle { Id = 2, Name = _fixture.Create<string>(),
                    OilType = _fixture.Create<string>(),
                    VehicleType = _fixture.Create<VehicleType>(),
                    User = targetUser,
                    InsuranceTermination = referenceDate.AddDays(30),
                },

                new Vehicle { Id = 3, Name = _fixture.Create<string>(),
                    OilType = _fixture.Create<string>(),
                    VehicleType = _fixture.Create<VehicleType>(),
                    User = targetUser,
                    InsuranceTermination = referenceDate.AddDays(10),
                },
            };
            
            await _db.Vehicles.AddRangeAsync(vehicles);
            await _db.SaveChangesAsync();

            var resultVehicles = await _sut.GetVehiclesWithCloseInsuranceTermination(targetUser, referenceDate);

            Assert.Multiple(() =>
            {
                Assert.That(resultVehicles, Is.Not.Null.Or.Empty);
                Assert.That(resultVehicles, Has.Count.EqualTo(2));
                Assert.That(resultVehicles.Any(v => v.Id == 2), Is.True);
                Assert.That(resultVehicles.Any(v => v.Id == 3), Is.True);
            });
        }

        [Test]
        public async Task ShouldReturnVehiclesWithCloseNextReviewDate_GetVehiclesWithCloseNextReviewDate()
        {
            var targetUser = _fixture.Create<User>();
            var referenceDate = new DateTime(2024, 5, 5);
            var vehicles = new List<Vehicle>()
            {
                new Vehicle { Id = 1, Name = _fixture.Create<string>(),
                    OilType = _fixture.Create<string>(),
                    VehicleType = _fixture.Create<VehicleType>(),
                    User = targetUser,
                    NextTechnicalReview = referenceDate.AddDays(60),
                },

                new Vehicle { Id = 2, Name = _fixture.Create<string>(),
                    OilType = _fixture.Create<string>(),
                    VehicleType = _fixture.Create<VehicleType>(),
                    User = targetUser,
                    NextTechnicalReview = referenceDate.AddDays(30),
                },

                new Vehicle { Id = 3, Name = _fixture.Create<string>(),
                    OilType = _fixture.Create<string>(),
                    VehicleType = _fixture.Create<VehicleType>(),
                    User = targetUser,
                    NextTechnicalReview = referenceDate.AddDays(10),
                },
            };

            await _db.Vehicles.AddRangeAsync(vehicles);
            await _db.SaveChangesAsync();

            var resultVehicles = await _sut.GetVehiclesWithCloseNextReviewDate(targetUser, referenceDate);

            Assert.Multiple(() =>
            {
                Assert.That(resultVehicles, Is.Not.Null.Or.Empty);
                Assert.That(resultVehicles, Has.Count.EqualTo(2));
                Assert.That(resultVehicles.Any(v => v.Id == 2), Is.True);
                Assert.That(resultVehicles.Any(v => v.Id == 3), Is.True);
            });
        }
    }
}
