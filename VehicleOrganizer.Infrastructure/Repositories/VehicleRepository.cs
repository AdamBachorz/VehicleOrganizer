﻿using BachorzLibrary.DAL.DotNetSix.Repositories;
using Microsoft.EntityFrameworkCore;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Domain.Abstractions.Exceptions;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.Infrastructure.Repositories
{
    public class VehicleRepository : EFCBaseRepository<Vehicle, DataBaseContext, int>, IVehicleRepository
    {
        public VehicleRepository(DataBaseContext db) : base(db)
        {
        }

        public bool UserHasVehicle(User user)
        {
            return _db.Vehicles.Any(v => v.User.Id == user.Id);   
        }

        public bool UserHasVehicleWithName(User user, string vehicleName)
        {
            return _db.Vehicles.Any(v => v.User.Id == user.Id && v.Name.Equals(vehicleName));   
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehcle, int mileage)
        {
            vehcle.MileageHistory = new List<MileageHistory>()
            {
                new MileageHistory
                {
                    Vehicle = vehcle,
                    Mileage = mileage 
                }
            };
            
            await _db.Vehicles.AddAsync(vehcle);
            await _db.SaveChangesAsync();

            return vehcle;
        }

        public async Task<IList<Vehicle>> GetVehiclesForUserAsync(User user, bool includeSold = false)
        {
            var query = _db.Vehicles.Include(v => v.MileageHistory).Where(v => v.User.Id == user.Id);

            if (!includeSold)
            {
                query = query.Where(v => !v.SaleDate.HasValue);
            }
            
            return await query.OrderBy(v => v.PurchaseDate).ToListAsync();
        }

        public async Task UpdateMileageAsync(Vehicle vehicle, int mileage)
        {
            vehicle = await _db.Vehicles.FindAsync(vehicle.Id);

            if (vehicle == null)
            {
                throw new ArgumentNullException("Vehicle not found");
            }

            if (vehicle.LatestMileage > mileage)
            {
                throw new CustomArgumentException("Given mileage is smaller than current mileage");
            }

            var mileageHistory = new MileageHistory
            {
                Vehicle = vehicle,
                AddDate = DateTime.Now.Date,
                Mileage = mileage
            };

            vehicle.MileageHistory.Add(mileageHistory);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateInsuranceDateAsync(Vehicle vehicle, DateTime newInsuranceConclusionDate)
        {
            vehicle = await _db.Vehicles.FindAsync(vehicle.Id);

            if (vehicle == null)
            {
                throw new ArgumentNullException("Vehicle not found");
            }

            if (vehicle.InsuranceTermination > newInsuranceConclusionDate)
            {
                throw new CustomArgumentException("Date of new insurance cannot be earlier than date of the previous insurance termination");
            }

            vehicle.InsuranceConclusion = newInsuranceConclusionDate;
            vehicle.InsuranceTermination = newInsuranceConclusionDate.AddYears(1);

            await _db.SaveChangesAsync();
        }

        public async Task UpdateTechnicalReviewDateAsync(Vehicle vehicle, DateTime latestReviewDate)
        {
            vehicle = await _db.Vehicles.FindAsync(vehicle.Id);

            if (vehicle == null)
            {
                throw new ArgumentNullException("Vehicle not found");
            }

            if (vehicle.LastTechnicalReview > latestReviewDate)
            {
                throw new CustomArgumentException("Date of new technical review cannot be earlier than date of the previous review");
            }

            vehicle.LastTechnicalReview = latestReviewDate;
            vehicle.NextTechnicalReview = latestReviewDate.AddYears(1);

            await _db.SaveChangesAsync();
        }

        public async Task SaleVehicleAsync(Vehicle vehicle, DateTime saleDate)
        {
            vehicle = await _db.Vehicles.FindAsync(vehicle.Id);

            if (vehicle == null)
            {
                throw new ArgumentNullException("Vehicle not found");
            }

            vehicle.SaleDate = saleDate;
            await _db.SaveChangesAsync();
        }

        public async Task<IList<Vehicle>> GetVehiclesWithCloseInsuranceTermination(User user, DateTime referenceDate)
        {
            var vehiclesForUser = await GetVehiclesForUserAsync(user);
            return vehiclesForUser.Where(v => v.DaysToInsuranceExpires(referenceDate) <= Codes.Defaults.DaysToRemindAboutInsuranceTermination).ToList();
        }

        public async Task<IList<Vehicle>> GetVehiclesWithCloseNextReviewDate(User user, DateTime referenceDate)
        {
            var vehiclesForUser = await GetVehiclesForUserAsync(user);
            return vehiclesForUser.Where(v => v.DaysToNextTechnicalReview(referenceDate) <= Codes.Defaults.DaysToRemindAboutTechnicalReview).ToList();
        }

    }
}
