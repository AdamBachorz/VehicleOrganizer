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
        private readonly IVehicleRepository _vehicleRepository;

        public EmailService(EmailSender emailSenderService, HtmlHelper htmlHelper, IOperationalActivityRepository operationalActivityRepository, 
            IVehicleRepository vehicleRepository)
        {
            _emailSenderService = emailSenderService;
            _htmlHelper = htmlHelper;
            _operationalActivityRepository = operationalActivityRepository;
            _vehicleRepository = vehicleRepository;
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

        public async Task RemindUserAboutVehicleInsuranceOrTechnicalReviewAsync(User user, DateTime referenceDate)
        {
            if (!user.IsEmailOk)
            {
                return;
            }

            var vehiclesWithCloseInsuranceTermination = await _vehicleRepository.GetVehiclesWithCloseInsuranceTermination(user, referenceDate);
            var vehiclesWithCloseNextReviewDate = await _vehicleRepository.GetVehiclesWithCloseNextReviewDate(user, referenceDate);

            if (vehiclesWithCloseInsuranceTermination.IsNullOrEmpty() && vehiclesWithCloseNextReviewDate.IsNullOrEmpty())
            {
                return;
            }

            _htmlHelper.Begin();
            _htmlHelper.Paragraph("Poniżej znajduje się lista pojazdów, dla których zbliżają się ważne terminy:");
            _htmlHelper.NextLine();
            if (vehiclesWithCloseInsuranceTermination.IsNotNullOrEmpty())
            {
                foreach (var vehicle in vehiclesWithCloseInsuranceTermination)
                {
                    _htmlHelper.H(3, vehicle.Name);
                    _htmlHelper.Paragraph($"{vehicle.InsuranceTerminationPrompt(referenceDate)} ({vehicle.InsuranceTermination.ToShortDateString()})");
                    _htmlHelper.NextLine();
                }
                _htmlHelper.NextLine();
            }

            if (vehiclesWithCloseNextReviewDate.IsNotNullOrEmpty())
            {
                foreach (var vehicle in vehiclesWithCloseNextReviewDate)
                {
                    _htmlHelper.H(3, vehicle.Name);
                    _htmlHelper.Paragraph($"{vehicle.TechnicalReviewPrompt(referenceDate)} ({vehicle.NextTechnicalReview.ToShortDateString()})");
                    _htmlHelper.NextLine();
                }
                _htmlHelper.NextLine(); 
            }

            var emailBody = _htmlHelper.EndWithResult();

            await _emailSenderService.SendEmailAsync("Przypomnienie o nadchodzących terminach (ubezpieczenie lub przegląd techniczny)", 
                emailBody, user.Email, user.Name);
        }
    }
}
