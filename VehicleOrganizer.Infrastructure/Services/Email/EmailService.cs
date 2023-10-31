using BachorzLibrary.Common.Extensions;
using BachorzLibrary.Common.Tools.Html;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSenderService _emailSenderService;
        private readonly HtmlHelper _htmlHelper;
        private readonly IOperationalActivityRepository _operationalActivityRepository;

        public EmailService(EmailSenderService emailSenderService, HtmlHelper htmlHelper, IOperationalActivityRepository operationalActivityRepository)
        {
            _emailSenderService = emailSenderService;
            _htmlHelper = htmlHelper;
            _operationalActivityRepository = operationalActivityRepository;
        }

        public async Task RemindUserAboutActivitiesAsync(User user)
        {
            var operationalActivitiySummaries = await _operationalActivityRepository.GetOpertationalActivitiesForUserToRemindAsync(
                user, (Codes.Defaults.DaysToRemind, Codes.Defaults.MileageToRemind), DateTime.Now.Date, shouldSetReminderDate: false);

            if (operationalActivitiySummaries.IsNullOrEmpty())
            {
                return;
            }

            _htmlHelper.Begin();
            _htmlHelper.Paragraph("Poniżej znajduje się lista czynności, jakie w najbliższym czasie należy wykonać dla poszczególnych pojazdów:");
            _htmlHelper.NextLine();
            foreach (var operationalActivitiySummary in operationalActivitiySummaries)
            {
                _htmlHelper.H(3, operationalActivitiySummary.VehicleName);
                _htmlHelper.List(operationalActivitiySummary.ActivitySummaries);
                _htmlHelper.NextLine();
            }

            var emailBody = _htmlHelper.EndWithResult();

            await _emailSenderService.SendEmailAsync("Przypomnienie o nadchodzących czynnościach", emailBody, user.Email, user.Name);
        }

        public async Task RemindAllUsersAboutActivitiesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
