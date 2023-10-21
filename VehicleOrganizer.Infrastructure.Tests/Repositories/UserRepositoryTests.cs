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
        public async Task ShouldAuthorizeUser_AuthorizeUser()
        {
            var newUser = _fixture.Create<User>();
            var existingUser = _fixture.Create<User>();
            var oldIdValue = newUser.Id;

            await _db.Users.AddAsync(existingUser);
            await _db.SaveChangesAsync();

            await _sut.AuthorizeUser(newUser, refreshUserAsDefault: false);

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
