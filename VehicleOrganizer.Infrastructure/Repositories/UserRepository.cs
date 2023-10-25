using BachorzLibrary.DAL.DotNetSix.Repositories;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.Infrastructure.Repositories
{
    public class UserRepository : EFCBaseRepository<User, DataBaseContext, string>, IUserRepository
    {
        public UserRepository(DataBaseContext db) : base(db)
        {
        }

        public async Task AuthorizeUserAsync(User user, bool refreshUserAsDefault = true)
        {
            var existingUser = await _db.Users.FindAsync(user.Id);

            if (existingUser is null) 
            {
                user.Id = Guid.NewGuid().ToString();
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
