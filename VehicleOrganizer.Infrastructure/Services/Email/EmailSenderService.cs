using FluentNHibernate.Conventions.Instances;
using mailslurp.Api;
using mailslurp.Client;
using mailslurp.Model;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public class EmailSenderService
    {
        private InboxControllerApi _controller;
        private InboxDto _inbox;

        public EmailSenderService()
        {
            //Fail
            // first configure your api key
            var config = new Configuration();
            config.ApiKey.Add("x-api-key", "6a17b5e7251cd259aafd58675991c0d1b1dbcec5162b428e5ada8e6c7e2ea9b1");

            _controller = new InboxControllerApi(config);
            _inbox = _controller.CreateInbox();         
        }

        public void SendEmail(string subject, string body)
        {
            var sendEmailOptions = new SendEmailOptions()
            {
                To = new List<string>() { User.Default.Email },
                Subject = subject,
                Body = body
            };
            _controller.SendEmail(_inbox.Id, sendEmailOptions);
        }
    }
}
