using BachorzLibrary.Common;
using BachorzLibrary.Common.Extensions;
using BachorzLibrary.Common.Tools.Email;
using BachorzLibrary.Common.Tools.Html;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Infrastructure.Criteria;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSender _emailSender;
        private readonly HtmlHelper _htmlHelper;
        private readonly IOperationalActivityRepository _operationalActivityRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public EmailService(EmailSender emailSender, HtmlHelper htmlHelper, IOperationalActivityRepository operationalActivityRepository, 
            IVehicleRepository vehicleRepository)
        {
            _emailSender = emailSender;
            _htmlHelper = htmlHelper;
            _operationalActivityRepository = operationalActivityRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task InformAdminAboutProblemAsync(IList<string> errors)
        {
            if (errors.IsNullOrEmpty()) 
            {
                return;
            }

            _htmlHelper.Paragraph($"Dnia {DateTime.Now.ToString(Consts.DateFormat.DateFirstWithTime)} wystąpiły następujące błędy:");
            _htmlHelper.NextLine();

            var tableContent = new TableCellContent[errors.Count, 1];
            for (int i = 0; i < errors.Count; i++)
            {
                tableContent[i, 0] = new TableCellContent(errors[i]);
            }

            _htmlHelper.Table(rows: errors.Count, columns: 1, cellWidth: 500, cellHeight: 100, border: 10, tableContent);
            _htmlHelper.NextLine();

            await _emailSender.SendEmailAsync("[Error] Wystąpiły błędy", _htmlHelper.EndWithResult(), Codes.AdminEmail, "Adam");
        }

        public async Task RemindUserAboutActivitiesAsync(User user, OperationalActivityCriteria criteria = null)
        {
            if (!user.IsEmailOk)
            {
                return;
            }

            criteria ??= new OperationalActivityCriteria();
            criteria.ShouldSetReminderDate = true;
            var operationalActivitiySummaries = await _operationalActivityRepository.GetOperationalActivitiesForUserToRemindAsync(user, criteria);

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

            await _emailSender.SendEmailAsync("Przypomnienie o nadchodzących czynnościach", emailBody, user.Email, user.Name);
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

            await _emailSender.SendEmailAsync("Przypomnienie o nadchodzących terminach (ubezpieczenie lub przegląd techniczny)", 
                emailBody, user.Email, user.Name);
        }
    }
}
