using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Services.Email;

namespace VehicleOrganizer.Infrastructure.Tests.Services.Email
{
    public class EmailSenderServiceTests : BaseTests
    {
        private EmailSender _sut;

        [SetUp]
        public void Setup()
        {
            base.Setup();

            var values = _customConfig.ValuesBag["Sender"] as string;
            var settings = new EmailSenderSettings
            {
                SmtpClientUrl = "smtp.poczta.onet.pl",
                SenderValues = values,
                SenderEmail = "adar_1@op.pl",
                SenderHeader = Codes.AppName,
            };
            _sut = new EmailSender(settings);
        }

        [Test]
        [Explicit]
        public async Task ShouldSendEmail_SendEmail()
        {
            await _sut.SendEmailAsync("Testowy temat", "testowa treść <h1>Nagłówek</h1>", User.Default.Email, User.Default.Name);

            Assert.Pass();
        }
    }
}
