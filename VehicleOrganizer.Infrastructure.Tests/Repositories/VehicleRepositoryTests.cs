using VehicleOrganizer.Domain.Abstractions.Enums;
using VehicleOrganizer.Domain.Abstractions.Exceptions;
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
        public async Task ShouldReturnTrue_UserHasVehicle()
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
        
        [Test]
        public async Task ShouldReturnTrueIfUserHasVehicleWithGivenName_UserHasVehicleWithName()
        {
            var user = _fixture.Create<User>();
            var targetVehicleName = _fixture.Create<string>();

            var otherUserVehicle = new Vehicle
            {
                Name = _fixture.Create<string>(),
                OilType = _fixture.Create<string>(),
                User = user,
            };
            
            var targetUserVehicle = new Vehicle
            {
                Name = targetVehicleName,
                OilType = _fixture.Create<string>(),
                User = user,
            };

            var someOtherVehicle = new Vehicle
            {
                Name = _fixture.Create<string>(),
                OilType = _fixture.Create<string>(),
                User = _fixture.Create<User>(),
            };

            await _db.Vehicles.AddAsync(otherUserVehicle);
            await _db.Vehicles.AddAsync(targetUserVehicle);
            await _db.Vehicles.AddAsync(someOtherVehicle);
            await _db.SaveChangesAsync();

            Assert.Multiple(() =>
            {
                Assert.That(_sut.UserHasVehicleWithName(user, targetVehicleName), Is.True);
                Assert.That(_sut.UserHasVehicleWithName(user, _fixture.Create<string>()), Is.False);
            });
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
            var vehicle = _fixture.Create<Vehicle>();
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
            var vehicle = _fixture.Create<Vehicle>();
            vehicle.MileageHistory = new List<MileageHistory> 
            {
                new MileageHistory{ Vehicle = vehicle, Mileage = 100 }
            };

            await _db.Vehicles.AddAsync(vehicle);
            await _db.SaveChangesAsync();

            var newMileage = 1;

            Assert.That(() => _sut.UpdateMileageAsync(vehicle, newMileage), Throws.InstanceOf<CustomArgumentException>());
        }

        [Test]
        public async Task ShouldUpdateInsurance_UpdateInsuranceDate()
        {
            var newInsuranceConclusionDate = _fixture.Create<DateTime>();
            _fixture.Customize<Vehicle>(x => x.With(x => x.InsuranceTermination, newInsuranceConclusionDate.AddMonths(-1)));

            var vehicle = _fixture.Create<Vehicle>();

            await _db.Vehicles.AddAsync(vehicle);
            await _db.SaveChangesAsync();
           
            await _sut.UpdateInsuranceDateAsync(vehicle, newInsuranceConclusionDate);

            var updatedVehicle = _db.Vehicles.FirstOrDefault(x => x.Id == vehicle.Id);
            Assert.Multiple(() =>
            {
                Assert.That(updatedVehicle, Is.Not.Null);
                Assert.That(updatedVehicle.Id, Is.GreaterThan(0));
                Assert.That(updatedVehicle.InsuranceConclusion, Is.EqualTo(newInsuranceConclusionDate));
                Assert.That(updatedVehicle.InsuranceTermination, Is.EqualTo(newInsuranceConclusionDate.AddYears(1)));
            });

        }

        [Test]
        public async Task ShouldThrowException_NewInsuranceTerminationDateTooEarly_UpdateInsuranceDate()
        {
            var newInsuranceConclusionDate = _fixture.Create<DateTime>();
            _fixture.Customize<Vehicle>(x => x.With(x => x.InsuranceTermination, newInsuranceConclusionDate.AddYears(2)));

            var vehicle = _fixture.Create<Vehicle>();

            await _db.Vehicles.AddAsync(vehicle);
            await _db.SaveChangesAsync();
           
            Assert.That(() => _sut.UpdateInsuranceDateAsync(vehicle, newInsuranceConclusionDate), Throws.InstanceOf<CustomArgumentException>());
        }

        [Test]
        public async Task ShouldUpdateTechnicalReviewDate_UpdateTechnicalReviewDate()
        {
            var latestReviewDate = _fixture.Create<DateTime>();
            _fixture.Customize<Vehicle>(x => x.With(x => x.LastTechnicalReview, latestReviewDate.AddMonths(-1)));

            var vehicle = _fixture.Create<Vehicle>();

            await _db.Vehicles.AddAsync(vehicle);
            await _db.SaveChangesAsync();
           
            await _sut.UpdateTechnicalReviewDateAsync(vehicle, latestReviewDate);

            var updatedVehicle = _db.Vehicles.FirstOrDefault(x => x.Id == vehicle.Id);
            Assert.Multiple(() =>
            {
                Assert.That(updatedVehicle, Is.Not.Null);
                Assert.That(updatedVehicle.Id, Is.GreaterThan(0));
                Assert.That(updatedVehicle.LastTechnicalReview, Is.EqualTo(latestReviewDate));
                Assert.That(updatedVehicle.NextTechnicalReview, Is.EqualTo(latestReviewDate.AddYears(1)));
            });

        }

        [Test]
        public async Task ShouldThrowException_NewInsuranceTerminationDateTooEarly_UpdateTechnicalReviewDate()
        {
            var latestReviewDate = _fixture.Create<DateTime>();
            _fixture.Customize<Vehicle>(x => x.With(x => x.LastTechnicalReview, latestReviewDate.AddYears(2)));

            var vehicle = _fixture.Create<Vehicle>();

            await _db.Vehicles.AddAsync(vehicle);
            await _db.SaveChangesAsync();
           
            Assert.That(() => _sut.UpdateTechnicalReviewDateAsync(vehicle, latestReviewDate), Throws.InstanceOf<CustomArgumentException>());
        }

        [Test]
        public async Task ShouldSellVehicle_SellVehicle()
        {
            var vehicle = _fixture.Create<Vehicle>();
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
            var daysAppartValues = new[] { 60, 30, 10 };
            var currentId = 1;
            _fixture.Customize<Vehicle>(x => x.With(x => x.User, targetUser).Without(x => x.SaleDate));

            foreach (var daysAdded in daysAppartValues)
            {
                var vehicle = _fixture.Create<Vehicle>();
                vehicle.Id = currentId++;
                vehicle.InsuranceTermination = referenceDate.AddDays(daysAdded);
                await _db.Vehicles.AddAsync(vehicle);
            }
                        
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
