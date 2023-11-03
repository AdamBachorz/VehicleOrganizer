using BachorzLibrary.Common.Extensions;
using BachorzLibrary.Common.Tools.Email;
using BachorzLibrary.Common.Tools.Html;
using VehicleOrganizer.Infrastructure.Criteria;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSender _emailSenderService;
        private readonly HtmlHelper _htmlHelper;
        private readonly IOperationalActivityRepository _operationalActivityRepository;
        private readonly IUserRepository _userRepository;

        public EmailService(EmailSender emailSenderService, HtmlHelper htmlHelper, IOperationalActivityRepository operationalActivityRepository, 
            IUserRepository userRepository)
        {
            _emailSenderService = emailSenderService;
            _htmlHelper = htmlHelper;
            _operationalActivityRepository = operationalActivityRepository;
            _userRepository = userRepository;
        }

        public async Task RemindUserAboutActivitiesAsync(User user, OperationalActivityCriteria criteria = null)
        {
            if (!user.IsEmailOk)
            {
                return;
            }

            criteria ??= new OperationalActivityCriteria();
            var operationalActivitiySummaries = await _operationalActivityRepository.GetOpertationalActivitiesForUserToRemindAsync(user, criteria);

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
            foreach (var user in await _userRepository.GetAllActiveAsync())
            {
                await RemindUserAboutActivitiesAsync(user, new OperationalActivityCriteria());
            }
        }
    }
}
