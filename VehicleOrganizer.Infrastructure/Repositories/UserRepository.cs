using BachorzLibrary.DAL.DotNetSix.Repositories;
using Microsoft.EntityFrameworkCore;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.Infrastructure.Repositories
{
    public class UserRepository : EFCBaseRepository<User, DataBaseContext, Guid>, IUserRepository
    {
        public UserRepository(DataBaseContext db) : base(db)
        {
        }

        public async Task<IList<User>> GetAllActiveAsync()
        {
            return await _db.Users.Where(u => u.IsActive).ToListAsync();
        }

        public async Task AuthorizeUserAsync(User user, bool refreshUserAsDefault = true)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Id.Equals(user.Id));

            if (existingUser is null) 
            {
                user.Id = Guid.NewGuid();
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                if (refreshUserAsDefault) 
                {
                    User.RefreshData(user);
                }
            }
        }
    }
}
