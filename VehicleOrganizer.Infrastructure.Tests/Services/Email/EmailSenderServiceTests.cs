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

            _sut = new EmailSenderService();
        }

        [Test]
        [Explicit]
        public void ShouldSendEmail_SendEmail()
        {
            _sut.SendEmail("Testowy temat", "testowa treść <h1>Nagłówek</h1>");

            Assert.Pass();
        }
    }
}
