namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public class EmailSenderSettings
    {
        public string SmtpClientUrl { get; set; }
        public string SenderEmail { get; set; }
        public string SenderHeader { get; set; }
        public string SenderValues { get; set; }
    }
}
