using BachorzLibrary.DAL.DotNetSix.Repositories;
using Microsoft.EntityFrameworkCore;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.Infrastructure.Repositories
{
    public class VehicleRepository : EFCBaseRepository<Vehicle, DataBaseContext, int>, IVehicleRepository
    {
        public VehicleRepository(DataBaseContext db) : base(db)
        {
        }

        public async Task<Vehicle> AddVehicle(Vehicle vehcle, int mileage)
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

        public async Task<IList<Vehicle>> GetVehiclesForUser(User user, bool includeSold = false)
        {
            var query = _db.Vehicles.Where(v => v.User.Id == user.Id);

            if (!includeSold)
            {
                query = query.Where(v => !v.SaleDate.HasValue);
            }

            return await query.OrderBy(v => v.PurchaseDate).ToListAsync();
        }

        public async Task SaleVehicle(Vehicle vehcle, DateTime saleDate)
        {
            vehcle = await _db.Vehicles.FindAsync(vehcle.Id);

            if (vehcle == null)
            {
                throw new ArgumentNullException("Vehicle not found");
            }

            vehcle.SaleDate = saleDate;
            await _db.SaveChangesAsync();
        }
    }
}
