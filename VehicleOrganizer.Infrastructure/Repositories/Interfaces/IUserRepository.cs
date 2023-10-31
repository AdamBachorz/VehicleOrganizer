﻿using BachorzLibrary.DAL.DAO;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IBaseDao<User, string>
    {
        Task<IList<User>> GetAllActiveAsync();
        Task AuthorizeUserAsync(User user, bool refreshUserAsDefault = true);
    }
}
