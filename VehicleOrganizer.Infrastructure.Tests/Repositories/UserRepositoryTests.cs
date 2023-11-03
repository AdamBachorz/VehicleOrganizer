using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.Infrastructure.Tests.Repositories
{
    public class UserRepositoryTests : BaseDataBaseTests
    {
        private IUserRepository _sut;

        [SetUp]
        public void Setup()
        {
            base.Setup();

            _sut = new UserRepository(_db);
        }

        [Test]
        public async Task ShouldGetActiveUsers_GetAllActiveAsync()
        {
            const int StartingNumberOfUsers = 4;
            var inactiveUser = _fixture.Create<User>();
            inactiveUser.IsActive = false;

            var users = Enumerable.Range(1, StartingNumberOfUsers)
                .Select(_ =>
                {
                    var user = _fixture.Create<User>();
                    user.IsActive = true;
                    return user;
                })
                .Append(inactiveUser);

            await _db.Users.AddRangeAsync(users);
            await _db.SaveChangesAsync();

            var result = await _sut.GetAllActiveAsync();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null.Or.Empty);
                Assert.That(result, Has.Count.EqualTo(StartingNumberOfUsers));
                Assert.That(result.All(u => u.IsActive), Is.True);
            });
        }

        [Test]
        public async Task ShouldAuthorizeUser_AuthorizeUser()
        {
            var newUser = _fixture.Create<User>();
            var existingUser = _fixture.Create<User>();
            var oldIdValue = newUser.Id;

            await _db.Users.AddAsync(existingUser);
            await _db.SaveChangesAsync();

            await _sut.AuthorizeUserAsync(newUser, refreshUserAsDefault: false);

            var allUsers = _sut.GetAll();

            Assert.Multiple(() =>
            {
                Assert.That(newUser.Id, Is.Not.Null.Or.Empty);
                Assert.That(existingUser.Id, Is.Not.Null.Or.Empty);
                Assert.That(newUser.Id, Is.Not.EqualTo(oldIdValue));
                Assert.That(allUsers, Has.Count.EqualTo(2));
            });
        }
    }
}
