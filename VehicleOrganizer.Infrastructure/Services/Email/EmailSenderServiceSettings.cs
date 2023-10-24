namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public class EmailSenderServiceSettings
    {
        public string SmtpClientUrl { get; set; }
        public string SenderEmail { get; set; }
        public string SenderHeader { get; set; }
        public string SenderValues { get; set; }
    }
}
