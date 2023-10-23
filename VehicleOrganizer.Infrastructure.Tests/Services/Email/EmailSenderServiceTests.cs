using FluentEmail.Core;
using VehicleOrganizer.Infrastructure.Services.Email;

namespace VehicleOrganizer.Infrastructure.Tests.Services.Email
{
    public class EmailSenderServiceTests : BaseTests
    {
        private EmailSenderService _sut;

        [SetUp]
        public void Setup()
        {
            base.Setup();

            _sut = new EmailSenderService(_customConfig);
        }

        [Test]
        [Explicit]
        public async Task ShouldSendEmail_SendEmail()
        {
            await _sut.SendEmail("Testowy temat", "testowa treść <h1>Nagłówek</h1>");

            Assert.Pass();
        }
    }
}
