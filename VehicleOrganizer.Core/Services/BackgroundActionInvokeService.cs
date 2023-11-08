using BachorzLibrary.Common.Extensions;
using System.Collections.Concurrent;
using VehicleOrganizer.Core.Services.Interfaces;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Services.Email;

namespace VehicleOrganizer.Core.Services
{
    public class BackgroundActionInvokeService : IBackgroundActionInvokeService
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        private ConcurrentBag<string> _errors = new ConcurrentBag<string>();

        public BackgroundActionInvokeService(IEmailService emailService, IUserRepository userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public IList<string> CurrentErrors()
        {
            return _errors.ToList();
        }

        public async Task InvokeAllAsync()
        {
            await AuthorizeDefaultUserAsync();
            await RunRemindersAsync();

            await _emailService.InformAdminAboutProblemAsync(_errors.ToList());
            
            if (!_errors.IsEmpty)
            {
                _errors.Clear();
            }
        }
        public async Task AuthorizeDefaultUserAsync()
        {
            try
            {
                await _userRepository.AuthorizeUserAsync(User.Default);
            }
            catch (Exception ex)
            {
                _errors.Add(ex.FullMessageWithStackTrace());
            }
        }

        public async Task RunRemindersAsync()
        {
            try
            {
                foreach (var user in await _userRepository.GetAllActiveAsync())
                {
                    try
                    {
                        await _emailService.RemindUserAboutActivitiesAsync(user);
                    }
                    catch (Exception ex)
                    {
                        _errors.Add(ex.FullMessageWithStackTrace());
                    }

                    try
                    {
                        await _emailService.RemindUserAboutVehicleInsuranceOrTechnicalReviewAsync(user, DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        _errors.Add(ex.FullMessageWithStackTrace());
                    }
                }
            }
            catch (Exception ex) 
            {
                _errors.Add(ex.FullMessageWithStackTrace());
            }
        }
    }
}
