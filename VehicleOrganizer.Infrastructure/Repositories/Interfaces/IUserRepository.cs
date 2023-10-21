using BachorzLibrary.DAL.DAO;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IBaseDao<User, string>
    {
        Task AuthorizeUser(User user, bool refreshUserAsDefault = true);
    }
}
